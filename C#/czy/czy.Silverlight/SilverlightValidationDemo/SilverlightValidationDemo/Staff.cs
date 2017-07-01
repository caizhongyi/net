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
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SilverlightValidationDemo
{
    public class Staff : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region data memebers
        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
                ValidateUserNameandPasswordAsync(value);
            }
        }

        #endregion

        #region Constructor
        public Staff()
        {
            _validationErrors = new Dictionary<string, ObservableCollection<string>>();
            GenerateErrorsCollection("UserName");
        }
        #endregion

        #region private methods
        private void GenerateErrorsCollection(string propertyName)
        {
            if (!_validationErrors.ContainsKey(propertyName))
            {
                _validationErrors.Add(propertyName, new ObservableCollection<string>());
            }
        }

        private void ValidateUserNameandPasswordAsync(string username)
        {
            var client = new ValidationService.ValidationServiceClient();
            client.ValidationUserNameCompleted += (o, e) =>
            {
                _validationErrors["UserName"].Clear();

                if (e.Result)
                {
                    _username = username;
                    NotifyPropertyChanged("UserName");
                }
                else
                {
                    _validationErrors["UserName"].Add("服务器端返回错误，用户名必须是jv9");

                }
                if (ErrorsChanged != null)
                {
                    ErrorsChanged(this, new DataErrorsChangedEventArgs("UserName"));
                }
            };

            client.ValidationUserNameAsync(username);
        }
        #endregion
        

        #region INotifyDataErrorInfo Members
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private Dictionary<string, ObservableCollection<string>> _validationErrors;
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_validationErrors.ContainsKey(propertyName))
                    return _validationErrors[propertyName];
                else
                    return null;
            }
            else
                return null;
        }

        public bool HasErrors
        {
            get 
            {
                foreach (string key in _validationErrors.Keys)
                {
                    if (_validationErrors[key].Count > 0)
                        return true;
                }
                return false;
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

        
    }
}
