using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManApi.Models;

namespace StudentManApi.Models
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }
        //需添加出生日期birthday
        [Required]
        [MaxLength (50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Nation NationBelong { get; set; }
        [Required]
        public Sex SexBelong { get; set; }
        public string Describe { get; set; }
        public List<MyClass> MyClasses { get; set; } = new List<MyClass>();
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
