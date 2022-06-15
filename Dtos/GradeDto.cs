using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Dtos
{
    public class GradeDto
    {
        public int Unit { get; set; }
        [Range(0, 200)]
        public double Math { get; set; }
        [Range(0, 200)]
        public double English { get; set; }
        [Range(0, 200)]
        public double Chinese { get; set; }
        [Range(0, 200)]
        public double Physical { get; set; }
        [ForeignKey("StudentId")]
        public Guid StudentId { get; set; }
    }
}
