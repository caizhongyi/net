//---------------------------------------------------------------------------
#pragma hdrstop

#include <stdio.h>

#include <vcl.h>
#include "sendsms.h"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TNoahSoft *NoahSoft;
//---------------------------------------------------------------------------
__fastcall TNoahSoft::TNoahSoft(TComponent* Owner)
        : TForm(Owner)
{
       //加载动态连接库
       DllSmsInst = NULL;
       if (DllSmsInst == NULL)
           DllSmsInst = LoadLibrary("sms.dll");
       if (DllSmsInst == NULL) Application->MessageBox("没有发现sms.dll文件", "提示", MB_OK);
}

__fastcall TNoahSoft::~TNoahSoft( )
{
    //释放动态连接库
    if (DllSmsInst)
    {
        FreeLibrary(DllSmsInst);
        DllSmsInst=NULL;
    }
}
void __fastcall TNoahSoft::Button2Click(TObject *Sender)
{
    int iResult;
    int port;
    port=StrToInt(Edit1->Text);
    String StrMobileType[1000];
    StrMobileType[0]='\0';
    FARPROC proc;
    proc = GetProcAddress(DllSmsInst,"Sms_Connection");
    typedef int (* FUNC)(int Com_Port, int Com_BaudRate,String *Mobile_Type);
    FUNC aFunc=(FUNC)proc;
    iResult = aFunc(port,9600,StrMobileType);

    if (iResult==1)
    {
        Label6->Caption="连接成功,短信猫型号:"+*StrMobileType;
        Button2->Enabled=false;
        Button6->Enabled=true;
    }
    else
    {
       Label6->Caption="连接失败";
       Button2->Enabled=true;
       Button6->Enabled=false;
    }

}
//---------------------------------------------------------------------------
void __fastcall TNoahSoft::Button3Click(TObject *Sender)
{
    int iResult;

    FARPROC proc;
    AnsiString StrCopyRight;
    StrCopyRight="//深圳市国爵电子有限公司,网址www.gprscat.com //";
    proc = GetProcAddress(DllSmsInst,"Sms_Send");
    typedef int (* FUNC)(AnsiString CopyRight, AnsiString  Sms_TelNum, AnsiString Sms_Text);
    FUNC aFunc=(FUNC)proc;
    iResult = aFunc(StrCopyRight,Edit3->Text,Memo1->Text);
    if (iResult==0)
    {
       Label10->Caption="发送失败";
    }
    else
    {
       Label10->Caption="发送成功";
    }

}

void __fastcall TNoahSoft::Button1Click(TObject *Sender)
{
   //Button2->Click();
   Button1->Enabled=false;
   Button4->Enabled=true;
   Timer1->Enabled=true;
}

void __fastcall TNoahSoft::Button6Click(TObject *Sender)
{

    FARPROC proc2;
    int iResult;
    proc2 = GetProcAddress(DllSmsInst,"Sms_Disconnection");
    typedef int (* FUNC2)();
    FUNC2 Sms_Disconnection=(FUNC2)proc2;
    iResult = Sms_Disconnection();
    Button2->Enabled=true;
    Button6->Enabled=false;
    Label6->Caption="已断开";

}

void __fastcall TNoahSoft::Button4Click(TObject *Sender)
{
        //Button6->Click();
        Button1->Enabled=true;
        Button4->Enabled=false;
        Timer1->Enabled=false;
        Label7->Caption="无新短信";
}

void __fastcall TNoahSoft::Button5Click(TObject *Sender)
{
    String StrReceiveSMS[1000];
    StrReceiveSMS[0]='\0';
    FARPROC proc3;
    int iResult;
    proc3 = GetProcAddress(DllSmsInst,"Sms_Receive");
    typedef int (* FUNC3)(AnsiString, String *ReceiveSMS);
    FUNC3 Sms_Receive=(FUNC3)proc3;
    iResult = Sms_Receive("4",StrReceiveSMS);
    Memo2->Text=*StrReceiveSMS;
}

void __fastcall TNoahSoft::Button7Click(TObject *Sender)
{

    FARPROC proc1;
    int iResult;
    proc1 = GetProcAddress(DllSmsInst,"Sms_Delete");
    typedef int (* FUNC1)(AnsiString  Sms_Index);
    FUNC1 Sms_Delete=(FUNC1)proc1;
    iResult = Sms_Delete(Edit5->Text);
}


void __fastcall TNoahSoft::Timer1Timer(TObject *Sender)
{
    FARPROC proc2;
    int iResult;
    proc2 = GetProcAddress(DllSmsInst,"Sms_NewFlag");
    typedef int (* FUNC2)();
    FUNC2 Sms_NewFlag=(FUNC2)proc2;
    iResult = Sms_NewFlag();
    if(iResult==1)
    {
        Label7->Caption="有新短信，请接收";
    }
    else
    {
        Label7->Caption="无新短信";
    }
}
//---------------------------------------------------------------------------

void __fastcall TNoahSoft::Button8Click(TObject *Sender)
{
Application->Terminate();        
}
//---------------------------------------------------------------------------


