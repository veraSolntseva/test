using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.DbObjects
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        [StringLength(40)]
        public string Surname { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(60)]
        public string MiddleName { get; set; }

        [MinLength(6)]
        [MaxLength(16)]
        public string UniqueName { get; set; }


        public ICollection<StudentsInGroups> StudentsInGroups { get; set; }
    }
}
