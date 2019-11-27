using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class DownFileModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string link { get; set; }
    }
}