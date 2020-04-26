using System.ComponentModel.DataAnnotations;

namespace ttsBackEnd.Models
{
    public class DownFileModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}