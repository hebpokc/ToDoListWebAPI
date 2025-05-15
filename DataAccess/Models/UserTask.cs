using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserTask
    {
        public string UserId { get; set; } = string.Empty;
        public int? TaskId { get; set; }

        public bool IsOwner { get; set; } = false;

        public ApplicationUser? User { get; set; }

        public Task? Task { get; set; }
    }

}
