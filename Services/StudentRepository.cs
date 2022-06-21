using Microsoft.EntityFrameworkCore;
using StudentManApi.Dtos;
using StudentManApi.Helper;
using StudentManApi.Models;
using StudentManApi.ResourceParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi.Services
{
    public class StudentRepository : IStuRepository
    {
        private readonly  SchoolDbcontext _context;
        IPropertyMappingService _propertyMappingService;
        public StudentRepository(SchoolDbcontext context, IPropertyMappingService propertyMappingService)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
        }
        //获取某个单元学生的成绩
        public async Task<Grade> GetGradeAsync(int unit, Guid studentId)
        {
            return await _context.Grades.FirstOrDefaultAsync(c => c.Unit == unit && c.StudentId == studentId);
        }
        //获得某个学生的成绩
        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(Guid studentId)
        {
            return await  _context.Grades.Where(c => c.StudentId == studentId).ToListAsync();
        }
        //查到一个学生,并将其成绩也查出来
        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await  _context.Students.Include(t=>t.Grades).FirstOrDefaultAsync(c => c.StudentId == studentId);
        }
        //根据姓名查出某些学生
        public async Task<PagedList<Student>> GetStudentsAsync(StudentParameter studentParameter)
        {
            if (studentParameter==null)
            {
                throw new ArgumentNullException(nameof(studentParameter));
            }

            IQueryable<Student> result = _context.Students.Include(t => t.Grades);
            //if (string.IsNullOrWhiteSpace(studentParameter.Keyword)&& string.IsNullOrWhiteSpace(studentParameter.AddressBelong))
            //{
            //    return await result.ToListAsync();
            //}
            if (!string.IsNullOrWhiteSpace(studentParameter.Keyword))
            {
                studentParameter.Keyword.Trim();
                result = result.Where(t => t.Name.Contains(studentParameter.Keyword));
            }
            if (!string.IsNullOrWhiteSpace(studentParameter.AddressBelong))
            {
                studentParameter.AddressBelong.Trim();
                result = result.Where(t => t.Address== studentParameter.AddressBelong);
            }
            //result = result.Skip(studentParameter.PageSize * (studentParameter.PageNumber - 1))//第几页,就跳过该页所有的数据
            //.Take(studentParameter.PageSize);
            //------------------------------------------------------------------------------
            //if (!string.IsNullOrWhiteSpace(studentParameter.SortBy))
            //{
            //    if (studentParameter.SortBy.ToLowerInvariant()== "nationbelong")
            //    {
            //        result = result.OrderBy(c => c.NationBelong);
            //    }
            //}
            var mappingDictionary = _propertyMappingService.GetPropertyMapping<StudentDto,Student>();
            result= result.ApplySort(studentParameter.SortBy,mappingDictionary);
            return await PagedList<Student>.CreateAsync(result, studentParameter.PageNumber, studentParameter.PageSize);
        }

        //判断某个学生是否存在
        public async Task<bool> StudentExistsAsync(Guid studentId)
        {
            return await _context.Students.AnyAsync(c => c.StudentId == studentId);
        }

        //增
        public async Task CreateStudentAsync(Student student) 
        {
            if (student==null)
            {
                throw new ArgumentException(nameof(student));
            }
            await _context.Students.AddAsync(student);
        }
        //保存
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        //删除
        public void DeleteStu(Student student)
        {
            _context.Students.Remove(student);
        }
        //查到某个教师所有学生的信息

        //定义某个学生成为一个老师的学生
        public async Task BeSomeOneStudebntAsync(MyClass myClass)
        {
          await  _context.MyClasses.AddAsync(myClass);
        }

        public async Task<bool> ClassesExistsAsync(Guid studentId, int teacherId)
        {
            return await _context.MyClasses.AnyAsync(c => c.StudentId == studentId&&c.TeacherId==teacherId);
        }
        //定义找到某个关系
        public async Task<MyClass> FindRelationshipAsync(Guid studentId, int teacherId)
        {
            return await  _context.MyClasses.FirstOrDefaultAsync(c => c.StudentId == studentId && c.TeacherId == teacherId);
        }
        public void DeleteClass(MyClass myClass)
        {
            _context.MyClasses.Remove(myClass);
        }
        public Task<IEnumerable<Student>> GetStudentsAsync(string keyword, int teacherId)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int TeacherId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Grade>> GetGradesBystudentIdAsync(Guid studentId)
        {
            return await _context.Grades.Where(c => c.StudentId == studentId).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentIdList(IEnumerable<Guid> guids)
        {
            return await _context.Students.Where(c=> guids.Contains(c.StudentId)).ToListAsync();
        }

        public void DeleteStudents(IEnumerable<Student> students)
        {
            _context.Students.RemoveRange(students);
        }
    }
}
