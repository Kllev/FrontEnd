using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_educations")]

    public class Education
    {
        [Key]
        public int Id { get; set; } 
        public string Degree { get; set; } 
        public string GPA { get; set; }     
        [JsonIgnore]
        public virtual University University { get; set; }
        public int UniversityId { get; set; }
        public virtual ICollection<Profiling> Profilings { get; set; }
    }
}
