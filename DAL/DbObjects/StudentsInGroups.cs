using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.DbObjects
{
    public class StudentsInGroups
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Group")]
        public int GroupId { get; set; }


        public virtual Student Student { get; set; }
        public virtual Group Group { get; set; }
    }
}
