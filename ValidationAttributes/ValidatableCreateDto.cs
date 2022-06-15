﻿using StudentManApi.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManApi.ValidationAttributes
{
    public class ValidatableCreateDto : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var student = (StudentCreateDto)validationContext.ObjectInstance;
            Regex regex = new Regex(@"^[\u4e00-\u9fa5]{1,}$");//不包含特殊字符
            Match match = regex.Match(student.Name);
            if (!match.Success)
            {
                return new ValidationResult("名字中不能含有特殊字符",new[] { nameof(student.Name) });
            }
            return ValidationResult.Success;
        }
    }
}
