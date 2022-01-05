using System.ComponentModel.DataAnnotations;

namespace ReportsCollaborationAPI.Domain
{
    public class File : Attachment
    {
        [Required]
        public string Link { get; set; }
    }
}
