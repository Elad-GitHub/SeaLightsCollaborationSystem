using System.ComponentModel.DataAnnotations;

namespace ReportsCollaborationAPI.Models
{
    public class File : Attachment
    {
        [Required]
        public string Link { get; set; }
    }
}
