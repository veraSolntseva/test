using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDataModel>> GetStudentList();

        Task<IEnumerable<StudentDataModel>> GetStudentListForGroup(int groupId);

        Task<StudentDataModel> GetStudent(int studentId);

        Task AddStudent(StudentDataModel student);

        Task UpdateStudent(StudentDataModel student);

        Task DeleteStudent(int studentId);
    }
}
