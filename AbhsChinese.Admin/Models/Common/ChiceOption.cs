using System.ComponentModel.DataAnnotations;

namespace AbhsChinese.Admin.Models.Common
{
    public class ChiceOption
    {
        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }
}