using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForEntityFramework
{
    public class UserCategory
    {
        [Key] public int Id { get; set; }
        public string ServiceCategory { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
