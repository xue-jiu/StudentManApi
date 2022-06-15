using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi.Models
{
    public class Teacher:IdentityUser
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string Profession { get; set; }
        public List<MyClass> MyClasses { get; set; } = new List<MyClass>();
        //外键关系 导航
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    }
}
