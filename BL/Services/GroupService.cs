using BL.Interfaces;
using BL.Models;
using DAL;
using DAL.DbObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<StudentDataModel>> GetStudentListForGroup(int groupId)
        {
            List<Student> studentEntityList = await _context.Students.Include(o => o.StudentsInGroups).AsNoTracking().ToListAsync();

            studentEntityList = studentEntityList.Where(s => s.StudentsInGroups.Any(i => i.GroupId == groupId)).ToList();

            List<StudentDataModel> studentList = studentEntityList.Select(e => new StudentDataModel(e)).ToList();

            return studentList;
        }

        public async Task AddGroup(GroupDataModel group)
        {
            Group entity = group.FillToEntity();

            entity.ID = 0;

            try
            {
                _context.Groups.Add(entity);

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

            if (entity is null)
                return null;

            return new GroupDataModel(entity);
        }

        public async Task UpdateGroup(GroupDataModel group)
        {
            if (!(_context.Groups.Any(i => i.ID == group.ID)))
                throw new Exception("Группа не найдена");

            Group entity = group.FillToEntity();

            try
            {
                _context.Entry(entity).State = EntityState.Modified;

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

            List<StudentsInGroups> studentsInGroups = await _context.StudentsInGroups.Where(i => i.GroupId == groupId).ToListAsync();

            _context.StudentsInGroups.RemoveRange(studentsInGroups);

            await _context.SaveChangesAsync();
        }

        public async Task AddStudentToGroup(int studentId, int groupId)
        {
            Student student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);

            Group group = await _context.Groups.FirstOrDefaultAsync(g => g.ID == groupId);

            if (student is null || group is null)
                throw new Exception("Некорректные данные");

            StudentsInGroups studentsInGroups = new StudentsInGroups()
            {
                GroupId = groupId,
                StudentId = studentId
            };

            try
            {
                _context.StudentsInGroups.Add(studentsInGroups);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Ошибка добавления в базу данных.");
            }
        }

        public async Task RemoveStudentFromGroup(int studentId, int groupId)
        {
            if (!(await _context.StudentsInGroups.FirstOrDefaultAsync(i => i.GroupId == groupId && i.StudentId == studentId) is StudentsInGroups entity))
                throw new Exception("Некорректные данные");

            try
            {
                _context.StudentsInGroups.Remove(entity);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
