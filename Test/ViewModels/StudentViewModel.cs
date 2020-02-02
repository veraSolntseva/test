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
        [Required(ErrorMessage ="Обязательное поле")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 2)]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(40, ErrorMessage ="Не более 40 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(40, ErrorMessage = "Не более 40 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(60, ErrorMessage = "Не более 60 символов")]
        public string MiddleName { get; set; }

        [MinLength(6, ErrorMessage = "Не менее 6 символов")]
        [MaxLength(16, ErrorMessage = "Не более 16 символов")]
        public string UniqueName { get; set; }

        public string Groups { get; set; }

        public string FullName => this.Surname + " " + this.Name + " " + this.MiddleName;



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
            this.Groups = studentDataModel.GroupList is null ? "" : string.Join(',', studentDataModel.GroupList?.Select(g => g.Name));
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
