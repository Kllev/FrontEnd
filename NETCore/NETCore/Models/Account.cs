using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_tr_accounts")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
