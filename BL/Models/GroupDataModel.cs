using DAL.DbObjects;

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
