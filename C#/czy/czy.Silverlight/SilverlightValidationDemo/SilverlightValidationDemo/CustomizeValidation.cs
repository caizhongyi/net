using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.DataAnnotations;

namespace SilverlightValidationDemo
{
    public class CustomizeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            String checkName = value.ToString();

            return checkName == "jv9" ? ValidationResult.Success : new ValidationResult("请使用指定用户名");
        }
    }
}
