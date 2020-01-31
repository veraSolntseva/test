using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.DbObjects
{
    public class Group
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }


        public ICollection<StudentsInGroups> StudentsInGroups { get; set; }
    }
}
