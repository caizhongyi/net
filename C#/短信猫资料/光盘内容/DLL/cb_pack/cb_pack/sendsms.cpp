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
           DllSmsInst = LoadLibrary("mysms.dll");
       if (DllSmsInst == NULL) Application->MessageBox("没有发现mysms.dll文件", "提示", MB_OK);
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
    bool iResult;
    int port;
    port=StrToInt(Edit1->Text);
    String StrMobileType[1000];
    StrMobileType[0]='\0';
    FARPROC proc;
    proc = GetProcAddress(DllSmsInst,"InitModem");
    typedef bool (* FUNC)(int comport, int Baud);
    FUNC aFunc=(FUNC)proc;
    iResult = aFunc(port,9600);

    if (iResult)
    {
        Label6->Caption="连接成功";
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
    bool iResult;
    int port;
    port=StrToInt(Edit1->Text);
    FARPROC proc;
    AnsiString StrCopyRight;
    proc = GetProcAddress(DllSmsInst,"SendSms");
    typedef bool (* FUNC)(int comport, int Baud, AnsiString sMessage, AnsiString  sTo,bool bEnglish,bool bAlert,bool bSr);
    FUNC aFunc=(FUNC)proc;
    iResult = aFunc(port,9600,Memo1->Text,Edit3->Text,false,false,false);
    if (iResult)
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
    String comefrom[1000];
    comefrom[0]='\0';
    String sendtime[1000];
    sendtime[0]='\0';
    FARPROC proc3;
    bool iResult;
    int port;
    port=StrToInt(Edit1->Text);
    proc3 = GetProcAddress(DllSmsInst,"ReadSms");
    typedef bool (* FUNC3)(int comport, int Baud, int Index, String *sMessage, String *sFrom, String *sTime, bool bDel);
    FUNC3 Sms_Receive=(FUNC3)proc3;
    iResult = Sms_Receive(port,9600,1,StrReceiveSMS,comefrom,sendtime,true);
    Memo2->Text=*StrReceiveSMS;
}

void __fastcall TNoahSoft::Button7Click(TObject *Sender)
{
    int iResult;
}


void __fastcall TNoahSoft::Timer1Timer(TObject *Sender)
{
    int iResult;
}
//---------------------------------------------------------------------------

void __fastcall TNoahSoft::Button8Click(TObject *Sender)
{
Application->Terminate();        
}
//---------------------------------------------------------------------------


