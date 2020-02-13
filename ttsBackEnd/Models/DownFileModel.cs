using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class DownFileModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}