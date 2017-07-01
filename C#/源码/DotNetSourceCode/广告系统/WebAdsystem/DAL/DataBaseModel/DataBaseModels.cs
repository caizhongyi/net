using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataBaseModel
{
    public class Ad_WBInfo
    {
        public int WB_ID;
        public int Ad_ID;
    }

    public class AdTypeInfo
    {
        public int Ad_Type_ID;
        public string Ad_Type_Name;
        public string Ad_Type_Remark;
    }

    public class UserInfoInfo
    {
        public int User_ID;
        public string User_Name;
        public string Login_Pwd;
        public string Login_Name;
        public string User_Register_Time;
        public int User_Right;
        public string User_Remark;
    }

    public class WBInfoInfo
    {
        public int WB_ID;
        public string WB_Name;
        public string WB_IP;
        public string WB_Register_Time;
        public string WB_Address;
        public string WB_Phone;
        public string WB_Remark;
    }

    public class ClientInfoInfo
    {
        public int Client_ID;
        public string Client_Name;
        public string Client_Sex;
        public string Client_Phone;
        public string Client_Address;
        public string Client_Register_Time;
        public string Client_Remark;
    }

    public class WB_ExtensionInfo
    {
        public int WB_C_ID;
        public string WB_C_IP;
        public int WB_ID;
        public string WB_C_Register_Time;
        public string WB_C_Remark;
    }

    public class AdInfoInfo
    {
        public int Ad_ID;
        public int Ad_Type_Name;
        public string Ad_Name;
        public string Ad_Url;
        public int Ad_ClickNum;
        public string Ad_Remark;
        public string Ad_Operation;
        public string Ad_time;
    }

    public class Client_WbInfo
    {
        public string Client_ID;
        public string Wb_id;
    }

    public class sysdiagramsInfo
    {
        public string name;
        public int principal_id;
        public int diagram_id;
        public int version;
        public byte[] definition;
    }
}
