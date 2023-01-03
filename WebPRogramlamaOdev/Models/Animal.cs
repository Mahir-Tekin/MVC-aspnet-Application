using System.ComponentModel.DataAnnotations;

namespace WebPRogramlamaOdev.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Story { get; set; }
    }
}
