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
    public class StudentService : IStudentService
    {
        private readonly TestContext _context;

        public StudentService(TestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDataModel>> GetStudentList()
        {
            List<Student> studentList = await _context.Students.Include(o => o.StudentsInGroups).AsNoTracking().ToListAsync();

            List<Group> groupList = await _context.Groups.Include(o => o.StudentsInGroups).AsNoTracking().ToListAsync();

            List<StudentDataModel> studentDataList = studentList.Select(s => new StudentDataModel(s, groupList.Where(g => s.StudentsInGroups.Any(i => i.GroupId == g.ID)))).ToList();

            return studentDataList;
        }

        public async Task AddStudent(StudentDataModel student)
        {
            Student entity = student.FillToEntity();

            entity.Id = 0;

            _context.Students.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Ошибка добавления в базу данных.");
            }
        }

        public async Task<StudentDataModel> GetStudent(int studentId)
        {
            Student entity = await _context.Students.Include(o => o.StudentsInGroups).AsNoTracking().FirstOrDefaultAsync(e => e.Id == studentId);

            if (entity is null)
                return null;

            List<Group> groupList = await _context.Groups.Include(o => o.StudentsInGroups).Where(g => entity.StudentsInGroups.Any(i => i.GroupId == g.ID)).AsNoTracking().ToListAsync();

            return new StudentDataModel(entity, groupList);
        }

        public async Task UpdateStudent(StudentDataModel student)
        {
            if (!(_context.Students.Any(i => i.Id == student.Id)))
                throw new Exception("Студент не найден");

            Student entity = student.FillToEntity();

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

        public async Task DeleteStudent(int id)
        {
            Student student = await _context.Students.FindAsync(id);

            _context.Students.Remove(student);

            List<StudentsInGroups> studentsInGroups = await _context.StudentsInGroups.Where(i => i.StudentId == id).ToListAsync();

            _context.StudentsInGroups.RemoveRange(studentsInGroups);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUniqueName(string uniqueName)
        {
            List<Student> studentList = await _context.Students.AsNoTracking().ToListAsync();

            return !studentList.Any(s => s.UniqueName == uniqueName);
        }
    }
}
