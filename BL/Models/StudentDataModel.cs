using DAL.DbObjects;
using System.Collections.Generic;
using System.Linq;
using static BL.GenderEnum;

namespace BL.Models
{
    public class StudentDataModel
    {
        public int Id { get; set; }

        public Gender Gender { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string UniqueName { get; set; }

        public List<int> GroupsIdList { get; set; }

        public string FullName => this.Surname + " " + this.Name + " " + this.MiddleName;


        public StudentDataModel() { }

        public StudentDataModel(Student student)
        {
            if (student is null)
                return;

            this.Id = student.Id;
            this.Gender = (Gender)student.Gender;
            this.Surname = student.Surname;
            this.Name = student.Name;
            this.MiddleName = student.MiddleName;
            this.UniqueName = student.UniqueName;
            this.GroupsIdList = student.StudentsInGroups?.Select(i => i.GroupId).ToList();
        }

        public Student FillToEntity()
        {
            return new Student()
            {
                Id = this.Id,
                Gender = (int)this.Gender,
                Surname = this.Surname,
                Name = this.Name,
                MiddleName = this.MiddleName,
                UniqueName = this.UniqueName
            };
        }
    }
}
