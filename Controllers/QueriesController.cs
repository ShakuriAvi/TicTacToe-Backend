using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Model;

namespace TicTacToe.Controllers
{
    [Route("api/queries")]
    [ApiController]
    public class QueriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        //You use these variables throughout the application.

        public QueriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("getallgames")]
        public IList<Game> GetAllGames(int id)
        {
            return _db.Game.FromSqlRaw("SELECT * FROM Game WHERE PlayerId = {0}", id).ToList();
        }


        [HttpGet("getallplayers")]
        public IList<Player> GetAllPlayers()
        {
            return _db.Player.FromSqlRaw("SELECT * FROM Player").ToList();
        }

        [HttpGet("getallplayersasc")]
        public IList<Player> GetAllPlayersDesc()
        {
            return _db.Player.OrderBy(u => u.Loses + u.Draws + u.Wins).ToList();
        }

        [HttpGet("getplayer")]
        public Player GetPlayer(string name)
        {
            var PlayerFromDb = _db.Player.FirstOrDefault(u => u.Name == name);
            return PlayerFromDb;
        }

        [HttpDelete("deletegame")]
        public bool DeleteGame(int id)
        {
            var GameFromDb = _db.Game.FirstOrDefault(u => u.Id == id);
            if (GameFromDb == null)
            {
                return false;
            }
            _db.Game.Remove(GameFromDb);
            _db.SaveChanges();

            return true;
        }

        [HttpDelete("deleteplayer")]
        public bool DeletePlayer(int id)
        {
            var PlayerFromDb = _db.Player.FirstOrDefault(u => u.Id == id);
            if (PlayerFromDb == null)
            {
                return false;
            }
            _db.Player.Remove(PlayerFromDb);
            _db.SaveChanges();

            return true;
        }

        [HttpPut("updateplayer")]
        public IActionResult UpdatePlayer(int id, string username, int pwd)
        {
            if (username.Length != 0 && pwd != 0)
            {
                var PlayerFromDb = _db.Player.Find(id);
                if (PlayerFromDb != null)
                {
                    PlayerFromDb.Name = username;
                    PlayerFromDb.Password = pwd;

                    _db.Update(PlayerFromDb); //data pushed to database
                    _db.SaveChanges();
                    return Json(new { status = 200 }); //moving back to books index page
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
    }
}
