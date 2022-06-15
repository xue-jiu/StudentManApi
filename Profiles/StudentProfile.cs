using AutoMapper;
using StudentManApi.Dtos;
using StudentManApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Profiles
{
    public class StudentProfile:Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember
                (
                    dest => dest.SexBelong,
                    opt => opt.MapFrom(src => src.SexBelong.ToString())
                )
                .ForMember
                (
                    dest => dest.NationBelong,
                    opt=>opt.MapFrom(src=>src.NationBelong.ToString())
                );

            CreateMap<StudentCreateDto, Student>().ForMember(
                    dest => dest.StudentId,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                    );
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<Student, StudentUpdateDto>();
        }
    }
}
