using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.Tools;
using Test.ViewModels;

namespace Test.Controllers
{
    //[Authorize(AuthenticationSchemes =
    //JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }


        [HttpGet("[action]")]
        public async Task<JsonResult> Index(StudentFilter filter)
        {
            List<StudentViewModel> studentViewList = new List<StudentViewModel>();

            try
            {
                IEnumerable<StudentDataModel> studentList = await _studentService.GetStudentList();

                IEnumerable<GroupDataModel> groupList = await _groupService.GetGroupList();

                studentViewList = studentList.Select(x => new StudentViewModel(x, groupList.Where(i => x.GroupsIdList.Contains(i.ID)))).ToList();
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            if (studentViewList != null)
                studentViewList = filter.FilterList(studentViewList);

            JsonResult result = Json(studentViewList?.Select(s => new { id = s.Id, fullName = s.FullName, uniqId = s.UniqueName, groups = String.Join(',', s.GroupList.Select(i => i.Name)) }).ToList());

            return result;
        }

        [HttpGet("[action]/{id}")]
        public async Task<JsonResult> Get(int id)
        {
            StudentDataModel student = await _studentService.GetStudent(id);

            if (student is null)
                return Json(new { error = "Студент не найден" });

            IEnumerable<GroupDataModel> groupList = await _groupService.GetGroupListForStudent(id);

            return Json(new StudentViewModel(student, groupList));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody]StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Некорректные данные");

            if (!(await _studentService.CheckUniqueName(student.UniqueName)))
                return BadRequest("Такой уникальный идентификатор уже существует");

            try
            {
                await _studentService.AddStudent(student.GetDataModel());
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit([FromBody]StudentViewModel student)
        {
            if (!ModelState.IsValid || student.Id < 1)
                return BadRequest("Некорректные данные");

            if (!(await _studentService.CheckUniqueName(student.UniqueName)))
                return BadRequest("Такой уникальный идентификатор уже существует");

            try
            {
                await _studentService.UpdateStudent(student.GetDataModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}