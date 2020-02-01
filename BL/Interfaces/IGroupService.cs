using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Получить список групп для студента
        /// </summary>
        /// <param name="studentId">ид студента</param>
        /// <returns></returns>
        Task<IEnumerable<GroupDataModel>> GetGroupListForStudent(int studentId);

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
    }
}
