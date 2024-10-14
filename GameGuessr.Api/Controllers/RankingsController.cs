using GameGuessr.Api.Infrastructure;
using GameGuessr.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameGuessr.Api.Controllers
{
    public class RankingsController : BaseController
    {
        private readonly ILogger<RankingsController> _logger;
        private readonly AppDbContext _dbContext;

        public RankingsController(ILogger<RankingsController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{mode}")]
        public List<RankingItem> Get(GameMode mode)
        {
            var rankingItems = _dbContext.Scores
               .Include(x => x.Player)
               .Where(x => x.Mode == mode)
               .OrderByDescending(x => x.Points)
               .Take(10)
               .Select(x => new RankingItem
                {
                    Score = x.Points,
                    Player = x.Player.Name,
                    Date = x.CreatedAt
                })
               .ToList();
            
            return rankingItems;
        }
    }
}