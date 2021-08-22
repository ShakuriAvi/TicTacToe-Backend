using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Model
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Password { get; set; }
        [Required]
        public string Name { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }
        public int Wins { get; set; }
    }
}
