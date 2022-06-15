using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi.Models
{
    public class MyClass
    {
        [ForeignKey("StudentId")]
        public Guid StudentId { get; set; }
        [ForeignKey("TeacherId")]
        public int TeacherId { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
