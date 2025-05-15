using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public double Amount { get; set; } = 0.0;
        public DateTime? Date { get; set; }
        public string Description { get; set; } = string.Empty;

        public int? CategoryId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }
        public Category? Category { get; set; }
    }

}
