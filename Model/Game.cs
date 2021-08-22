using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Model
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        public string GameSteps { get; set; }
        public string Date { get; set; }
        public string Result { get; set; }

    }
}
