using System.ComponentModel.DataAnnotations;

namespace ttsBackEnd.Models
{
    public class FileDownload
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}