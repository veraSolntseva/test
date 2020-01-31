using DAL.DbObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SampleData
    {
        public static void InitDataBase(TestContext context)
        {
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student() { Gender = 1, Surname = "Петров", Name = "Петр", MiddleName = "Петрович", UniqueName = null },
                    new Student() { Gender = 1, Surname = "Сидоров", Name = "Сидор", MiddleName = "Сидорович", UniqueName = "sidorov" },
                    new Student() { Gender = 2, Surname = "Степанова", Name = "Ольга", MiddleName = "Петровна", UniqueName = "степанова" },
                    new Student() { Gender = 1, Surname = "Иванов", Name = "Иван", MiddleName = "Иванович", UniqueName = "162ии-36921" },
                    new Student() { Gender = 2, Surname = "Степанова", Name = "Марина", MiddleName = "Альбертовна", UniqueName = null });

                context.SaveChanges();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                    new Group() {  Name = "1бух-2019" },
                    new Group() { Name = "1прогр-2019" },
                    new Group() { Name = "2мен-2019" });

                context.SaveChanges();
            }

            if (!context.StudentsInGroups.Any())
            {
                List<int> studentIdList = context.Students.Select(s => s.Id).ToList();

                List<int> groupIdList = context.Groups.Select(g => g.ID).ToList();

                foreach (int studentId in studentIdList)
                    context.AddRange(groupIdList.Select(groupId => new StudentsInGroups() { GroupId = groupId, StudentId = studentId }));

                context.SaveChanges();
            }
        }

    }
}
