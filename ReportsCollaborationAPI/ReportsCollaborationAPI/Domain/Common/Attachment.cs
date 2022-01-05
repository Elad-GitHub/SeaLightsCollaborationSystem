using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsCollaborationAPI.Domain
{
    public abstract class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public int ParentId { get; set; }

        public int CollaboratorId { get; set; }

        public PrivacyLevel Privacy { get; set; }
    }
}
