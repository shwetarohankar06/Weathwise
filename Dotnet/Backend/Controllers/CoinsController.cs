using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using WealthWiseAPI.Data;
using WealthWiseAPI.Models;

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CoinController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add/coin")]
        public async Task<IActionResult> AddCoinToWatchlist([FromBody] WatchlistUser coinDto)
        {
            var success = await _context.watchlistUsers.AddAsync(coinDto);
            await _context.SaveChangesAsync();

            //if (!success)
            //    return BadRequest("Coin already in watchlist.");
            return Ok("Coin added successfully.");
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyCoin([FromBody] WatchlistUser coinDto)
        {
            var success = await _context.watchlistUsers.AddAsync(coinDto);
            //if (!success)
            //    return BadRequest("Coin already in watchlist.");
            return Ok("Coin added successfully.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coin>>> GetCoins()
        {

            var CoinData = _context.CoinData.ToList();

            return Ok(CoinData);
        }

        [HttpGet("getactiondata/{userId}/{actionType}")]
        public async Task<ActionResult<IEnumerable<WatchlistUser>>> GetActionData(int userId, string actionType)
        {
            if (_context.watchlistUsers == null)
            {
                return NotFound("Database context is not initialized.");
            }
            var coinData = _context.watchlistUsers.ToList();

            coinData = coinData.Where(c => c.UserId == userId && c.Action == actionType).ToList();
            return Ok(coinData);
        }



        //[HttpPost]
        //public async Task<ActionResult<CoinData>> AddCoin([FromBody] Coin newCoin)
        //{
        //    if (newCoin == null)
        //    {
        //        return BadRequest("Invalid coin data.");
        //    }

        //    var coinData = new CoinData
        //    {
        //        Name = newCoin.Name,
        //        Symbol = newCoin.Symbol,
        //        Price = newCoin.Price,
        //        MarketCap = newCoin.MarketCap,
        //        Volume = newCoin.Volume,
        //        Change24h = newCoin.Change24h
        //    };

        //    _context.CoinData.Add(coinData);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCoins), new { id = coinData.Id }, coinData);
        //}
        //[HttpPost]
        //public async Task<ActionResult<CoinData>> AddCoin([FromBody] Coin newCoin)
        //{
        //    if (newCoin == null)
        //    {
        //        return BadRequest("Invalid coin data.");
        //    }

        //    var coinData = new CoinData
        //    {
        //        Name = newCoin.Name,
        //        Symbol = newCoin.Symbol,
        //        Price = newCoin.Price,
        //        MarketCap = newCoin.MarketCap,
        //        Volume = newCoin.Volume,
        //        Change24h = newCoin.Change24h,
        //        //ImageUrl = string.IsNullOrEmpty(newCoin.ImageUrl) ? "https://via.placeholder.com/150" : newCoin.ImageUrl
        //    };

        //    _context.CoinData.Add(coinData);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCoins), new { id = coinData.Id }, coinData);
        //}




        [HttpDelete("remove/{userId}/{id}")]
        public async Task<IActionResult> DeleteCoin(int userId, int id)
        {
            if (_context.watchlistUsers == null)
            {
                return NotFound("Database context is not initialized.");
            }

            // Fetch the coin associated with the user
            var coin = _context.watchlistUsers.FirstOrDefault(c => c.Id == id && c.UserId == userId);

            if (coin == null)
            {
                return NotFound($"Coin with ID {id} for User {userId} not found.");
            }

            _context.watchlistUsers.Remove(coin);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Coin deleted successfully", coinId = id });
        }



    }
}
