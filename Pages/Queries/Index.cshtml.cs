using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Model;

namespace TicTacToe.Pages.Queries
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

    }
}
