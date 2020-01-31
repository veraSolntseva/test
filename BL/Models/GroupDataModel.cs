using DAL.DbObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Models
{
    public class GroupDataModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int StudentsCount { get; set; }


        public GroupDataModel() { }

        public GroupDataModel(Group group)
        {
            this.ID = group.ID;
            this.Name = group.Name;
            this.StudentsCount = group.StudentsInGroups.Count;
        }

        public Group FillToEntity()
        {
            return new Group()
            {
                ID = this.ID,
                Name = this.Name
            };
        }

    }
}
