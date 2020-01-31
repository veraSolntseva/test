using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDataModel>> GetGroupList();

        Task<IEnumerable<GroupDataModel>> GetGroupListForStudent(int studentId);

        Task<GroupDataModel> GetGroup(int groupId);

        Task AddGroup(GroupDataModel group);

        Task UpdateGroup(GroupDataModel group);

        Task DeleteGroup(int groupId);
    }
}
