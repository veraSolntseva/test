using System.ComponentModel.DataAnnotations;

namespace BL
{
    public class GenderEnum
    {
        public enum Gender
        {
            [Display(Name = "Мужской")]
            Male = 1,

            [Display(Name = "Женский")]
            Famale = 2
        }
    }
}
