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

        public List<GroupDataModel> GroupList { get; set; }



        public StudentDataModel() { }

        public StudentDataModel(Student student, IEnumerable<Group> groupList)
        {
            if (student is null)
                return;

            this.Id = student.Id;
            this.Gender = (Gender)student.Gender;
            this.Surname = student.Surname;
            this.Name = student.Name;
            this.MiddleName = student.MiddleName;
            this.UniqueName = student.UniqueName;
            this.GroupList = groupList?.Select(g => new GroupDataModel(g)).ToList();
        }

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
