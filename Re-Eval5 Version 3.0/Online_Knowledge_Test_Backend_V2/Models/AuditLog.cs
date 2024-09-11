using System.ComponentModel.DataAnnotations;

namespace Online_Knowledge_Test_Backend_V2.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [Required, StringLength(50)]
        public string EntityName { get; set; }

        [Required, StringLength(255)]
        public string ChangeType { get; set; }

        [Required, StringLength(100)]
        public string ChangedBy { get; set; }

        [Required]
        public DateTime ChangeDate { get; set; } = DateTime.Now;

        [StringLength(400)]
        public string OriginalValue { get; set; }

        [StringLength(400)]
        public string ModifiedValue { get; set; }

        [StringLength(400)]
        public string Description { get; set; }
    }
}
