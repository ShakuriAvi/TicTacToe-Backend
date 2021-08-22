using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.Controllers
{
    namespace BookList.Controllers
    {
        [Route("api/player")]
        [ApiController]
        public class PlayerController : Controller
        {
            private readonly ApplicationDbContext _db;
            public PlayerController(ApplicationDbContext db)
            {
                _db = db;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Json(new { data = await _db.Player.ToListAsync() });
            }

            [HttpGet]
            [Route("/games")]
            public async Task<IActionResult> GetAllGames()
            {
                return Json(new { data = await _db.Game.ToListAsync() });
            }

            [HttpPut]
            public async Task<IActionResult> PutData(int id, int win, int draw, int lose)
            {
                if (id != 0)
                {
                    var PlayerFromDb = await _db.Player.FindAsync(id);

                    if (PlayerFromDb != null)
                    {

                        PlayerFromDb.Loses = lose;
                        PlayerFromDb.Wins = win;
                        PlayerFromDb.Draws = draw;
                        await _db.SaveChangesAsync();
                        return Json(new { id, win, draw, lose });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            [HttpPost("{game}")]
            public async Task<IActionResult> PutGameData(string gameSteps, string date, int id, string result)
            {

                if (id != 0)
                {
                    Game game = new Game();
                    game.Date = date;
                    game.PlayerId = id;
                    game.GameSteps = gameSteps;
                    game.Result = result;
                    await _db.Game.AddAsync(game);
                    await _db.SaveChangesAsync(); //data pushed to 
                    return Json(new { msg = "Sccess" });
                }
                else
                {
                    return BadRequest();
                }
            }

            [HttpPost("addplayer")]
            public async Task<IActionResult> PostNewPlayer(string username, int pwd)
            {
                if (username.Length != 0 && pwd != 0)
                {
                    var PlayerFromDb = await _db.Player.FirstOrDefaultAsync(u => u.Name == username);
                    if (PlayerFromDb == null)
                    {
                        Player player = new Player();
                        player.Name = username;
                        player.Password = pwd;
                        player.Loses = 0;
                        player.Draws = 0;
                        player.Wins = 0;

                        await _db.Player.AddAsync(player); //data saved into a queue
                        await _db.SaveChangesAsync(); //data pushed to database
                        return RedirectToPage("/Index"); //moving back to books index page
                    }
                    else
                    {
                        return Json(new { status = 400 });
                    }
                }
                else
                {
                    return Json(new { status = 400 });
                }
            }

            [HttpGet("playergames")]
            public async Task<IActionResult> GetPlayerGames(int id)
            {
                return Json(new { data = await _db.Game.Where(a => a.PlayerId == id).ToListAsync() });
            }

            [HttpGet("systemstep")]
            public async Task<IActionResult> GetSystemStep(string steps)
            {
                int[][] matrix = convertTo2D(steps);
                Random r = new Random();
                int row = r.Next(0, 5);
                int col = r.Next(0, 5);
                while (matrix[row][col] != -1)
                {
                    row = r.Next(0, 5);
                    col = r.Next(0, 5);
                }
                return Json(new { row = row, col = col });
            }
            private int[][] convertTo2D(string steps)
            {
                var cleanedRows = Regex.Split(steps, @"}\s*,\s*{")
                        .Select(r => r.Replace("{", "").Replace("}", "").Trim())
                        .ToList();

                var matrix = new int[cleanedRows.Count][];
                for (var i = 0; i < cleanedRows.Count; i++)
                {
                    var data = cleanedRows.ElementAt(i).Split(',');
                    matrix[i] = data.Select(c => int.Parse(c.Trim())).ToArray();
                }
                return matrix;
            }
            //         [HttpGet("{playergames}")]
            //         public async Task<List<Game>> GetPlayerGames(int id)
            //         {
            //             var data = await _db.Game.Where(a => a.PlayerId == id).ToListAsync();
            //             return data;
            //        }

            /*[HttpDelete]
                public async Task<IActionResult> Delete(int id)
                {
                    var bookFromDb = await _db.Player.FirstOrDefaultAsync(u => u.Id == id);
                    if (bookFromDb == null)
                    {
                        return Json(new { success = false, message = "Error White Deleting" });
                    }
                    _db.Player.Remove(bookFromDb);
                    await _db.SaveChangesAsync();
                    return Json(new { success = true, message = "Delete Successful" });
                }*/
        }
    }
}
