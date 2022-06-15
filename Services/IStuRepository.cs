using StudentManApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManApi.Services
{
    public interface IStuRepository
    {
        //查
        Task<IEnumerable<Student>>GetStudentsAsync(string keyword,string address);
        Task<IEnumerable<Student>> GetStudentsAsync(string keyword, int teacherId);
        Task<IEnumerable<Student>> GetStudentsByTeacherIdAsync(int TeacherId);
        Task<Student> GetStudentAsync(Guid studentId);
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(Guid studentId);
        Task<Grade> GetGradeAsync(int unit, Guid studentId);
        Task BeSomeOneStudebntAsync(MyClass myClass);
        Task<MyClass> FindRelationshipAsync(Guid studentId, int teacherId);

        Task<IEnumerable<Student>> GetStudentIdList(IEnumerable<Guid> guids);
        //grade部分
        Task<IEnumerable<Grade>> GetGradesBystudentIdAsync(Guid studentId);

        //增
        Task CreateStudentAsync(Student student);
        //删
        void DeleteStu(Student student);
        void DeleteStudents(IEnumerable<Student> students);
        void DeleteClass(MyClass myClass);
        //保存,更新
        public bool Save();
        //判断存在与否
        Task<bool> StudentExistsAsync(Guid studentId);
        Task<bool> ClassesExistsAsync(Guid studentId,int teacherId);
    }
}
