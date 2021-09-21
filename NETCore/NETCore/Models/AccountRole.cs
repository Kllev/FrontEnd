using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        public string NIK { get; set; }
        public int RoleId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
