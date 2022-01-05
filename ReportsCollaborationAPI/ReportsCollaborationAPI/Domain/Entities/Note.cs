using System.ComponentModel.DataAnnotations;

namespace ReportsCollaborationAPI.Domain
{
    public class Note : Attachment
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name can only be 50 characters long")]
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
