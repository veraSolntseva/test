using BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.ViewModels
{
    public class GroupViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(25, ErrorMessage = "Не более 25 символов")]
        public string Name { get; set; }

        public int StudentsCount { get; }


        public GroupViewModel() { }

        public GroupViewModel(GroupDataModel groupDataModel)
        {
            this.ID = groupDataModel.ID;
            this.Name = groupDataModel.Name;
            this.StudentsCount = groupDataModel.StudentsCount;
        }

        public GroupDataModel GetDataModel()
        {
            return new GroupDataModel()
            {
                ID = this.ID,
                Name = this.Name
            };
        }
    }
}
