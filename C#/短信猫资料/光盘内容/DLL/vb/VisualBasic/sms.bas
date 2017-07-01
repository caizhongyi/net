Attribute VB_Name = "loadsms"
Option Explicit
'短信收发二次开发接口(标准版)函数使用说明
'Sms_Connection函数说明如下：
'功能描述：用于初始化移动电话与串口的连接
'Com_Port：串口号
'Com_BaudRate：波特率
'Mobile_Type：返回移动电话型号
'Sms_Connection：返回值(0：连接移动电话失败；1：连接移动电话成功)
'******************************************************

'******************************************************
'Sms_Send函数说明如下：
'功能描述：发送短信
'Sms_TelNum：发送给的移动电话号码
'Sms_Text：发送的短信内容
'Sms_Connection：返回值(0：发送短信失败；1：发送短信成功)
'******************************************************

'******************************************************
'Sms_Receive函数说明如下：
'功能描述：接收指定类型的短信
'Sms_Type：短信类型(0：未读短信；1：已读短信；2：待发短信；3：已发短信；4：全部短信)
'Sms_Text：返回指定类型的短信内容字符串(短信内容字符串说明：短信与短信之前用"|"符号作为分隔符,每条短信中间的各字段用"#"符号作为分隔符)
'******************************************************

'******************************************************
'Sms_Delete函数说明如下：
'功能描述：删除指定的短信
'Sms_Index：短信的索引号
'******************************************************

'******************************************************
'Sms_AutoFlag函数说明如下：
'功能描述：检测连接的移动电话是否支持自动收发短信功能
'Sms_AutoFlag：返回值(0：不支持；1：支持)
'******************************************************

'******************************************************
'Sms_NewFlag函数说明如下：
'功能描述：查询是否收到新的短信息
'Sms_AutoFlag：返回值(0：未收到；1：收到)
'******************************************************

'******************************************************
'Sms_Disconnection函数说明如下：
'功能描述：断开移动电话与串口的连接
'******************************************************

Public Declare Function Sms_Connection Lib "sms.dll" (ByVal CopyRight As String, ByVal Com_Port As Integer, ByVal Com_BaudRate As Integer, Mobile_Type As String, CopyRightToCOM As String) As Long
Public Declare Function Sms_Send Lib "sms.dll" (ByVal Sms_TelNum As String, ByVal Sms_Text As String) As Long
Public Declare Function Sms_Receive Lib "sms.dll" (ByVal Sms_Type As String, Sms_Text As String) As Long
Public Declare Function Sms_Delete Lib "sms.dll" (ByVal Sms_Index As String) As Long
Public Declare Function Sms_AutoFlag Lib "sms.dll" () As Long
Public Declare Function Sms_NewFlag Lib "sms.dll" () As Long
Public Declare Function Sms_Disconnection Lib "sms.dll" () As Long









