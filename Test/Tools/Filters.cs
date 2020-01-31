using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.ViewModels;

namespace Test.Tools
{
    public class Paginator
    {
        public int? PageNumber { get; set; }

        public int? StudentsCountAtPage { get; set; }
    }

    public class StudentFilter
    {
        public int? GenderID { get; set; }

        public string FullName { get; set; }

        public string UniqName { get; set; }

        public string GroupName { get; set; }

        public List<StudentViewModel> FilterList(List<StudentViewModel> studentList)
        {
            if (GenderID.HasValue)
                studentList = studentList.Where(s => s.GenderId == this.GenderID).ToList();

            if (!string.IsNullOrEmpty(this.FullName))
                studentList = studentList.Where(s => s.FullName == this.FullName).ToList();

            if(!string.IsNullOrEmpty(this.UniqName))
                studentList = studentList.Where(s => s.UniqueName == this.UniqName).ToList();

            if(!string.IsNullOrEmpty(this.GroupName))
                studentList = studentList.Where(s => s.GroupList.Any(i => i.Name == this.GroupName)).ToList();

            return studentList;
        }
    }
}
