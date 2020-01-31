using BL.Interfaces;
using BL.Models;
using DAL;
using DAL.DbObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class GroupService : IGroupService
    {
        private readonly TestContext _context;

        public GroupService(TestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupDataModel>> GetGroupList()
        {
            List<GroupDataModel> groupList = (from e in await _context.Groups.Include(o => o.StudentsInGroups).AsNoTracking().ToListAsync() select e)
                .Select(e => new GroupDataModel(e)).OrderBy(e => e.Name).ToList();

            return groupList;
        }

        public async Task<IEnumerable<GroupDataModel>> GetGroupListForStudent(int studentId)
        {
            List<Group> groupEntityList = (from e in await _context.Groups.Include(i => i.StudentsInGroups).AsNoTracking().ToListAsync() select e)
                .Where(i => i.StudentsInGroups.Any(s => s.StudentId == studentId)).ToList();

            List<GroupDataModel> groupList = groupEntityList.Select(e => new GroupDataModel(e)).OrderBy(e => e.Name).ToList();

            return groupList;
        }

        public async Task AddGroup(GroupDataModel group)
        {
            Group entity = group.FillToEntity();

            _context.Groups.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Ошибка добавления в базу данных.");
            }
        }

        public async Task<GroupDataModel> GetGroup(int groupId)
        {
            Group entity = await _context.Groups.Include(o => o.StudentsInGroups).AsNoTracking().FirstOrDefaultAsync(e => e.ID == groupId);

            GroupDataModel group = entity is null ? new GroupDataModel() : new GroupDataModel(entity);

            return group;
        }

        public async Task UpdateGroup(GroupDataModel group)
        {
            Group entity = group.FillToEntity();

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Ошибка обновления данных");
            }
        }

        public async Task DeleteGroup(int groupId)
        {
            Group group = await _context.Groups.FindAsync(groupId);

            _context.Groups.Remove(group);

            await _context.SaveChangesAsync();
        }
    }
}
