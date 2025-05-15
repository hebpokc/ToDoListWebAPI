using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AuditLog
    {
        public int? Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string ObjectType { get; set; } = string.Empty;
        public int? ObjectId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;

        public ApplicationUser? User { get; set; }
    }

}
