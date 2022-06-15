using StudentManApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManApi.Dtos
{
    public class StudentDto
    {
        [Key]
        public Guid StudentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string NationBelong { get; set; }
        [Required]
        public string SexBelong { get; set; }
        public string Describe { get; set; }
        //public List<MyClass> MyClasses { get; set; } = new List<MyClass>();
        public List<GradeDto> Grades { get; set; } = new List<GradeDto>();
    }
}
