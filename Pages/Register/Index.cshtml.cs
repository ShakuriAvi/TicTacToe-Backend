using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicTacToe.Model;

namespace TicTacToe.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Player Player { get; set; }

        public void OnGet()
        {
        }

        /*        public async Task<IActionResult> OnPost()
                {
                    if (ModelState.IsValid)
                    {
                        var PlayerFromDb = await _db.Player.FirstOrDefaultAsync(u => u.Name == Player.Name);

                        if (PlayerFromDb == null)
                        {
                            await _db.Player.AddAsync(Player); //data saved into a queue
                            await _db.SaveChangesAsync(); //data pushed to database
                            return RedirectToPage("/Index"); //moving back to books index page
                        }
                        else
                        {
                            return Page();
                        }
                    }
                    else
                    {
                        return Page();
                    }
                }*/
    }
}
