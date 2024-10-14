using System.Security.Cryptography;
using System.Text;
using GameGuessr.Api.Domain;
using GameGuessr.Api.Infrastructure;
using GameGuessr.Shared;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace GameGuessr.Api.Controllers
{
    public class PlayersController : BaseController
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly AppDbContext _dbContext;

        public PlayersController(ILogger<PlayersController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public ValueDto<SaveScoreStatus> Post(PlayerScore playerScore)
        {
            if (Encoding.UTF32.GetString(playerScore.Sh).ReverseString() != playerScore.Score.ToString())
                return ValueDto<SaveScoreStatus>.Create(SaveScoreStatus.AccountBannedForCheating);
            
            if (playerScore.Password.Length > 150)
                return ValueDto<SaveScoreStatus>.Create(SaveScoreStatus.IncorrectPasswordFormat);
            
            if (playerScore.Name.Length > 20)
                return ValueDto<SaveScoreStatus>.Create(SaveScoreStatus.IncorrectNickFormat);
            
            var player = _dbContext.Players.FirstOrDefault(x => x.Name == playerScore.Name);

            if (player == null)
            {
                var hashsalt = EncryptPassword(playerScore.Password);
                var passwordHash =  hashsalt.Hash;
                var salt = hashsalt.Salt;
                player = Player.Create(playerScore.Name, passwordHash, salt).Value;
                _dbContext.Players.Add(player);
                _dbContext.SaveChanges();
            }
            else if (VerifyPassword(playerScore.Password, player.Salt, player.Password) == false)
                return ValueDto<SaveScoreStatus>.Create(SaveScoreStatus.WrongPassword);
            

            var score = Score.Create(playerScore.Score, playerScore.Mode, player.Id).Value;
            _dbContext.Scores.Add(score);
            _dbContext.SaveChanges();
            
            return ValueDto<SaveScoreStatus>.Create(SaveScoreStatus.Saved);
        }

        public class HashSalt
        {
            public string Hash { get; set; }
            public byte[] Salt { get; set; }
        }
        
        private HashSalt EncryptPassword(byte[] password)
        {
            var passwordEncoded = Encoding.UTF32.GetString(password).ReverseString();
            var salt = new byte[128 / 8]; // Generate a 128-bit salt using a secure PRNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordEncoded,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return new HashSalt { Hash = encryptedPassw , Salt = salt };
        }
    
        private bool VerifyPassword(byte[] enteredPassword, byte[] salt, string storedPassword)
        {
            var passwordEncoded = Encoding.UTF32.GetString(enteredPassword).ReverseString();
            
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordEncoded,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassw == storedPassword;
        }
    }
}
