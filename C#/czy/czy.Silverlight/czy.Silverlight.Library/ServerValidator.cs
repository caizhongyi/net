
//详细的说明在代码中给出。
//注意：用VS调试时，要在菜单Debug->Exception...，去掉Common Language Runtime Exception前面的勾，不然会报错。
 
//1）业务类Employees.cs文件：
//using System;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
 
//namespace SilverlightClient
//{
//    public class Employees: INotifyPropertyChanged
//    {
//        private int _EmployeeID;
//        private string _EmployeeName;
//        private int _EmployeeAge;
//        private string _EmployeeTelephone;
 
//        [Required(ErrorMessage="该字段为必填！")]
//        [CustomValidation(typeof(EmployeeValidation), "IsValidID")]//这里用到了自定义验证，自定义验证类为EmployeeValidation.cs
//        public int EmployeeID
//        {
//            get
//            {
//                return _EmployeeID;
//            }
//            set
//            {
//                if (value != _EmployeeID)
//                {
//                    _EmployeeID = value;
//                    OnPropertyChanged("EmployeeID");
//                }
//            }
//        }
 
//        [Required(ErrorMessage = "该字段为必填！")]
//        public string EmployeeName
//        {
//            get
//            {
//                return _EmployeeName;
//            }
//            set
//            {
//                if (value != _EmployeeName)
//                {
//                    _EmployeeName = value;
//                    OnPropertyChanged("EmployeeName");
//                }
//            }
//        }
 
//        [Required(ErrorMessage = "该字段为必填！")]
//        [Range(18,60,ErrorMessage = "输入的年龄范围有误(18-60)！\n请重新填写。")]
//        public int EmployeeAge
//        {
//            get
//            {
//                return _EmployeeAge;
//            }
//            set
//            {
//                if (value != _EmployeeAge)
//                {
//                    _EmployeeAge = value;
//                    OnPropertyChanged("EmployeeAge");
//                }
//            }
//        }
 
//        [Required(ErrorMessage = "该字段为必填！")]
//        [RegularExpression(@"^\(\d\d\d\) \d\d\d\d\d\d\d\d$",ErrorMessage = "输入的电话格式不正确！\n请按(###) ########格式匹配输入。")]
//        public string EmployeeTelephone
//        {
//            get
//            {
//                return _EmployeeTelephone;
//            }
//            set
//            {
//                if (value != _EmployeeTelephone)
//                {
//                    //用VS调试时，要在菜单Debug->Exception...，去掉Common Language Runtime Exception前面的勾，不然会报错。
//                    Validator.ValidateProperty(value, new ValidationContext(this,null,null) { MemberName = "EmployeeTelephone" });//跳出提示框显示出错原因
//                    _EmployeeTelephone = value;
//                    OnPropertyChanged("EmployeeTelephone");
//                }
//            }
//        }
 
//        #region INotifyPropertyChanged Members
 
//        public event PropertyChangedEventHandler PropertyChanged;
 
//        protected virtual void OnPropertyChanged(string propName)
//        {
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs(propName));
//            }
//        }
 
//        #endregion
//    }
//}
 
//2）自定义验证类EmployeeValidation.cs文件：
//using System.ComponentModel.DataAnnotations;
 
//namespace SilverlightClient
//{
//    public class EmployeeValidation
//    {
//        public static ValidationResult IsValidID(int employeeid)
//        {
//            bool isValid;
 
//            if (employeeid > 0)
//                isValid = true;
//            else
//                isValid = false;
 
//            if (isValid)
//            {
//                return ValidationResult.Success;
//            }
//            else
//            {
//                return new ValidationResult("输入的工号无效！");
//            }
//        }
 
//    }
//}
 
//3）MainPage.xaml文件
//<UserControl
//    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
//    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//    xmlns:d="http://schemas.microsoft.com/expression/blend/2008" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
//    mc:Ignorable="d" xmlns:data="clr-namespace:System.Windows.Controls;assembly=System.Windows.Controls.Data" x:Class="SilverlightClient.MainPage"
//    d:DesignWidth="320" d:DesignHeight="240">
//    <Grid x:Name="LayoutRoot" Background="White" Width="320" Height="240">
//        <data:DataGrid x:Name="dgEmployee" Margin="8" AutoGenerateColumns="False" FontSize="14">
//            <data:DataGrid.Columns>
//                <data:DataGridTextColumn Header="工号" Binding="{Binding EmployeeID}"/>
//                <data:DataGridTextColumn Header="姓名" Binding="{Binding EmployeeName}"/>
//                <data:DataGridTextColumn Header="年龄" Binding="{Binding EmployeeAge}"/>
//                <data:DataGridTextColumn Header="电话" Binding="{Binding EmployeeTelephone}"/>
//            </data:DataGrid.Columns>
//        </data:DataGrid>
//    </Grid>
//</UserControl>
 
//4）MainPage.xaml.cs文件
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;
//using System.Windows.Data;
 
//namespace SilverlightClient
//{
//    public partial class MainPage : UserControl
//    {
//        public List<Employees> GetEmployees()
//        {
//            List<Employees> returnedValue = new List<Employees>();
//            returnedValue.Add(new Employees() { EmployeeID = 1, EmployeeName = "张三", EmployeeAge = 23, EmployeeTelephone = "(001) 56891243" });
//            returnedValue.Add(new Employees() { EmployeeID = 2, EmployeeName = "李四", EmployeeAge = 24, EmployeeTelephone = "(002) 67453210" });
//            returnedValue.Add(new Employees() { EmployeeID = 3, EmployeeName = "王五", EmployeeAge = 25, EmployeeTelephone = "(010) 46782312" });
//            returnedValue.Add(new Employees() { EmployeeID = 4, EmployeeName = "赵六", EmployeeAge = 26, EmployeeTelephone = "(011) 78564254" });
//            returnedValue.Add(new Employees() { EmployeeID = 5, EmployeeName = "钱七", EmployeeAge = 27, EmployeeTelephone = "(020) 87465676" });
//            returnedValue.Add(new Employees() { EmployeeID = 6, EmployeeName = "孙八", EmployeeAge = 28, EmployeeTelephone = "(030) 97321687" });
//            return returnedValue;
//        }
 
//        public MainPage()
//        {
//            InitializeComponent();
//            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
//        }
 
//        void MainPage_Loaded(object sender, RoutedEventArgs e)
//        {
//            dgEmployee.ItemsSource = GetEmployees();
//        }
//    }
//}

