Attribute VB_Name = "loadsms"
Option Explicit
'�����շ����ο����ӿ�(��׼��)����ʹ��˵��
'Sms_Connection����˵�����£�
'�������������ڳ�ʼ���ƶ��绰�봮�ڵ�����
'Com_Port�����ں�
'Com_BaudRate��������
'Mobile_Type�������ƶ��绰�ͺ�
'Sms_Connection������ֵ(0�������ƶ��绰ʧ�ܣ�1�������ƶ��绰�ɹ�)
'******************************************************

'******************************************************
'Sms_Send����˵�����£�
'�������������Ͷ���
'Sms_TelNum�����͸����ƶ��绰����
'Sms_Text�����͵Ķ�������
'Sms_Connection������ֵ(0�����Ͷ���ʧ�ܣ�1�����Ͷ��ųɹ�)
'******************************************************

'******************************************************
'Sms_Receive����˵�����£�
'��������������ָ�����͵Ķ���
'Sms_Type����������(0��δ�����ţ�1���Ѷ����ţ�2���������ţ�3���ѷ����ţ�4��ȫ������)
'Sms_Text������ָ�����͵Ķ��������ַ���(���������ַ���˵�������������֮ǰ��"|"������Ϊ�ָ���,ÿ�������м�ĸ��ֶ���"#"������Ϊ�ָ���)
'******************************************************

'******************************************************
'Sms_Delete����˵�����£�
'����������ɾ��ָ���Ķ���
'Sms_Index�����ŵ�������
'******************************************************

'******************************************************
'Sms_AutoFlag����˵�����£�
'����������������ӵ��ƶ��绰�Ƿ�֧���Զ��շ����Ź���
'Sms_AutoFlag������ֵ(0����֧�֣�1��֧��)
'******************************************************

'******************************************************
'Sms_NewFlag����˵�����£�
'������������ѯ�Ƿ��յ��µĶ���Ϣ
'Sms_AutoFlag������ֵ(0��δ�յ���1���յ�)
'******************************************************

'******************************************************
'Sms_Disconnection����˵�����£�
'�����������Ͽ��ƶ��绰�봮�ڵ�����
'******************************************************

Public Declare Function Sms_Connection Lib "sms.dll" (ByVal CopyRight As String, ByVal Com_Port As Integer, ByVal Com_BaudRate As Integer, Mobile_Type As String, CopyRightToCOM As String) As Long
Public Declare Function Sms_Send Lib "sms.dll" (ByVal Sms_TelNum As String, ByVal Sms_Text As String) As Long
Public Declare Function Sms_Receive Lib "sms.dll" (ByVal Sms_Type As String, Sms_Text As String) As Long
Public Declare Function Sms_Delete Lib "sms.dll" (ByVal Sms_Index As String) As Long
Public Declare Function Sms_AutoFlag Lib "sms.dll" () As Long
Public Declare Function Sms_NewFlag Lib "sms.dll" () As Long
Public Declare Function Sms_Disconnection Lib "sms.dll" () As Long









