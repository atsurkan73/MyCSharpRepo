using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkDbMigration
{
    internal class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int ProductCode { get; set; }
        public int Samples { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public DateTime Date { get; set; } = DateTime.Now;
        
    }
}
