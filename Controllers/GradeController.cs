using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManApi.Dtos;
using StudentManApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Controllers
{
    [Route("Api/Student/{studentId}/[controller]")]
    [ApiController]//用于数据验证返回错误值
    public class GradeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStuRepository _stuRepository;
        public GradeController(IMapper mapper, IStuRepository stuRepository)
        {
            _mapper = mapper;
            _stuRepository = stuRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetGradeByStudentId([FromRoute] Guid studentId)
        {
            if (!await _stuRepository.StudentExistsAsync(studentId))
            {
                return NotFound("该学生不存在");
            }
            var GradeFromRepo = await _stuRepository.GetGradesBystudentIdAsync(studentId);
            return Ok(_mapper.Map<IEnumerable<GradeDto>>(GradeFromRepo));  
        }
    }
}
