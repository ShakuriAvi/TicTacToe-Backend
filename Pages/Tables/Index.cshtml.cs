using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.Pages.Lists
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Player> Players { get; set; }

        public async Task OnGet()
        {
            Players = await _db.Player.ToListAsync();
        }
    }
}
