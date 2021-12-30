using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsCollaborationAPI.Models
{
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int ParentId { get; set; }

        public int CollaboratorId { get; set; }

        public PrivacyType Privacy { get; set; }
    }
}
