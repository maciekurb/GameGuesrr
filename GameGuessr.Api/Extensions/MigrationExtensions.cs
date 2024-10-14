using GameGuessr.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GameGuessr.Api.Extensions;

public static class MigrationExtensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
        return app;
    }
}
