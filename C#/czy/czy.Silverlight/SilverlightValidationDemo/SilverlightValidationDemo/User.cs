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
using System.ComponentModel;
using System.Collections.Generic;

namespace SilverlightValidationDemo
{
    public class User : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _name;
        [CustomizeValidation]
        public string Name
        {
            get { return _name; }
            set 
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Name" });
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("用户名不能为空.");
                }
                _name = value; 
            }
        }

        private string _password;
        [StringLength(6, ErrorMessage="密码不能超过6个字符")]
        public string password
        {
            get { return _password; }
            set 
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "password" });
                _password = value; 
            }
        }

        private int _age;
        [Range(0, 100, ErrorMessage = "请输入年龄值在0 - 100之间")]
        public int Age
        {
            get { return _age; }
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Age" });
                _age = value;
            }
        }

        private string _email;
        [Required(ErrorMessage = "必填选项")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$",ErrorMessage="请输入正确的Email格式")]
        public string email
        {
            get { return _email; }
            set 
            {
                var tmpValidator = new ValidationContext(this, null, null);
                tmpValidator.MemberName = "email";
                Validator.ValidateProperty(value, tmpValidator);
                _email = value; 
            }
        }

        private string _address;
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _dataErrors["address"] = "地址必须填写";
                else if (value.Trim().Length < 6)
                    _dataErrors["address"] = "地址至少6个字";
                else
                    if (_dataErrors.ContainsKey("address"))
                        _dataErrors.Remove("address");

                _address = value;
                NotifyPropertyChanged("address");
            }
        }

        private string _phone;
        public string phone
        {
            get { return _phone; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    _dataErrors["phone"] = "电话必须填写";
                else if (value.Trim().Length < 6)
                    _dataErrors["phone"] = "电话至少11位";
                else
                    if (_dataErrors.ContainsKey("phone"))
                        _dataErrors.Remove("phone");

                _phone = value;
                NotifyPropertyChanged("phone");
            }
        }

        private string _gradelevel;
        public string gradelevel
        {
            get { return _gradelevel; }
            set
            {
                if (ValidateGradeLevelandRange(value,graderange))
                {
                    _gradelevel = value;
                    NotifyPropertyChanged("gradelevel");
                }
            }
        }

        private decimal _graderange;
        public decimal graderange
        {
            get { return _graderange; }
            set
            {
                if (ValidateGradeLevelandRange(gradelevel, value))
                {
                    _graderange = value;
                    NotifyPropertyChanged("graderange");
                }
            }
        }

        public User()
        {
        }

        #region IDataErrorInfo Members

        private string _dataError = string.Empty;
        public string Error
        {
            get { return _dataError; }
        }

        private Dictionary<string, string> _dataErrors = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get 
            {
                if (_dataErrors.ContainsKey(columnName))
                    return _dataErrors[columnName];
                else
                    return null;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Customize Validation
        private bool ValidateGradeLevelandRange(string level, decimal range)
        {
            bool isValid = false;

            if (level == null)
            {
                _dataErrors["gradelevel"] = "成绩等级必须是A，B，C，D，F";
                return false;
            }
            else
            {
                switch (level.ToUpper())
                {
                    case "A":
                        isValid = (range >= 90 && range <= 100);
                        break;
                    case "B":
                        isValid = (range >= 80 && range <= 89);
                        break;
                    case "C":
                        isValid = (range >= 70 && range <= 79);
                        break;
                    case "D":
                        isValid = (range >= 60 && range <= 69);
                        break;
                    case "F":
                        isValid = (range >= 0 && range <= 59);
                        break;
                    default:
                        _dataErrors["gradelevel"] = "成绩等级必须是A，B，C，D，F";
                        return false;
                }
            }

            if (isValid)
            {
                if (_dataErrors.ContainsKey("gradelevel"))
                    _dataErrors.Remove("gradelevel");
                if (_dataErrors.ContainsKey("graderange"))
                    _dataErrors.Remove("graderange");
            }
            else
            {
                _dataErrors["gradelevel"] = "成绩等级和成绩范围不符合";
                _dataErrors["graderange"] = "成绩范围和成绩等级不符合";
            }

            return isValid;
        }
        #endregion
        
    }
}



