using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using StudentManApi.API.Helper;
using StudentManApi.Dtos;
using StudentManApi.Helpers;
using StudentManApi.Models;
using StudentManApi.ResourceParameter;
using StudentManApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentManApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]//用于数据验证返回错误值
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStuRepository _stuRepository;
        //IUrlHelper _urlHelper;
        public StudentController(IMapper mapper, IStuRepository stuRepository)
        {
            _mapper = mapper;
            _stuRepository = stuRepository;
            //_urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }
        //获得指定学生
        [HttpGet("{studentId}",Name = "GetStudentRoute")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
        {
            if (!await _stuRepository.StudentExistsAsync(studentId))
            {
                return NotFound("查无此人");
            }
            var studentFromRepo =await _stuRepository.GetStudentAsync(studentId);
            return Ok(_mapper.Map<StudentDto>(studentFromRepo));
        }
        //关键字查询某个学生
        [HttpGet(Name =nameof(GetStudents))]
        public async Task<IActionResult> GetStudents([FromQuery] StudentParameter studentParameter)
        {
            var studentsFromRepo = await _stuRepository.GetStudentsAsync(studentParameter);
            //前后导航
            var previouLink = studentsFromRepo.HasPrevious ? CreateStudentUri(studentParameter, ResourceUriType.PreviousPage):null;
            var nextLink = studentsFromRepo.HasNext ? CreateStudentUri(studentParameter, ResourceUriType.NextPage) : null;
            var paginationMetadata = new
            {
                totalCount = studentsFromRepo.TotalItemCount,
                pageSize = studentsFromRepo.PageSize,
                currentPage = studentsFromRepo.CurrentPage,
                TotalPage = studentsFromRepo.TotalPage,
                previouLink,
                nextLink
            };
            Response.Headers.Add("paginationMetadata", JsonSerializer.Serialize(paginationMetadata));
            if (studentsFromRepo == null || !studentsFromRepo.Any())
            {
                return NotFound("没有符合该特征的学生");
            }
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(studentsFromRepo));
        }
        //增
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDto studentCreateDto)
        {
            //此处studentdto是否合法的判断交给数据验证[apicontroller]
            var studentModel = _mapper.Map<Student>(studentCreateDto);
            await _stuRepository.CreateStudentAsync(studentModel);
            _stuRepository.Save();
            var studentToReturn = _mapper.Map<StudentDto>(studentModel);
            //可以返回ok 200,也可以如下返回create 201,可以在返回头中多加入一条url
            return CreatedAtRoute(
                //routename
                "GetStudentRoute",
                //routeValues
                new { studentId = studentToReturn.StudentId},
                //ObjectValue
                studentToReturn
                );
        }
        //整体修改
        [HttpPut("{studentId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId, [FromBody] StudentUpdateDto studentUpdateDto)
        {
            if (!await _stuRepository.StudentExistsAsync(studentId))
            {
                return NotFound("查无此人");
            }
            var studentFromRepo =await _stuRepository.GetStudentAsync(studentId);
            _mapper.Map(studentUpdateDto, studentFromRepo);
            _stuRepository.Save();
            return NoContent();//操作成功执行,响应正文为空
        }

        [HttpPatch("{studentId}")]//无法使用modelstate进行数据验证
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> PartiallyUpdateStudent([FromRoute] Guid studentId, [FromBody]JsonPatchDocument<StudentUpdateDto> patchDocument)
        {
            if (!await _stuRepository.StudentExistsAsync(studentId))
            {
                return NotFound("查无此人");
            }
            var studentFromRepo = await _stuRepository.GetStudentAsync(studentId);
            var studentPatch = _mapper.Map<StudentUpdateDto>(studentFromRepo);
            patchDocument.ApplyTo(studentPatch, ModelState);//第二个参数ModelState是为了验证patchDocument，如果有问题就会携带错误，到下方
            if (!TryValidateModel(studentPatch))
            {
                //return BadRequest();//400
                return ValidationProblem(ModelState);//根据某弹幕ValidationProblem返回就是badrequest400，为啥不是422？因为不是同一个库中的验证
            }
            _mapper.Map(studentPatch, studentFromRepo);
            _stuRepository.Save();
            return NoContent();
        }

        [HttpDelete("{studentId}")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
        {
            if (!await _stuRepository.StudentExistsAsync(studentId))
            {
                return NotFound("查无此人");
            }
            var StudentFromRepo= await _stuRepository.GetStudentAsync(studentId);
            _stuRepository.DeleteStu(StudentFromRepo);
            _stuRepository.Save();
            return NoContent();
        }

        [HttpDelete("({studentIds})")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteStudents([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> studentIds)
        {
            if (studentIds==null)
            {
                return BadRequest();
            }
             var studentFromRepo=await _stuRepository.GetStudentIdList(studentIds);
            _stuRepository.DeleteStudents(studentFromRepo);
            _stuRepository.Save();
            return NoContent();
        }

//        public override ActionResult ValidationProblem(ModelStateDictionary modelStateDictionary)//重写ValidationProblem，将400改成422
//        {
//            var options = HttpContext.RequestServices.GetService<IOptions<ApiBehaviorOptions>>();
//;        }

        private string CreateStudentUri(StudentParameter studentParameter,ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetStudents), new
                    {
                        pageNumber = studentParameter.PageNumber-1,
                        pageSize=studentParameter.PageSize,
                        Keyword= studentParameter.Keyword,
                        Address=studentParameter.AddressBelong
                    });

                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetStudents), new//controllerbase自带urlhelper
                    {
                        pageNumber = studentParameter.PageNumber + 1,
                        pageSize = studentParameter.PageSize,
                        Keyword = studentParameter.Keyword,
                        Address = studentParameter.AddressBelong
                    });

                default:
                    return Url.Link(nameof(GetStudents), new
                    {
                        pageNumber = studentParameter.PageNumber,
                        pageSize = studentParameter.PageSize,
                        Keyword = studentParameter.Keyword,
                        Address = studentParameter.AddressBelong
                    });


            }
        }
    }
}
