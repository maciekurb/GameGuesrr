using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using GameGuessr.Api.Domain.Common;

namespace GameGuessr.Api.Domain;

public class Player : IAuditable
{
    internal Player() { }

    [Key]
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }
    public string Password { get; internal set; }
    public byte[] Salt { get; internal set; }

    public virtual ICollection<Score> GameSuggestions { get; internal set; } = new HashSet<Score>();

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static Result<Player> Create(string name, string password, byte[] salt) =>
        Result.Success()
           .EnsureHasValue(name, nameof(name))
           .EnsureHasValue(password, nameof(password))
           .Map(() => new Player
            {
                Name = name,
                Password = password,
                Salt = salt
            });
}   
