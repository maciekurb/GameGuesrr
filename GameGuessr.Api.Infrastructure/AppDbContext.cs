using GameGuessr.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameGuessr.Api.Domain.Common;
using GameGuessr.Api.Domain.Genres;
using GameGuessr.Api.Domain.Platforms;
using GameGuessr.Api.Domain.Screenshots;
using GameGuessr.Api.Infrastructure.Common;
using MediatR;

namespace GameGuessr.Api.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<GameSuggestion> GameSuggestions { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeProvider dateTimeProvider, IMediator mediator)
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
            _mediator = mediator;
        }
        
        public override int SaveChanges()
        {
            ApplyIAuditableBehavior();

            var result = base.SaveChanges();

            return result;
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyIAuditableBehavior();

            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GameSuggestion>()
               .HasOne(s => s.Game)
               .WithMany(g => g.GameSuggestions)
               .HasForeignKey(s => s.GameId);

            builder.Entity<GameSuggestion>()
               .HasOne(s => s.SuggestedGame)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
            
            base.OnModelCreating(builder);

            builder.HasPostgresExtension("uuid-ossp");
            builder.HasPostgresExtension("fuzzystrmatch");
        }

        private void ApplyIAuditableBehavior()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTimeProvider.UtcNow.SetKindUtc();

                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = _dateTimeProvider.UtcNow.SetKindUtc();

                        break;
                }
            }
        }
        
        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
        {
            if (_mediator == null)
                return;

            var domainEntities = ChangeTracker
               .Entries<IHasDomainEvents>()
               .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
               .ToList();

            var domainEvents = domainEntities
               .SelectMany(x => x.Entity.DomainEvents)
               .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
