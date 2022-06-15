using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Dtos
{
    public class TeacherRegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password),ErrorMessage ="密码输出不一致")]
        public string ComfirmPassword { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Profession { get; set; }
    }
}
