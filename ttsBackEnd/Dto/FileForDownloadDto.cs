using System.ComponentModel.DataAnnotations;

namespace ttsBackEnd.Dto
{
    public class FileForDownloadDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}