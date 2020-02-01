using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IStudentService
    {
        /// <summary>
        /// Получить список студентов со списками групп
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StudentDataModel>> GetStudentList();

        /// <summary>
        /// Получить список студентов 1 группы
        /// </summary>
        /// <param name="groupId">ид группы</param>
        /// <returns></returns>
        Task<IEnumerable<StudentDataModel>> GetStudentListForGroup(int groupId);

        /// <summary>
        /// Получить студента
        /// </summary>
        /// <param name="studentId">ид студента</param>
        /// <returns></returns>
        Task<StudentDataModel> GetStudent(int studentId);

        /// <summary>
        /// Добавить студента
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task AddStudent(StudentDataModel student);

        /// <summary>
        /// Обновить студента
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task UpdateStudent(StudentDataModel student);

        /// <summary>
        /// Удалить студента
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task DeleteStudent(int studentId);

        /// <summary>
        /// Проверить уникальность поля UniqueName
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <returns>true - уникальное</returns>
        Task<bool> CheckUniqueName(string uniqueName);
    }
}
