using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.ViewModels;

namespace Test.Controllers
{
    //[Authorize(AuthenticationSchemes =
    //JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public GroupController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> Index(string groupName)
        {
            List<GroupViewModel> groupViewList = new List<GroupViewModel>();

            try
            {
                IEnumerable<GroupDataModel> groupList = await _groupService.GetGroupList();

                groupViewList = groupList.Select(x => new GroupViewModel(x)).ToList();
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            if (groupViewList != null && !string.IsNullOrEmpty(groupName))
                groupViewList = groupViewList.Where(i => i.Name.Contains(groupName)).ToList();

            JsonResult result = Json(groupViewList);

            return result;
        }

        [HttpGet("[action]/{id}")]
        public async Task<JsonResult> Get(int id)
        {
            GroupDataModel group = await _groupService.GetGroup(id);

            if (group is null)
                return Json(new { error = "Группа не найдена" });

            return Json(new GroupViewModel(group));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody]GroupViewModel group)
        {
            if (!ModelState.IsValid)
                return BadRequest("Некорректные данные");

            try
            {
                await _groupService.AddGroup(group.GetDataModel());
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
                await _groupService.DeleteGroup(id);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit([FromBody]GroupViewModel group)
        {
            if (!ModelState.IsValid || group.ID < 1)
                return BadRequest("Некорректные данные");

            try
            {
                await _groupService.UpdateGroup(group.GetDataModel());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<JsonResult> GetStudentListForGroup(int groupId)
        {
            List<StudentViewModel> studentViewList = new List<StudentViewModel>();

            try
            {
                IEnumerable<StudentDataModel> studentList = await _groupService.GetStudentListForGroup(groupId);

                studentViewList = studentList.Select(x => new StudentViewModel(x)).ToList();
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            JsonResult result = Json(studentViewList?.Select(s => new { id = s.Id, fullName = s.FullName, uniqId = s.UniqueName }).ToList());

            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddStudentsToGroup(int studentId, int groupId)
        {
            try
            {
                await _groupService.AddStudentToGroup(studentId, groupId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteStudentFromGroup(int studentId, int groupId)
        {
            try
            {
                await _groupService.RemoveStudentFromGroup(studentId, groupId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

    }
}