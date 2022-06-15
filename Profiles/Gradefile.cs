using AutoMapper;
using StudentManApi.Dtos;
using StudentManApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Profiles
{
    public class Gradefile : Profile
    {
        public Gradefile()
        {
            CreateMap<Grade,GradeDto>();
            CreateMap<GradeCreateDto, Grade>();
        }
    }
}
