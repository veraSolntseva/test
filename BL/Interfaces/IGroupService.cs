using BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IGroupService
    {
        /// <summary>
        /// Получить список групп
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<GroupDataModel>> GetGroupList();

        /// <summary>
        /// Получить список студентов 1 группы
        /// </summary>
        /// <param name="groupId">ид группы</param>
        /// <returns></returns>
        Task<IEnumerable<StudentDataModel>> GetStudentListForGroup(int groupId);

        /// <summary>
        /// получить группу
        /// </summary>
        /// <param name="groupId">ид группы</param>
        /// <returns></returns>
        Task<GroupDataModel> GetGroup(int groupId);

        /// <summary>
        /// Добавить группу
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task AddGroup(GroupDataModel group);

        /// <summary>
        /// Обновить группу
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task UpdateGroup(GroupDataModel group);

        /// <summary>
        /// Удалить группу
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task DeleteGroup(int groupId);

        /// <summary>
        /// Добавить сотудников в группу
        /// </summary>
        /// <param name="studentIdArray">массив ид сотрудников</param>
        /// <param name="groupId">id группы</param>
        /// <returns></returns>
        Task AddStudentToGroup(int studentId, int groupId);

        /// <summary>
        /// Удалить сотрудника из группы
        /// </summary>
        /// <param name="studentId">ид сотрудника</param>
        /// <param name="groupId">ид группы</param>
        /// <returns></returns>
        Task RemoveStudentFromGroup(int studentId, int groupId);
    }
}
