using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManApi.Models
{
    public class Grade
    {
        public int Unit { get; set; }
        public double Math { get; set; }
        public double English { get; set; }
        public double Chinese { get; set; }
        public double Physical { get; set; }
        [ForeignKey("StudentId")]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
