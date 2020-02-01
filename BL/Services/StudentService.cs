﻿using BL.Interfaces;
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
    public class StudentService : IStudentService
    {
        private readonly TestContext _context;

        public StudentService(TestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDataModel>> GetStudentList()
        {
            List<StudentDataModel> studentList = (from e in await _context.Students.Include(o => o.StudentsInGroups).AsNoTracking().ToListAsync() select e)
                .Select(e => new StudentDataModel(e)).OrderBy(e => e.FullName).ToList();

            return studentList;
        }

        public async Task<IEnumerable<StudentDataModel>> GetStudentListForGroup(int groupId)
        {
            List<Student> studentEntityList = (from e in await _context.Students.Include(i => i.StudentsInGroups).AsNoTracking().ToListAsync() select e)
                .Where(i => i.StudentsInGroups.Any(s => s.GroupId == groupId)).ToList();

            List<StudentDataModel> studentList = studentEntityList.Select(e => new StudentDataModel(e)).OrderBy(e => e.FullName).ToList();

            return studentList;
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

            StudentDataModel student = entity is null ? null : new StudentDataModel(entity);

            return student;
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
