using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsCollaborationAPI.Models
{
    public class File : Attachment
    {
        [Required]
        public string Link { get; set; }
    }
}
