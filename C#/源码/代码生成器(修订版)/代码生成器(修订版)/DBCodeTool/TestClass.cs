using System;
using System.Collections.Generic;
using System.Text;

namespace DBCodeTool
    {
    public class Tsm_EventsInfo
    {
       public int64 EventID;
       public string EventType;
       public string Events;
       public string OperateTime;
       public string EventSource;
       public string UserCode;
       public string ComputerName;
       public string ComputerIP;
       public string OperateType;
    }

    public class Tss_customerordersInfo
    {
       public string billno;
       public string customizedbillno;
       public string customerno;
       public string employeeno;
       public string issuedate;
       public string issueuser;
       public string deliverydate;
       public string deliveryterm;
       public string paymentterm;
       public float othercharge;
       public float grandtotal;
       public bool verifyresult;
       public bool hassigncontract;
       public bool hascanceled;
       public string contractno;
       public string signdate;
       public string SignAddress;
       public int contractaccessoryid;
       public bool contracthaspassed;
       public string remarks;
       public int flag;
       public int InjunctionFlag;
    }

    public class Twh_billnoTempInfo
    {
       public string BillNo;
       public string CreateDate;
       public string Flag;
    }

    public class Tbd_customersInfo
    {
       public string CustomerNo;
       public string Name;
       public string FullName;
       public string Country;
       public string Province;
       public string City;
       public string Address;
       public string Bank;
       public string Account;
       public string Corporation;
       public string TaxpayingAccount;
       public string Contact;
       public string Tel;
       public string Fax;
       public string PostCode;
       public string E-mail;
       public string Http;
       public string DeliverDestination;
       public string Remarks;
       public int ID;
    }

    public class Tbd_dictionaryInfo
    {
       public int Id;
       public string Value;
       public string Identifier;
    }

    public class Tbd_enumerationsInfo
    {
       public int Id;
       public string Identifier;
       public string Description;
       public bool CustomerDefine;
       public string Remarks;
    }

    public class Tbd_suppliersInfo
    {
       public string SupplierNo;
       public string Name;
       public string FullName;
       public string Kind;
       public string Country;
       public string Province;
       public string City;
       public string Address;
       public string Bank;
       public string Account;
       public string Corporation;
       public string TaxpayingAccount;
       public string Contact;
       public string Tel;
       public string Fax;
       public string PostCode;
       public string E-mail;
       public string Http;
       public string Remarks;
       public int ID;
    }

    public class Tbd_CompOrgInfo
    {
       public string ObjectID;
       public string ObjectName;
       public string Description;
       public string Type;
       public string ParentID;
       public string Parent;
       public int ImageID;
    }

    public class Tbd_regionsInfo
    {
       public string RegionNo;
       public string Name;
       public string PostalCode;
       public string ZoneNo;
       public int ID;
       public int PID;
       public string CustomCode;
    }

    public class Tsm_OrdersPowersInfo
    {
       public string EmployeeNo;
       public string StartDate;
       public string EndDate;
       public string StatusDate;
       public int ID;
    }

    public class Tbd_warehousesInfo
    {
       public int64 id;
       public int64 pid;
       public string WhNo;
       public string CustomizedNo;
       public string Description;
       public string Location;
       public bool SupplierMark;
       public string SupplierNo;
       public byte MngIdentifier;
       public string Type;
       public string remarks;
       public byte ImageID;
    }

    public class TWh_Stock_AlertlineInfo
    {
       public string WarehouseNo;
       public string ItemNo;
       public double SecurityStock;
       public double ResupplyStock;
       public double TopStock;
       public double InitStock;
       public string InitStockDate;
       public double CurrentStock;
       public double OccupiedStock;
       public double EOQ;
    }

    public class Tbd_goodsInfo
    {
       public string ItemNo;
       public string ChartNo;
       public string Description;
       public string Specification;
       public string Unit;
       public string MakeSign;
       public string Kind;
       public string Status;
       public int AdvancingPeriod;
       public double Batch;
       public double OutputRate;
       public string Remarks;
       public int ID;
       public string CustomizedNo;
       public string GoodsKindNo;
    }

    public class Tsm_BackupLogInfo
    {
       public int ID;
       public string BackupTime;
       public string BackupType;
       public string BackupName;
       public string BackupDescription;
       public string BackupFile;
    }

    public class Tbd_OrdersMaterialInfo
    {
       public string MaterialNo;
       public int Id;
       public string MaterialName;
       public byte[] Material;
    }

    public class Tss_SendItemsInfo
    {
       public string BillNo;
       public string ItemNo;
       public float Price;
       public double SendQty;
       public double Discount;
       public float ItemTotal;
       public string OriginalBill;
       public string DeliveryDate;
       public string RealDlvDate;
       public double OutStockQty;
       public int Id;
       public bool HasBuildInvoice;
       public string MaterialName;
    }

    public class Tbd_cardboardCalculateInfo
    {
       public int Id;
       public string No;
       public string Name;
       public string Description;
       public string Specification;
       public string CardboardBroadExpressions;
       public string CardboardLengthExpressions;
       public bool Status;
       public string Remarks;
       public string Syncopate;
    }

    public class TAs_WBEvaluationsInfo
    {
       public string BillNo;
       public string AppointedEvaluationDept;
       public string AppointedEvaluationUser;
       public bool IsDeptEvaluation;
       public int EvaluationSequence;
       public int CurrentEvaluationStep;
       public string EvaluationUser;
       public string EvaluationDate;
       public bool EvaluationRecord;
       public string Reason;
       public bool HasEvaluated;
       public int Id;
    }

    public class Twh_wastebook_masterInfo
    {
       public string billno;
       public string iosign;
       public string iokindno;
       public string IssueDate;
       public string IssueUser;
       public string ViaUser;
       public string RoomNo;
       public bool HasCheckOut;
       public string Remarks;
       public string customizedbillno;
    }

    public class Twh_wastebook_detailInfo
    {
       public string billno;
       public string RoomNo;
       public string ItemNo;
       public double Quantity;
       public string OriginalBill;
       public bool hascheckout;
       public string Remarks;
       public int ID;
       public string IOsign;
    }

    public class Tbd_ioKindsInfo
    {
       public string IOKindNo;
       public string Description;
       public string Identifier;
       public string MasterTableName;
       public string DetailTableName;
    }

    public class TSs_CoEvaluationsInfo
    {
       public string Billno;
       public string AppointedEvaluationDept;
       public string AppointedEvaluationUser;
       public bool IsDeptEvaluation;
       public int EvaluationSequence;
       public int CurrentEvaluationStep;
       public string EvaluationUser;
       public string EvaluationDate;
       public bool EvaluationRecord;
       public string Reason;
       public bool HasEvaluated;
       public int Id;
    }

    public class TWs_PbcompositorInfo
    {
       public string BillNo;
       public string CustomizedBillNo;
       public string IssueDate;
       public string IssueUser;
       public bool VerifyResult;
       public string Remarks;
       public int Id;
    }

    public class Tbd_UsersPosInfo
    {
       public string EmployeeNo;
       public string DeptNo;
    }

    public class Tss_coitemsInfo
    {
       public string billno;
       public string itemno;
       public double quantity;
       public float price;
       public double discount;
       public float itemtotal;
       public string deliverydate;
       public double deliverytotal;
       public string remarks;
       public int id;
       public string MaterialName;
    }

    public class TWs_InjunctionInfo
    {
       public string BillNo;
       public string AcceptDeptNo;
       public string AcceptDate;
       public string InjunctionTime;
       public string Proceeding;
       public string Content;
       public string Require;
       public string AssignDeptNo;
       public string AssignUser;
       public string IssueUser;
       public string IssueDate;
       public string Remarks;
       public int id;
       public string AcceptUser;
    }

    public class Tbd_FreightCompaniesInfo
    {
       public string FreightCompanyNo;
       public string FullName;
       public string ShortName;
       public string Address;
       public string Contact;
       public string Tel;
       public string Fax;
       public string PostCode;
       public string E-mail;
       public string Http;
       public string Remarks;
       public int ID;
       public string CarMark;
    }

    public class TAs_WorkBillInfo
    {
       public string BillNo;
       public string CustomizedBillNo;
       public string CustomerNo;
       public string IssueDate;
       public string IndentDate;
       public string IssueUser;
       public string Molding;
       public string Printing;
       public string exceptive;
       public string CheckNo;
       public string OriginalBill;
       public int flag;
       public string Remarks;
       public bool VerifyResult;
    }

    public class Tss_ReturnInfo
    {
       public string BillNo;
       public string CustomizedBillNo;
       public string CustomerNo;
       public string IssueDate;
       public string IssueUser;
       public string Remarks;
    }

    public class TAs_WBItemsInfo
    {
       public string BillNo;
       public string ItemNo;
       public string MaterialName;
       public string exceptive;
       public double PaperLength;
       public double PaperBroad;
       public double PaperHigh;
       public double PaperFrontHigh;
       public string BoxModel;
       public string Syncopate;
       public double CardboardFactBroad;
       public double CardboardFactLength;
       public double CardboardBroad;
       public double CardboardLength;
       public double BoxQuantity;
       public double CardboardQuantity;
       public string OriginalBill;
       public string DeliveryDate;
       public string Remarks;
       public string PressLine;
       public double BoxFactQuantity;
       public string CalculateExpressions;
       public double CardboardFactQuantity;
    }

    public class Tss_SendsInfo
    {
       public string BillNo;
       public string CustomizedBillNo;
       public string FreightCompanyNo;
       public string SendKind;
       public string CustomerNo;
       public string IssueDate;
       public string IssueUser;
       public string SendUser;
       public string DeliveryTerm;
       public string Remarks;
       public bool HasBuildInvoice;
    }

    public class TWs_CBCItemsInfo
    {
       public string BillNo;
       public int Sequence;
       public string OriginalBill;
       public string ItemNo;
       public string exceptive;
       public string Remarks;
       public int ID;
       public string CustomerNo;
    }

    public class TWs_CBcompositorInfo
    {
       public string BillNo;
       public string customizedbillno;
       public string IssueDate;
       public string IssueUser;
       public bool VerifyResult;
       public string Remarks;
       public int ID;
    }

    public class TWs_PBCItemsInfo
    {
       public string BillNo;
       public int Sequence;
       public string OriginalBill;
       public string ItemNo;
       public string exceptive;
       public string Remarks;
       public int id;
       public string CustomerNo;
    }

    public class TWs_PBCEvaluationsInfo
    {
       public string BillNo;
       public string AppointedEvaluationDept;
       public string AppointedEvaluationUser;
       public bool IsDeptEvaluation;
       public int EvaluationSequence;
       public int CurrentEvaluationStep;
       public string EvaluationUser;
       public string EvaluationDate;
       public bool EvaluationRecord;
       public string Reason;
       public bool HasEvaluated;
       public int id;
    }

    public class TWs_CBCEvaluationsInfo
    {
       public string BillNo;
       public string AppointedEvaluationDept;
       public string AppointedEvaluationUser;
       public bool IsDeptEvaluation;
       public int EvaluationSequence;
       public int CurrentEvaluationStep;
       public string EvaluationUser;
       public string EvaluationDate;
       public string Reason;
       public string Remarks;
       public bool EvaluationRecord;
       public bool HasEvaluated;
       public int Id;
    }

    public class Tss_ReturnItemsInfo
    {
       public string BillNo;
       public string ItemNo;
       public float Price;
       public double ReturnQty;
       public double Discount;
       public float ItemTotal;
       public string OriginalBill;
       public bool HasChecked;
       public string Reason;
       public double DiscardQty;
       public string CheckUser;
       public string CheckDate;
       public double EligibleQty;
       public double RecruitQty;
       public string Remarks;
       public int ID;
       public double RejectedQty;
       public double EnterStockQty;
       public string MaterialName;
    }

    public class Tbd_goodskindsInfo
    {
       public string GoodsKindNo;
       public string Name;
       public string Description;
       public int ID;
       public int PID;
       public string CustomCode;
    }

    public class Tbd_employeesInfo
    {
       public string EmployeeNo;
       public string LoginName;
       public string Name;
       public string Password;
       public string CreateTime;
       public string Sex;
       public string Birth;
       public string FamilyAddr;
       public string NativePlace;
       public string IDCard;
       public string PostCode;
       public string LinkAddr;
       public string HomePhone;
       public string WorkPhone;
       public string Mobile;
       public string Email;
       public string OtherLink;
       public string HireDate;
       public string FireDate;
       public string EduLevel;
       public bool IsAdmin;
       public bool Status;
       public byte[] Photo;
       public string Remark;
    }

    public class Tsm_PowerInfo
    {
       public string Type;
       public string ModuleName;
       public string Description;
    }

    public class Tsm_RolePowerInfo
    {
       public string RoleName;
       public string ModuleName;
       public bool R_Insert;
       public bool R_Update;
       public bool R_Delete;
       public bool R_PrivateBrowse;
       public bool R_PublicBrowse;
       public bool R_Execute;
       public string SuccessiveRole;
    }

    public class Tsm_RolesInfo
    {
       public string RoleName;
       public string Description;
    }

    public class Tsm_UserPowerInfo
    {
       public string EmployeeNo;
       public string ModuleName;
       public bool R_Insert;
       public bool R_Update;
       public bool R_Delete;
       public bool R_Execute;
       public bool R_PrivateBrowse;
       public bool R_PublicBrowse;
       public string SuccessiveRole;
    }

    public class Tsm_UserVsRoleInfo
    {
       public string EmployeeNo;
       public string RoleName;
    }

    public class TdtpropertiesInfo
    {
       public int id;
       public int objectid;
       public string property;
       public string value;
       public string uvalue;
       public byte[] lvalue;
       public int version;
    }

    /****************** 下面是 Tsm_Events 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_Events
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:05
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_Events:Ism_Events
    {
       TSqlHelp db = null;
       public Tsm_Events()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_EventsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_Events(Tsm_EventsInfo obj) //添加
       {
           //
       }

       public void Updatesm_Events(Tsm_EventsInfo obj) //修改
       {
           //
       }

       public void Delsm_Events(Tsm_EventsInfo obj) //删除
       {
           //
       }

       public void Updetesm_Events(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tss_customerorders 类 ********************/
    /// <实体类摘要>
    /// 类名：Tss_customerorders
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:05
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tss_customerorders:Iss_customerorders
    {
       TSqlHelp db = null;
       public Tss_customerorders()
       {
           db = new TSqlHelp();   }

       public DataTable getss_customerordersList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addss_customerorders(Tss_customerordersInfo obj) //添加
       {
           //
       }

       public void Updatess_customerorders(Tss_customerordersInfo obj) //修改
       {
           //
       }

       public void Delss_customerorders(Tss_customerordersInfo obj) //删除
       {
           //
       }

       public void Updetess_customerorders(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Twh_billnoTemp 类 ********************/
    /// <实体类摘要>
    /// 类名：Twh_billnoTemp
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:05
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Twh_billnoTemp:Iwh_billnoTemp
    {
       TSqlHelp db = null;
       public Twh_billnoTemp()
       {
           db = new TSqlHelp();   }

       public DataTable getwh_billnoTempList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addwh_billnoTemp(Twh_billnoTempInfo obj) //添加
       {
           //
       }

       public void Updatewh_billnoTemp(Twh_billnoTempInfo obj) //修改
       {
           //
       }

       public void Delwh_billnoTemp(Twh_billnoTempInfo obj) //删除
       {
           //
       }

       public void Updetewh_billnoTemp(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_customers 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_customers
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:06
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_customers:Ibd_customers
    {
       TSqlHelp db = null;
       public Tbd_customers()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_customersList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_customers(Tbd_customersInfo obj) //添加
       {
           //
       }

       public void Updatebd_customers(Tbd_customersInfo obj) //修改
       {
           //
       }

       public void Delbd_customers(Tbd_customersInfo obj) //删除
       {
           //
       }

       public void Updetebd_customers(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_dictionary 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_dictionary
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:06
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_dictionary:Ibd_dictionary
    {
       TSqlHelp db = null;
       public Tbd_dictionary()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_dictionaryList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_dictionary(Tbd_dictionaryInfo obj) //添加
       {
           //
       }

       public void Updatebd_dictionary(Tbd_dictionaryInfo obj) //修改
       {
           //
       }

       public void Delbd_dictionary(Tbd_dictionaryInfo obj) //删除
       {
           //
       }

       public void Updetebd_dictionary(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_enumerations 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_enumerations
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:06
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_enumerations:Ibd_enumerations
    {
       TSqlHelp db = null;
       public Tbd_enumerations()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_enumerationsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_enumerations(Tbd_enumerationsInfo obj) //添加
       {
           //
       }

       public void Updatebd_enumerations(Tbd_enumerationsInfo obj) //修改
       {
           //
       }

       public void Delbd_enumerations(Tbd_enumerationsInfo obj) //删除
       {
           //
       }

       public void Updetebd_enumerations(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_suppliers 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_suppliers
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:06
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_suppliers:Ibd_suppliers
    {
       TSqlHelp db = null;
       public Tbd_suppliers()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_suppliersList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_suppliers(Tbd_suppliersInfo obj) //添加
       {
           //
       }

       public void Updatebd_suppliers(Tbd_suppliersInfo obj) //修改
       {
           //
       }

       public void Delbd_suppliers(Tbd_suppliersInfo obj) //删除
       {
           //
       }

       public void Updetebd_suppliers(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_CompOrg 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_CompOrg
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:07
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_CompOrg:Ibd_CompOrg
    {
       TSqlHelp db = null;
       public Tbd_CompOrg()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_CompOrgList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_CompOrg(Tbd_CompOrgInfo obj) //添加
       {
           //
       }

       public void Updatebd_CompOrg(Tbd_CompOrgInfo obj) //修改
       {
           //
       }

       public void Delbd_CompOrg(Tbd_CompOrgInfo obj) //删除
       {
           //
       }

       public void Updetebd_CompOrg(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_regions 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_regions
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:07
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_regions:Ibd_regions
    {
       TSqlHelp db = null;
       public Tbd_regions()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_regionsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_regions(Tbd_regionsInfo obj) //添加
       {
           //
       }

       public void Updatebd_regions(Tbd_regionsInfo obj) //修改
       {
           //
       }

       public void Delbd_regions(Tbd_regionsInfo obj) //删除
       {
           //
       }

       public void Updetebd_regions(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_OrdersPowers 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_OrdersPowers
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:07
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_OrdersPowers:Ism_OrdersPowers
    {
       TSqlHelp db = null;
       public Tsm_OrdersPowers()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_OrdersPowersList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_OrdersPowers(Tsm_OrdersPowersInfo obj) //添加
       {
           //
       }

       public void Updatesm_OrdersPowers(Tsm_OrdersPowersInfo obj) //修改
       {
           //
       }

       public void Delsm_OrdersPowers(Tsm_OrdersPowersInfo obj) //删除
       {
           //
       }

       public void Updetesm_OrdersPowers(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_warehouses 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_warehouses
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:07
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_warehouses:Ibd_warehouses
    {
       TSqlHelp db = null;
       public Tbd_warehouses()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_warehousesList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_warehouses(Tbd_warehousesInfo obj) //添加
       {
           //
       }

       public void Updatebd_warehouses(Tbd_warehousesInfo obj) //修改
       {
           //
       }

       public void Delbd_warehouses(Tbd_warehousesInfo obj) //删除
       {
           //
       }

       public void Updetebd_warehouses(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWh_Stock_Alertline 类 ********************/
    /// <实体类摘要>
    /// 类名：TWh_Stock_Alertline
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:08
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWh_Stock_Alertline:IWh_Stock_Alertline
    {
       TSqlHelp db = null;
       public TWh_Stock_Alertline()
       {
           db = new TSqlHelp();   }

       public DataTable getWh_Stock_AlertlineList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWh_Stock_Alertline(TWh_Stock_AlertlineInfo obj) //添加
       {
           //
       }

       public void UpdateWh_Stock_Alertline(TWh_Stock_AlertlineInfo obj) //修改
       {
           //
       }

       public void DelWh_Stock_Alertline(TWh_Stock_AlertlineInfo obj) //删除
       {
           //
       }

       public void UpdeteWh_Stock_Alertline(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_goods 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_goods
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:08
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_goods:Ibd_goods
    {
       TSqlHelp db = null;
       public Tbd_goods()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_goodsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_goods(Tbd_goodsInfo obj) //添加
       {
           //
       }

       public void Updatebd_goods(Tbd_goodsInfo obj) //修改
       {
           //
       }

       public void Delbd_goods(Tbd_goodsInfo obj) //删除
       {
           //
       }

       public void Updetebd_goods(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_BackupLog 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_BackupLog
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:08
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_BackupLog:Ism_BackupLog
    {
       TSqlHelp db = null;
       public Tsm_BackupLog()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_BackupLogList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_BackupLog(Tsm_BackupLogInfo obj) //添加
       {
           //
       }

       public void Updatesm_BackupLog(Tsm_BackupLogInfo obj) //修改
       {
           //
       }

       public void Delsm_BackupLog(Tsm_BackupLogInfo obj) //删除
       {
           //
       }

       public void Updetesm_BackupLog(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_OrdersMaterial 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_OrdersMaterial
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:08
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_OrdersMaterial:Ibd_OrdersMaterial
    {
       TSqlHelp db = null;
       public Tbd_OrdersMaterial()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_OrdersMaterialList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_OrdersMaterial(Tbd_OrdersMaterialInfo obj) //添加
       {
           //
       }

       public void Updatebd_OrdersMaterial(Tbd_OrdersMaterialInfo obj) //修改
       {
           //
       }

       public void Delbd_OrdersMaterial(Tbd_OrdersMaterialInfo obj) //删除
       {
           //
       }

       public void Updetebd_OrdersMaterial(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tss_SendItems 类 ********************/
    /// <实体类摘要>
    /// 类名：Tss_SendItems
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:09
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tss_SendItems:Iss_SendItems
    {
       TSqlHelp db = null;
       public Tss_SendItems()
       {
           db = new TSqlHelp();   }

       public DataTable getss_SendItemsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addss_SendItems(Tss_SendItemsInfo obj) //添加
       {
           //
       }

       public void Updatess_SendItems(Tss_SendItemsInfo obj) //修改
       {
           //
       }

       public void Delss_SendItems(Tss_SendItemsInfo obj) //删除
       {
           //
       }

       public void Updetess_SendItems(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_cardboardCalculate 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_cardboardCalculate
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:09
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_cardboardCalculate:Ibd_cardboardCalculate
    {
       TSqlHelp db = null;
       public Tbd_cardboardCalculate()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_cardboardCalculateList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_cardboardCalculate(Tbd_cardboardCalculateInfo obj) //添加
       {
           //
       }

       public void Updatebd_cardboardCalculate(Tbd_cardboardCalculateInfo obj) //修改
       {
           //
       }

       public void Delbd_cardboardCalculate(Tbd_cardboardCalculateInfo obj) //删除
       {
           //
       }

       public void Updetebd_cardboardCalculate(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TAs_WBEvaluations 类 ********************/
    /// <实体类摘要>
    /// 类名：TAs_WBEvaluations
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:09
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TAs_WBEvaluations:IAs_WBEvaluations
    {
       TSqlHelp db = null;
       public TAs_WBEvaluations()
       {
           db = new TSqlHelp();   }

       public DataTable getAs_WBEvaluationsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddAs_WBEvaluations(TAs_WBEvaluationsInfo obj) //添加
       {
           //
       }

       public void UpdateAs_WBEvaluations(TAs_WBEvaluationsInfo obj) //修改
       {
           //
       }

       public void DelAs_WBEvaluations(TAs_WBEvaluationsInfo obj) //删除
       {
           //
       }

       public void UpdeteAs_WBEvaluations(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Twh_wastebook_master 类 ********************/
    /// <实体类摘要>
    /// 类名：Twh_wastebook_master
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:09
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Twh_wastebook_master:Iwh_wastebook_master
    {
       TSqlHelp db = null;
       public Twh_wastebook_master()
       {
           db = new TSqlHelp();   }

       public DataTable getwh_wastebook_masterList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addwh_wastebook_master(Twh_wastebook_masterInfo obj) //添加
       {
           //
       }

       public void Updatewh_wastebook_master(Twh_wastebook_masterInfo obj) //修改
       {
           //
       }

       public void Delwh_wastebook_master(Twh_wastebook_masterInfo obj) //删除
       {
           //
       }

       public void Updetewh_wastebook_master(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Twh_wastebook_detail 类 ********************/
    /// <实体类摘要>
    /// 类名：Twh_wastebook_detail
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:10
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Twh_wastebook_detail:Iwh_wastebook_detail
    {
       TSqlHelp db = null;
       public Twh_wastebook_detail()
       {
           db = new TSqlHelp();   }

       public DataTable getwh_wastebook_detailList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addwh_wastebook_detail(Twh_wastebook_detailInfo obj) //添加
       {
           //
       }

       public void Updatewh_wastebook_detail(Twh_wastebook_detailInfo obj) //修改
       {
           //
       }

       public void Delwh_wastebook_detail(Twh_wastebook_detailInfo obj) //删除
       {
           //
       }

       public void Updetewh_wastebook_detail(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_ioKinds 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_ioKinds
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:10
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_ioKinds:Ibd_ioKinds
    {
       TSqlHelp db = null;
       public Tbd_ioKinds()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_ioKindsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_ioKinds(Tbd_ioKindsInfo obj) //添加
       {
           //
       }

       public void Updatebd_ioKinds(Tbd_ioKindsInfo obj) //修改
       {
           //
       }

       public void Delbd_ioKinds(Tbd_ioKindsInfo obj) //删除
       {
           //
       }

       public void Updetebd_ioKinds(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TSs_CoEvaluations 类 ********************/
    /// <实体类摘要>
    /// 类名：TSs_CoEvaluations
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:10
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TSs_CoEvaluations:ISs_CoEvaluations
    {
       TSqlHelp db = null;
       public TSs_CoEvaluations()
       {
           db = new TSqlHelp();   }

       public DataTable getSs_CoEvaluationsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddSs_CoEvaluations(TSs_CoEvaluationsInfo obj) //添加
       {
           //
       }

       public void UpdateSs_CoEvaluations(TSs_CoEvaluationsInfo obj) //修改
       {
           //
       }

       public void DelSs_CoEvaluations(TSs_CoEvaluationsInfo obj) //删除
       {
           //
       }

       public void UpdeteSs_CoEvaluations(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_Pbcompositor 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_Pbcompositor
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:10
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_Pbcompositor:IWs_Pbcompositor
    {
       TSqlHelp db = null;
       public TWs_Pbcompositor()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_PbcompositorList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_Pbcompositor(TWs_PbcompositorInfo obj) //添加
       {
           //
       }

       public void UpdateWs_Pbcompositor(TWs_PbcompositorInfo obj) //修改
       {
           //
       }

       public void DelWs_Pbcompositor(TWs_PbcompositorInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_Pbcompositor(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_UsersPos 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_UsersPos
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:10
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_UsersPos:Ibd_UsersPos
    {
       TSqlHelp db = null;
       public Tbd_UsersPos()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_UsersPosList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_UsersPos(Tbd_UsersPosInfo obj) //添加
       {
           //
       }

       public void Updatebd_UsersPos(Tbd_UsersPosInfo obj) //修改
       {
           //
       }

       public void Delbd_UsersPos(Tbd_UsersPosInfo obj) //删除
       {
           //
       }

       public void Updetebd_UsersPos(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tss_coitems 类 ********************/
    /// <实体类摘要>
    /// 类名：Tss_coitems
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:11
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tss_coitems:Iss_coitems
    {
       TSqlHelp db = null;
       public Tss_coitems()
       {
           db = new TSqlHelp();   }

       public DataTable getss_coitemsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addss_coitems(Tss_coitemsInfo obj) //添加
       {
           //
       }

       public void Updatess_coitems(Tss_coitemsInfo obj) //修改
       {
           //
       }

       public void Delss_coitems(Tss_coitemsInfo obj) //删除
       {
           //
       }

       public void Updetess_coitems(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_Injunction 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_Injunction
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:11
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_Injunction:IWs_Injunction
    {
       TSqlHelp db = null;
       public TWs_Injunction()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_InjunctionList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_Injunction(TWs_InjunctionInfo obj) //添加
       {
           //
       }

       public void UpdateWs_Injunction(TWs_InjunctionInfo obj) //修改
       {
           //
       }

       public void DelWs_Injunction(TWs_InjunctionInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_Injunction(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_FreightCompanies 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_FreightCompanies
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:11
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_FreightCompanies:Ibd_FreightCompanies
    {
       TSqlHelp db = null;
       public Tbd_FreightCompanies()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_FreightCompaniesList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_FreightCompanies(Tbd_FreightCompaniesInfo obj) //添加
       {
           //
       }

       public void Updatebd_FreightCompanies(Tbd_FreightCompaniesInfo obj) //修改
       {
           //
       }

       public void Delbd_FreightCompanies(Tbd_FreightCompaniesInfo obj) //删除
       {
           //
       }

       public void Updetebd_FreightCompanies(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TAs_WorkBill 类 ********************/
    /// <实体类摘要>
    /// 类名：TAs_WorkBill
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:12
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TAs_WorkBill:IAs_WorkBill
    {
       TSqlHelp db = null;
       public TAs_WorkBill()
       {
           db = new TSqlHelp();   }

       public DataTable getAs_WorkBillList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddAs_WorkBill(TAs_WorkBillInfo obj) //添加
       {
           //
       }

       public void UpdateAs_WorkBill(TAs_WorkBillInfo obj) //修改
       {
           //
       }

       public void DelAs_WorkBill(TAs_WorkBillInfo obj) //删除
       {
           //
       }

       public void UpdeteAs_WorkBill(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tss_Return 类 ********************/
    /// <实体类摘要>
    /// 类名：Tss_Return
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:12
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tss_Return:Iss_Return
    {
       TSqlHelp db = null;
       public Tss_Return()
       {
           db = new TSqlHelp();   }

       public DataTable getss_ReturnList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addss_Return(Tss_ReturnInfo obj) //添加
       {
           //
       }

       public void Updatess_Return(Tss_ReturnInfo obj) //修改
       {
           //
       }

       public void Delss_Return(Tss_ReturnInfo obj) //删除
       {
           //
       }

       public void Updetess_Return(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TAs_WBItems 类 ********************/
    /// <实体类摘要>
    /// 类名：TAs_WBItems
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:12
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TAs_WBItems:IAs_WBItems
    {
       TSqlHelp db = null;
       public TAs_WBItems()
       {
           db = new TSqlHelp();   }

       public DataTable getAs_WBItemsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddAs_WBItems(TAs_WBItemsInfo obj) //添加
       {
           //
       }

       public void UpdateAs_WBItems(TAs_WBItemsInfo obj) //修改
       {
           //
       }

       public void DelAs_WBItems(TAs_WBItemsInfo obj) //删除
       {
           //
       }

       public void UpdeteAs_WBItems(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tss_Sends 类 ********************/
    /// <实体类摘要>
    /// 类名：Tss_Sends
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:12
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tss_Sends:Iss_Sends
    {
       TSqlHelp db = null;
       public Tss_Sends()
       {
           db = new TSqlHelp();   }

       public DataTable getss_SendsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addss_Sends(Tss_SendsInfo obj) //添加
       {
           //
       }

       public void Updatess_Sends(Tss_SendsInfo obj) //修改
       {
           //
       }

       public void Delss_Sends(Tss_SendsInfo obj) //删除
       {
           //
       }

       public void Updetess_Sends(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_CBCItems 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_CBCItems
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:13
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_CBCItems:IWs_CBCItems
    {
       TSqlHelp db = null;
       public TWs_CBCItems()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_CBCItemsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_CBCItems(TWs_CBCItemsInfo obj) //添加
       {
           //
       }

       public void UpdateWs_CBCItems(TWs_CBCItemsInfo obj) //修改
       {
           //
       }

       public void DelWs_CBCItems(TWs_CBCItemsInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_CBCItems(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_CBcompositor 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_CBcompositor
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:13
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_CBcompositor:IWs_CBcompositor
    {
       TSqlHelp db = null;
       public TWs_CBcompositor()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_CBcompositorList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_CBcompositor(TWs_CBcompositorInfo obj) //添加
       {
           //
       }

       public void UpdateWs_CBcompositor(TWs_CBcompositorInfo obj) //修改
       {
           //
       }

       public void DelWs_CBcompositor(TWs_CBcompositorInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_CBcompositor(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_PBCItems 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_PBCItems
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:13
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_PBCItems:IWs_PBCItems
    {
       TSqlHelp db = null;
       public TWs_PBCItems()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_PBCItemsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_PBCItems(TWs_PBCItemsInfo obj) //添加
       {
           //
       }

       public void UpdateWs_PBCItems(TWs_PBCItemsInfo obj) //修改
       {
           //
       }

       public void DelWs_PBCItems(TWs_PBCItemsInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_PBCItems(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_PBCEvaluations 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_PBCEvaluations
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:13
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_PBCEvaluations:IWs_PBCEvaluations
    {
       TSqlHelp db = null;
       public TWs_PBCEvaluations()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_PBCEvaluationsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_PBCEvaluations(TWs_PBCEvaluationsInfo obj) //添加
       {
           //
       }

       public void UpdateWs_PBCEvaluations(TWs_PBCEvaluationsInfo obj) //修改
       {
           //
       }

       public void DelWs_PBCEvaluations(TWs_PBCEvaluationsInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_PBCEvaluations(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 TWs_CBCEvaluations 类 ********************/
    /// <实体类摘要>
    /// 类名：TWs_CBCEvaluations
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:14
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class TWs_CBCEvaluations:IWs_CBCEvaluations
    {
       TSqlHelp db = null;
       public TWs_CBCEvaluations()
       {
           db = new TSqlHelp();   }

       public DataTable getWs_CBCEvaluationsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void AddWs_CBCEvaluations(TWs_CBCEvaluationsInfo obj) //添加
       {
           //
       }

       public void UpdateWs_CBCEvaluations(TWs_CBCEvaluationsInfo obj) //修改
       {
           //
       }

       public void DelWs_CBCEvaluations(TWs_CBCEvaluationsInfo obj) //删除
       {
           //
       }

       public void UpdeteWs_CBCEvaluations(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tss_ReturnItems 类 ********************/
    /// <实体类摘要>
    /// 类名：Tss_ReturnItems
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:14
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tss_ReturnItems:Iss_ReturnItems
    {
       TSqlHelp db = null;
       public Tss_ReturnItems()
       {
           db = new TSqlHelp();   }

       public DataTable getss_ReturnItemsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addss_ReturnItems(Tss_ReturnItemsInfo obj) //添加
       {
           //
       }

       public void Updatess_ReturnItems(Tss_ReturnItemsInfo obj) //修改
       {
           //
       }

       public void Delss_ReturnItems(Tss_ReturnItemsInfo obj) //删除
       {
           //
       }

       public void Updetess_ReturnItems(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_goodskinds 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_goodskinds
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:14
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_goodskinds:Ibd_goodskinds
    {
       TSqlHelp db = null;
       public Tbd_goodskinds()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_goodskindsList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_goodskinds(Tbd_goodskindsInfo obj) //添加
       {
           //
       }

       public void Updatebd_goodskinds(Tbd_goodskindsInfo obj) //修改
       {
           //
       }

       public void Delbd_goodskinds(Tbd_goodskindsInfo obj) //删除
       {
           //
       }

       public void Updetebd_goodskinds(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tbd_employees 类 ********************/
    /// <实体类摘要>
    /// 类名：Tbd_employees
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:14
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tbd_employees:Ibd_employees
    {
       TSqlHelp db = null;
       public Tbd_employees()
       {
           db = new TSqlHelp();   }

       public DataTable getbd_employeesList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addbd_employees(Tbd_employeesInfo obj) //添加
       {
           //
       }

       public void Updatebd_employees(Tbd_employeesInfo obj) //修改
       {
           //
       }

       public void Delbd_employees(Tbd_employeesInfo obj) //删除
       {
           //
       }

       public void Updetebd_employees(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_Power 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_Power
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:15
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_Power:Ism_Power
    {
       TSqlHelp db = null;
       public Tsm_Power()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_PowerList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_Power(Tsm_PowerInfo obj) //添加
       {
           //
       }

       public void Updatesm_Power(Tsm_PowerInfo obj) //修改
       {
           //
       }

       public void Delsm_Power(Tsm_PowerInfo obj) //删除
       {
           //
       }

       public void Updetesm_Power(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_RolePower 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_RolePower
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:15
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_RolePower:Ism_RolePower
    {
       TSqlHelp db = null;
       public Tsm_RolePower()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_RolePowerList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_RolePower(Tsm_RolePowerInfo obj) //添加
       {
           //
       }

       public void Updatesm_RolePower(Tsm_RolePowerInfo obj) //修改
       {
           //
       }

       public void Delsm_RolePower(Tsm_RolePowerInfo obj) //删除
       {
           //
       }

       public void Updetesm_RolePower(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_Roles 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_Roles
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:15
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_Roles:Ism_Roles
    {
       TSqlHelp db = null;
       public Tsm_Roles()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_RolesList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_Roles(Tsm_RolesInfo obj) //添加
       {
           //
       }

       public void Updatesm_Roles(Tsm_RolesInfo obj) //修改
       {
           //
       }

       public void Delsm_Roles(Tsm_RolesInfo obj) //删除
       {
           //
       }

       public void Updetesm_Roles(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_UserPower 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_UserPower
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:15
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_UserPower:Ism_UserPower
    {
       TSqlHelp db = null;
       public Tsm_UserPower()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_UserPowerList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_UserPower(Tsm_UserPowerInfo obj) //添加
       {
           //
       }

       public void Updatesm_UserPower(Tsm_UserPowerInfo obj) //修改
       {
           //
       }

       public void Delsm_UserPower(Tsm_UserPowerInfo obj) //删除
       {
           //
       }

       public void Updetesm_UserPower(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tsm_UserVsRole 类 ********************/
    /// <实体类摘要>
    /// 类名：Tsm_UserVsRole
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:16
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tsm_UserVsRole:Ism_UserVsRole
    {
       TSqlHelp db = null;
       public Tsm_UserVsRole()
       {
           db = new TSqlHelp();   }

       public DataTable getsm_UserVsRoleList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Addsm_UserVsRole(Tsm_UserVsRoleInfo obj) //添加
       {
           //
       }

       public void Updatesm_UserVsRole(Tsm_UserVsRoleInfo obj) //修改
       {
           //
       }

       public void Delsm_UserVsRole(Tsm_UserVsRoleInfo obj) //删除
       {
           //
       }

       public void Updetesm_UserVsRole(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    /****************** 下面是 Tdtproperties 类 ********************/
    /// <实体类摘要>
    /// 类名：Tdtproperties
    /// 作者：陈耀秋
    /// EMAIL：chenyaoqiu@163.com
    /// 版本：1.0.0.0
    /// 时间：2007-10-18 21:51:16
    /// 说明：本实体类由代码生成器生成
    /// </实体类摘要>

    class Tdtproperties:Idtproperties
    {
       TSqlHelp db = null;
       public Tdtproperties()
       {
           db = new TSqlHelp();   }

       public DataTable getdtpropertiesList(string sql) //根据查询语句获取数据
       {
           return db.ExecuteQuery(sql);
       }

       public void Adddtproperties(TdtpropertiesInfo obj) //添加
       {
           //
       }

       public void Updatedtproperties(TdtpropertiesInfo obj) //修改
       {
           //
       }

       public void Deldtproperties(TdtpropertiesInfo obj) //删除
       {
           //
       }

       public void Updetedtproperties(string sql) //根据更新语句更新数据
       {
           db.ExecuteNonQuery(sql);
       }

    }

    public interface Ism_Events
    {
       DataTable getsm_EventsList(string sql);//根据查询语句获取数据
       void Addsm_Events(Tsm_EventsInfo obj);//添加
       void Updatesm_Events(Tsm_EventsInfo obj);//修改
       void Delsm_Events(Tsm_EventsInfo obj);//删除
       void Updetesm_Events(string sql);//根据更新语句更新数据
    }

    public interface Iss_customerorders
    {
       DataTable getss_customerordersList(string sql);//根据查询语句获取数据
       void Addss_customerorders(Tss_customerordersInfo obj);//添加
       void Updatess_customerorders(Tss_customerordersInfo obj);//修改
       void Delss_customerorders(Tss_customerordersInfo obj);//删除
       void Updetess_customerorders(string sql);//根据更新语句更新数据
    }

    public interface Iwh_billnoTemp
    {
       DataTable getwh_billnoTempList(string sql);//根据查询语句获取数据
       void Addwh_billnoTemp(Twh_billnoTempInfo obj);//添加
       void Updatewh_billnoTemp(Twh_billnoTempInfo obj);//修改
       void Delwh_billnoTemp(Twh_billnoTempInfo obj);//删除
       void Updetewh_billnoTemp(string sql);//根据更新语句更新数据
    }

    public interface Ibd_customers
    {
       DataTable getbd_customersList(string sql);//根据查询语句获取数据
       void Addbd_customers(Tbd_customersInfo obj);//添加
       void Updatebd_customers(Tbd_customersInfo obj);//修改
       void Delbd_customers(Tbd_customersInfo obj);//删除
       void Updetebd_customers(string sql);//根据更新语句更新数据
    }

    public interface Ibd_dictionary
    {
       DataTable getbd_dictionaryList(string sql);//根据查询语句获取数据
       void Addbd_dictionary(Tbd_dictionaryInfo obj);//添加
       void Updatebd_dictionary(Tbd_dictionaryInfo obj);//修改
       void Delbd_dictionary(Tbd_dictionaryInfo obj);//删除
       void Updetebd_dictionary(string sql);//根据更新语句更新数据
    }

    public interface Ibd_enumerations
    {
       DataTable getbd_enumerationsList(string sql);//根据查询语句获取数据
       void Addbd_enumerations(Tbd_enumerationsInfo obj);//添加
       void Updatebd_enumerations(Tbd_enumerationsInfo obj);//修改
       void Delbd_enumerations(Tbd_enumerationsInfo obj);//删除
       void Updetebd_enumerations(string sql);//根据更新语句更新数据
    }

    public interface Ibd_suppliers
    {
       DataTable getbd_suppliersList(string sql);//根据查询语句获取数据
       void Addbd_suppliers(Tbd_suppliersInfo obj);//添加
       void Updatebd_suppliers(Tbd_suppliersInfo obj);//修改
       void Delbd_suppliers(Tbd_suppliersInfo obj);//删除
       void Updetebd_suppliers(string sql);//根据更新语句更新数据
    }

    public interface Ibd_CompOrg
    {
       DataTable getbd_CompOrgList(string sql);//根据查询语句获取数据
       void Addbd_CompOrg(Tbd_CompOrgInfo obj);//添加
       void Updatebd_CompOrg(Tbd_CompOrgInfo obj);//修改
       void Delbd_CompOrg(Tbd_CompOrgInfo obj);//删除
       void Updetebd_CompOrg(string sql);//根据更新语句更新数据
    }

    public interface Ibd_regions
    {
       DataTable getbd_regionsList(string sql);//根据查询语句获取数据
       void Addbd_regions(Tbd_regionsInfo obj);//添加
       void Updatebd_regions(Tbd_regionsInfo obj);//修改
       void Delbd_regions(Tbd_regionsInfo obj);//删除
       void Updetebd_regions(string sql);//根据更新语句更新数据
    }

    public interface Ism_OrdersPowers
    {
       DataTable getsm_OrdersPowersList(string sql);//根据查询语句获取数据
       void Addsm_OrdersPowers(Tsm_OrdersPowersInfo obj);//添加
       void Updatesm_OrdersPowers(Tsm_OrdersPowersInfo obj);//修改
       void Delsm_OrdersPowers(Tsm_OrdersPowersInfo obj);//删除
       void Updetesm_OrdersPowers(string sql);//根据更新语句更新数据
    }

    public interface Ibd_warehouses
    {
       DataTable getbd_warehousesList(string sql);//根据查询语句获取数据
       void Addbd_warehouses(Tbd_warehousesInfo obj);//添加
       void Updatebd_warehouses(Tbd_warehousesInfo obj);//修改
       void Delbd_warehouses(Tbd_warehousesInfo obj);//删除
       void Updetebd_warehouses(string sql);//根据更新语句更新数据
    }

    public interface IWh_Stock_Alertline
    {
       DataTable getWh_Stock_AlertlineList(string sql);//根据查询语句获取数据
       void AddWh_Stock_Alertline(TWh_Stock_AlertlineInfo obj);//添加
       void UpdateWh_Stock_Alertline(TWh_Stock_AlertlineInfo obj);//修改
       void DelWh_Stock_Alertline(TWh_Stock_AlertlineInfo obj);//删除
       void UpdeteWh_Stock_Alertline(string sql);//根据更新语句更新数据
    }

    public interface Ibd_goods
    {
       DataTable getbd_goodsList(string sql);//根据查询语句获取数据
       void Addbd_goods(Tbd_goodsInfo obj);//添加
       void Updatebd_goods(Tbd_goodsInfo obj);//修改
       void Delbd_goods(Tbd_goodsInfo obj);//删除
       void Updetebd_goods(string sql);//根据更新语句更新数据
    }

    public interface Ism_BackupLog
    {
       DataTable getsm_BackupLogList(string sql);//根据查询语句获取数据
       void Addsm_BackupLog(Tsm_BackupLogInfo obj);//添加
       void Updatesm_BackupLog(Tsm_BackupLogInfo obj);//修改
       void Delsm_BackupLog(Tsm_BackupLogInfo obj);//删除
       void Updetesm_BackupLog(string sql);//根据更新语句更新数据
    }

    public interface Ibd_OrdersMaterial
    {
       DataTable getbd_OrdersMaterialList(string sql);//根据查询语句获取数据
       void Addbd_OrdersMaterial(Tbd_OrdersMaterialInfo obj);//添加
       void Updatebd_OrdersMaterial(Tbd_OrdersMaterialInfo obj);//修改
       void Delbd_OrdersMaterial(Tbd_OrdersMaterialInfo obj);//删除
       void Updetebd_OrdersMaterial(string sql);//根据更新语句更新数据
    }

    public interface Iss_SendItems
    {
       DataTable getss_SendItemsList(string sql);//根据查询语句获取数据
       void Addss_SendItems(Tss_SendItemsInfo obj);//添加
       void Updatess_SendItems(Tss_SendItemsInfo obj);//修改
       void Delss_SendItems(Tss_SendItemsInfo obj);//删除
       void Updetess_SendItems(string sql);//根据更新语句更新数据
    }

    public interface Ibd_cardboardCalculate
    {
       DataTable getbd_cardboardCalculateList(string sql);//根据查询语句获取数据
       void Addbd_cardboardCalculate(Tbd_cardboardCalculateInfo obj);//添加
       void Updatebd_cardboardCalculate(Tbd_cardboardCalculateInfo obj);//修改
       void Delbd_cardboardCalculate(Tbd_cardboardCalculateInfo obj);//删除
       void Updetebd_cardboardCalculate(string sql);//根据更新语句更新数据
    }

    public interface IAs_WBEvaluations
    {
       DataTable getAs_WBEvaluationsList(string sql);//根据查询语句获取数据
       void AddAs_WBEvaluations(TAs_WBEvaluationsInfo obj);//添加
       void UpdateAs_WBEvaluations(TAs_WBEvaluationsInfo obj);//修改
       void DelAs_WBEvaluations(TAs_WBEvaluationsInfo obj);//删除
       void UpdeteAs_WBEvaluations(string sql);//根据更新语句更新数据
    }

    public interface Iwh_wastebook_master
    {
       DataTable getwh_wastebook_masterList(string sql);//根据查询语句获取数据
       void Addwh_wastebook_master(Twh_wastebook_masterInfo obj);//添加
       void Updatewh_wastebook_master(Twh_wastebook_masterInfo obj);//修改
       void Delwh_wastebook_master(Twh_wastebook_masterInfo obj);//删除
       void Updetewh_wastebook_master(string sql);//根据更新语句更新数据
    }

    public interface Iwh_wastebook_detail
    {
       DataTable getwh_wastebook_detailList(string sql);//根据查询语句获取数据
       void Addwh_wastebook_detail(Twh_wastebook_detailInfo obj);//添加
       void Updatewh_wastebook_detail(Twh_wastebook_detailInfo obj);//修改
       void Delwh_wastebook_detail(Twh_wastebook_detailInfo obj);//删除
       void Updetewh_wastebook_detail(string sql);//根据更新语句更新数据
    }

    public interface Ibd_ioKinds
    {
       DataTable getbd_ioKindsList(string sql);//根据查询语句获取数据
       void Addbd_ioKinds(Tbd_ioKindsInfo obj);//添加
       void Updatebd_ioKinds(Tbd_ioKindsInfo obj);//修改
       void Delbd_ioKinds(Tbd_ioKindsInfo obj);//删除
       void Updetebd_ioKinds(string sql);//根据更新语句更新数据
    }

    public interface ISs_CoEvaluations
    {
       DataTable getSs_CoEvaluationsList(string sql);//根据查询语句获取数据
       void AddSs_CoEvaluations(TSs_CoEvaluationsInfo obj);//添加
       void UpdateSs_CoEvaluations(TSs_CoEvaluationsInfo obj);//修改
       void DelSs_CoEvaluations(TSs_CoEvaluationsInfo obj);//删除
       void UpdeteSs_CoEvaluations(string sql);//根据更新语句更新数据
    }

    public interface IWs_Pbcompositor
    {
       DataTable getWs_PbcompositorList(string sql);//根据查询语句获取数据
       void AddWs_Pbcompositor(TWs_PbcompositorInfo obj);//添加
       void UpdateWs_Pbcompositor(TWs_PbcompositorInfo obj);//修改
       void DelWs_Pbcompositor(TWs_PbcompositorInfo obj);//删除
       void UpdeteWs_Pbcompositor(string sql);//根据更新语句更新数据
    }

    public interface Ibd_UsersPos
    {
       DataTable getbd_UsersPosList(string sql);//根据查询语句获取数据
       void Addbd_UsersPos(Tbd_UsersPosInfo obj);//添加
       void Updatebd_UsersPos(Tbd_UsersPosInfo obj);//修改
       void Delbd_UsersPos(Tbd_UsersPosInfo obj);//删除
       void Updetebd_UsersPos(string sql);//根据更新语句更新数据
    }

    public interface Iss_coitems
    {
       DataTable getss_coitemsList(string sql);//根据查询语句获取数据
       void Addss_coitems(Tss_coitemsInfo obj);//添加
       void Updatess_coitems(Tss_coitemsInfo obj);//修改
       void Delss_coitems(Tss_coitemsInfo obj);//删除
       void Updetess_coitems(string sql);//根据更新语句更新数据
    }

    public interface IWs_Injunction
    {
       DataTable getWs_InjunctionList(string sql);//根据查询语句获取数据
       void AddWs_Injunction(TWs_InjunctionInfo obj);//添加
       void UpdateWs_Injunction(TWs_InjunctionInfo obj);//修改
       void DelWs_Injunction(TWs_InjunctionInfo obj);//删除
       void UpdeteWs_Injunction(string sql);//根据更新语句更新数据
    }

    public interface Ibd_FreightCompanies
    {
       DataTable getbd_FreightCompaniesList(string sql);//根据查询语句获取数据
       void Addbd_FreightCompanies(Tbd_FreightCompaniesInfo obj);//添加
       void Updatebd_FreightCompanies(Tbd_FreightCompaniesInfo obj);//修改
       void Delbd_FreightCompanies(Tbd_FreightCompaniesInfo obj);//删除
       void Updetebd_FreightCompanies(string sql);//根据更新语句更新数据
    }

    public interface IAs_WorkBill
    {
       DataTable getAs_WorkBillList(string sql);//根据查询语句获取数据
       void AddAs_WorkBill(TAs_WorkBillInfo obj);//添加
       void UpdateAs_WorkBill(TAs_WorkBillInfo obj);//修改
       void DelAs_WorkBill(TAs_WorkBillInfo obj);//删除
       void UpdeteAs_WorkBill(string sql);//根据更新语句更新数据
    }

    public interface Iss_Return
    {
       DataTable getss_ReturnList(string sql);//根据查询语句获取数据
       void Addss_Return(Tss_ReturnInfo obj);//添加
       void Updatess_Return(Tss_ReturnInfo obj);//修改
       void Delss_Return(Tss_ReturnInfo obj);//删除
       void Updetess_Return(string sql);//根据更新语句更新数据
    }

    public interface IAs_WBItems
    {
       DataTable getAs_WBItemsList(string sql);//根据查询语句获取数据
       void AddAs_WBItems(TAs_WBItemsInfo obj);//添加
       void UpdateAs_WBItems(TAs_WBItemsInfo obj);//修改
       void DelAs_WBItems(TAs_WBItemsInfo obj);//删除
       void UpdeteAs_WBItems(string sql);//根据更新语句更新数据
    }

    public interface Iss_Sends
    {
       DataTable getss_SendsList(string sql);//根据查询语句获取数据
       void Addss_Sends(Tss_SendsInfo obj);//添加
       void Updatess_Sends(Tss_SendsInfo obj);//修改
       void Delss_Sends(Tss_SendsInfo obj);//删除
       void Updetess_Sends(string sql);//根据更新语句更新数据
    }

    public interface IWs_CBCItems
    {
       DataTable getWs_CBCItemsList(string sql);//根据查询语句获取数据
       void AddWs_CBCItems(TWs_CBCItemsInfo obj);//添加
       void UpdateWs_CBCItems(TWs_CBCItemsInfo obj);//修改
       void DelWs_CBCItems(TWs_CBCItemsInfo obj);//删除
       void UpdeteWs_CBCItems(string sql);//根据更新语句更新数据
    }

    public interface IWs_CBcompositor
    {
       DataTable getWs_CBcompositorList(string sql);//根据查询语句获取数据
       void AddWs_CBcompositor(TWs_CBcompositorInfo obj);//添加
       void UpdateWs_CBcompositor(TWs_CBcompositorInfo obj);//修改
       void DelWs_CBcompositor(TWs_CBcompositorInfo obj);//删除
       void UpdeteWs_CBcompositor(string sql);//根据更新语句更新数据
    }

    public interface IWs_PBCItems
    {
       DataTable getWs_PBCItemsList(string sql);//根据查询语句获取数据
       void AddWs_PBCItems(TWs_PBCItemsInfo obj);//添加
       void UpdateWs_PBCItems(TWs_PBCItemsInfo obj);//修改
       void DelWs_PBCItems(TWs_PBCItemsInfo obj);//删除
       void UpdeteWs_PBCItems(string sql);//根据更新语句更新数据
    }

    public interface IWs_PBCEvaluations
    {
       DataTable getWs_PBCEvaluationsList(string sql);//根据查询语句获取数据
       void AddWs_PBCEvaluations(TWs_PBCEvaluationsInfo obj);//添加
       void UpdateWs_PBCEvaluations(TWs_PBCEvaluationsInfo obj);//修改
       void DelWs_PBCEvaluations(TWs_PBCEvaluationsInfo obj);//删除
       void UpdeteWs_PBCEvaluations(string sql);//根据更新语句更新数据
    }

    public interface IWs_CBCEvaluations
    {
       DataTable getWs_CBCEvaluationsList(string sql);//根据查询语句获取数据
       void AddWs_CBCEvaluations(TWs_CBCEvaluationsInfo obj);//添加
       void UpdateWs_CBCEvaluations(TWs_CBCEvaluationsInfo obj);//修改
       void DelWs_CBCEvaluations(TWs_CBCEvaluationsInfo obj);//删除
       void UpdeteWs_CBCEvaluations(string sql);//根据更新语句更新数据
    }

    public interface Iss_ReturnItems
    {
       DataTable getss_ReturnItemsList(string sql);//根据查询语句获取数据
       void Addss_ReturnItems(Tss_ReturnItemsInfo obj);//添加
       void Updatess_ReturnItems(Tss_ReturnItemsInfo obj);//修改
       void Delss_ReturnItems(Tss_ReturnItemsInfo obj);//删除
       void Updetess_ReturnItems(string sql);//根据更新语句更新数据
    }

    public interface Ibd_goodskinds
    {
       DataTable getbd_goodskindsList(string sql);//根据查询语句获取数据
       void Addbd_goodskinds(Tbd_goodskindsInfo obj);//添加
       void Updatebd_goodskinds(Tbd_goodskindsInfo obj);//修改
       void Delbd_goodskinds(Tbd_goodskindsInfo obj);//删除
       void Updetebd_goodskinds(string sql);//根据更新语句更新数据
    }

    public interface Ibd_employees
    {
       DataTable getbd_employeesList(string sql);//根据查询语句获取数据
       void Addbd_employees(Tbd_employeesInfo obj);//添加
       void Updatebd_employees(Tbd_employeesInfo obj);//修改
       void Delbd_employees(Tbd_employeesInfo obj);//删除
       void Updetebd_employees(string sql);//根据更新语句更新数据
    }

    public interface Ism_Power
    {
       DataTable getsm_PowerList(string sql);//根据查询语句获取数据
       void Addsm_Power(Tsm_PowerInfo obj);//添加
       void Updatesm_Power(Tsm_PowerInfo obj);//修改
       void Delsm_Power(Tsm_PowerInfo obj);//删除
       void Updetesm_Power(string sql);//根据更新语句更新数据
    }

    public interface Ism_RolePower
    {
       DataTable getsm_RolePowerList(string sql);//根据查询语句获取数据
       void Addsm_RolePower(Tsm_RolePowerInfo obj);//添加
       void Updatesm_RolePower(Tsm_RolePowerInfo obj);//修改
       void Delsm_RolePower(Tsm_RolePowerInfo obj);//删除
       void Updetesm_RolePower(string sql);//根据更新语句更新数据
    }

    public interface Ism_Roles
    {
       DataTable getsm_RolesList(string sql);//根据查询语句获取数据
       void Addsm_Roles(Tsm_RolesInfo obj);//添加
       void Updatesm_Roles(Tsm_RolesInfo obj);//修改
       void Delsm_Roles(Tsm_RolesInfo obj);//删除
       void Updetesm_Roles(string sql);//根据更新语句更新数据
    }

    public interface Ism_UserPower
    {
       DataTable getsm_UserPowerList(string sql);//根据查询语句获取数据
       void Addsm_UserPower(Tsm_UserPowerInfo obj);//添加
       void Updatesm_UserPower(Tsm_UserPowerInfo obj);//修改
       void Delsm_UserPower(Tsm_UserPowerInfo obj);//删除
       void Updetesm_UserPower(string sql);//根据更新语句更新数据
    }

    public interface Ism_UserVsRole
    {
       DataTable getsm_UserVsRoleList(string sql);//根据查询语句获取数据
       void Addsm_UserVsRole(Tsm_UserVsRoleInfo obj);//添加
       void Updatesm_UserVsRole(Tsm_UserVsRoleInfo obj);//修改
       void Delsm_UserVsRole(Tsm_UserVsRoleInfo obj);//删除
       void Updetesm_UserVsRole(string sql);//根据更新语句更新数据
    }

    public interface Idtproperties
    {
       DataTable getdtpropertiesList(string sql);//根据查询语句获取数据
       void Adddtproperties(TdtpropertiesInfo obj);//添加
       void Updatedtproperties(TdtpropertiesInfo obj);//修改
       void Deldtproperties(TdtpropertiesInfo obj);//删除
       void Updetedtproperties(string sql);//根据更新语句更新数据
    }

    public class EntityFactory
    {
       public static Ism_Events getsm_Events()
       {
          return new Tsm_Events();
       }
       public static Iss_customerorders getss_customerorders()
       {
          return new Tss_customerorders();
       }
       public static Iwh_billnoTemp getwh_billnoTemp()
       {
          return new Twh_billnoTemp();
       }
       public static Ibd_customers getbd_customers()
       {
          return new Tbd_customers();
       }
       public static Ibd_dictionary getbd_dictionary()
       {
          return new Tbd_dictionary();
       }
       public static Ibd_enumerations getbd_enumerations()
       {
          return new Tbd_enumerations();
       }
       public static Ibd_suppliers getbd_suppliers()
       {
          return new Tbd_suppliers();
       }
       public static Ibd_CompOrg getbd_CompOrg()
       {
          return new Tbd_CompOrg();
       }
       public static Ibd_regions getbd_regions()
       {
          return new Tbd_regions();
       }
       public static Ism_OrdersPowers getsm_OrdersPowers()
       {
          return new Tsm_OrdersPowers();
       }
       public static Ibd_warehouses getbd_warehouses()
       {
          return new Tbd_warehouses();
       }
       public static IWh_Stock_Alertline getWh_Stock_Alertline()
       {
          return new TWh_Stock_Alertline();
       }
       public static Ibd_goods getbd_goods()
       {
          return new Tbd_goods();
       }
       public static Ism_BackupLog getsm_BackupLog()
       {
          return new Tsm_BackupLog();
       }
       public static Ibd_OrdersMaterial getbd_OrdersMaterial()
       {
          return new Tbd_OrdersMaterial();
       }
       public static Iss_SendItems getss_SendItems()
       {
          return new Tss_SendItems();
       }
       public static Ibd_cardboardCalculate getbd_cardboardCalculate()
       {
          return new Tbd_cardboardCalculate();
       }
       public static IAs_WBEvaluations getAs_WBEvaluations()
       {
          return new TAs_WBEvaluations();
       }
       public static Iwh_wastebook_master getwh_wastebook_master()
       {
          return new Twh_wastebook_master();
       }
       public static Iwh_wastebook_detail getwh_wastebook_detail()
       {
          return new Twh_wastebook_detail();
       }
       public static Ibd_ioKinds getbd_ioKinds()
       {
          return new Tbd_ioKinds();
       }
       public static ISs_CoEvaluations getSs_CoEvaluations()
       {
          return new TSs_CoEvaluations();
       }
       public static IWs_Pbcompositor getWs_Pbcompositor()
       {
          return new TWs_Pbcompositor();
       }
       public static Ibd_UsersPos getbd_UsersPos()
       {
          return new Tbd_UsersPos();
       }
       public static Iss_coitems getss_coitems()
       {
          return new Tss_coitems();
       }
       public static IWs_Injunction getWs_Injunction()
       {
          return new TWs_Injunction();
       }
       public static Ibd_FreightCompanies getbd_FreightCompanies()
       {
          return new Tbd_FreightCompanies();
       }
       public static IAs_WorkBill getAs_WorkBill()
       {
          return new TAs_WorkBill();
       }
       public static Iss_Return getss_Return()
       {
          return new Tss_Return();
       }
       public static IAs_WBItems getAs_WBItems()
       {
          return new TAs_WBItems();
       }
       public static Iss_Sends getss_Sends()
       {
          return new Tss_Sends();
       }
       public static IWs_CBCItems getWs_CBCItems()
       {
          return new TWs_CBCItems();
       }
       public static IWs_CBcompositor getWs_CBcompositor()
       {
          return new TWs_CBcompositor();
       }
       public static IWs_PBCItems getWs_PBCItems()
       {
          return new TWs_PBCItems();
       }
       public static IWs_PBCEvaluations getWs_PBCEvaluations()
       {
          return new TWs_PBCEvaluations();
       }
       public static IWs_CBCEvaluations getWs_CBCEvaluations()
       {
          return new TWs_CBCEvaluations();
       }
       public static Iss_ReturnItems getss_ReturnItems()
       {
          return new Tss_ReturnItems();
       }
       public static Ibd_goodskinds getbd_goodskinds()
       {
          return new Tbd_goodskinds();
       }
       public static Ibd_employees getbd_employees()
       {
          return new Tbd_employees();
       }
       public static Ism_Power getsm_Power()
       {
          return new Tsm_Power();
       }
       public static Ism_RolePower getsm_RolePower()
       {
          return new Tsm_RolePower();
       }
       public static Ism_Roles getsm_Roles()
       {
          return new Tsm_Roles();
       }
       public static Ism_UserPower getsm_UserPower()
       {
          return new Tsm_UserPower();
       }
       public static Ism_UserVsRole getsm_UserVsRole()
       {
          return new Tsm_UserVsRole();
       }
       public static Idtproperties getdtproperties()
       {
          return new Tdtproperties();
       }
    }
    public class TSqlHelp
    {
        SqlConnection cn = null;
        public TSqlHelp()
        {
            cn = new SqlConnection("Server=.;DataBase=Tractor;UID=sa;PWD=253");
        }

        public DataTable ExecuteQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
         }

        public void ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

    }


}
