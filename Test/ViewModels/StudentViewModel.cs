using BL;
using BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test.Tools;
using static BL.GenderEnum;

namespace Test.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Gender { get; set; }

        [Required]
        public int GenderId { get; set; }

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

        public List<GroupDataModel> GroupList { get; set; }

        public string FullName { get; set; }



        public StudentViewModel() { }

        public StudentViewModel(StudentDataModel studentDataModel)
        {
            this.Id = studentDataModel.Id;
            this.Gender = EnumHelper<Gender>.GetDisplayValue(studentDataModel.Gender);
            this.GenderId = (int)studentDataModel.Gender;
            this.Surname = studentDataModel.Surname;
            this.Name = studentDataModel.Name;
            this.MiddleName = studentDataModel.MiddleName;
            this.UniqueName = studentDataModel.UniqueName;
            this.FullName = studentDataModel.FullName;
        }

        public StudentViewModel(StudentDataModel studentDataModel, IEnumerable<GroupDataModel> groups)
        {
            this.Id = studentDataModel.Id;
            this.Gender = EnumHelper<Gender>.GetDisplayValue(studentDataModel.Gender);
            this.GenderId = (int)studentDataModel.Gender;
            this.Surname = studentDataModel.Surname;
            this.Name = studentDataModel.Name;
            this.MiddleName = studentDataModel.MiddleName;
            this.UniqueName = studentDataModel.UniqueName;
            this.FullName = studentDataModel.FullName;
            this.GroupList = groups.ToList();
        }


        public StudentDataModel GetDataModel()
        {
            return new StudentDataModel()
            {
                Id = this.Id,
                Gender = (Gender)this.GenderId,
                Surname = this.Surname,
                Name = this.Name,
                MiddleName = this.MiddleName,
                UniqueName = this.UniqueName
            };
        }
    }
}
