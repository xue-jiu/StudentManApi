using StudentManApi.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi.Dtos
{
    [ValidatableCreateDto]
    public class StudentCreateDto: IValidatableObject
    {
        private string name;
        private string telephone;
        private string address;
        [Required(ErrorMessage ="必须要有名字")]
        [MaxLength(50)]
        public string Name { get => name; set { name = value;} }
        [Required]
        [MaxLength(50)]
        public string Telephone { get => telephone; set { telephone = value;} }
        [Required]
        [MaxLength(50)]
        public string Address { get => address; set { address = value;} }
        public string SexBelong { get; set; }
        [MaxLength(500)]
        public string Describe { get; set; }
        public List<GradeCreateDto> Grades { get; set; } = new List<GradeCreateDto>();

        //正对单个属性的验证
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsContainSpace(Address))
            {
                yield return new ValidationResult("地址不能含有空格", new[] { nameof(Address) });//第二个参数提示哪个位置出错
            }
        }
        public bool IsContainSpace(string value)
        {
            if (value != null && value.ToString().Contains(" "))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
