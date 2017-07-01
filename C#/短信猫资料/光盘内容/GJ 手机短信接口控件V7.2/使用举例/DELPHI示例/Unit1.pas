unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleCtrls, ShouYan_SmsGate61_TLB,  ComCtrls, ExtCtrls,send;

type
   Tpt = Record
      Flag  :boolean;
      Phone :string;
      Content:string;
      N     :integer;
      S     :integer;
      Count :integer;
      Second:integer;
   End;

  TForm1 = class(TForm)
    Smsgate1: TSmsgate;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    ComboBox1: TComboBox;
    Label2: TLabel;
    Edit1: TEdit;
    Label3: TLabel;
    ComboBox2: TComboBox;
    Button1: TButton;
    Label4: TLabel;
    edit2: TEdit;
    GroupBox2: TGroupBox;
    Edit3: TEdit;
    Button2: TButton;
    Button3: TButton;
    GroupBox3: TGroupBox;
    Label5: TLabel;
    ComboBox3: TComboBox;
    Button4: TButton;
    Label7: TLabel;
    Memo2: TMemo;
    GroupBox4: TGroupBox;
    ComboBox4: TComboBox;
    Button5: TButton;
    Label8: TLabel;
    GroupBox6: TGroupBox;
    Edit4: TEdit;
    Label9: TLabel;
    Button10: TButton;
    Button11: TButton;
    Button12: TButton;
    GroupBox7: TGroupBox;
    Button13: TButton;
    Button14: TButton;
    Button15: TButton;
    Button16: TButton;
    Button17: TButton;
    Button18: TButton;
    Button19: TButton;
    StatusBar1: TStatusBar;
    Label10: TLabel;
    CheckBox2: TCheckBox;
    Button8: TButton;
    Button9: TButton;
    CheckBox1: TCheckBox;
    Label12: TLabel;
    Memo1: TMemo;
    Label6: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button8Click(Sender: TObject);
    procedure Button9Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button10Click(Sender: TObject);
    procedure Button11Click(Sender: TObject);
    procedure Button12Click(Sender: TObject);
    procedure Button19Click(Sender: TObject);
    procedure Button18Click(Sender: TObject);
    procedure Button17Click(Sender: TObject);
    procedure Button16Click(Sender: TObject);
    procedure Button15Click(Sender: TObject);
    procedure Button14Click(Sender: TObject);
    procedure Button13Click(Sender: TObject);
    procedure Smsgate1StatusChange(Sender: TObject);
    procedure Smsgate1RecvMsg(Sender: TObject);
    procedure Smsgate1RevReport(Sender: TObject);
    procedure Smsgate1Call(ASender: TObject; var PhoneNo: OleVariant);
    procedure CheckBox2Click(Sender: TObject);
  private
    { Private declarations }
    Thread1:SendMsg;
  public
    { Public declarations }
  end;
 Procedure PhoneSendInfoSeting(item:integer;Flag:boolean;Phone:string;
            Content:string;N:integer;S:integer;Count:integer;
            Second:integer);
var
  Form1: TForm1;
  Sum,super1:integer;
  PhoneSendInfo:Array[0..8] of Tpt;
  control:boolean;
implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
content:string;
aa:Smallint;
begin
Smsgate1.CommPort:=strtoint(ComboBox1.Text);
Smsgate1.SmsService:=trim(edit1.text);
Smsgate1.Settings:=trim(ComboBox2.Text)+',n,8,1';
aa:=1;
content:=Smsgate1.Connect(aa);
edit2.Text:=Smsgate1.M_imei;
IF content='y' Then
  button1.Caption:='连接（成功）'
Else
  begin
    button1.Caption:='连接（失败）' ;
    smsgate1.ClosePort;
  end;
End;

procedure TForm1.Button2Click(Sender: TObject);
var
se:string;
tmob,content:Widestring;
report:Smallint;

rewhy:Smallint;
re:string;
FileHandle:integer;
textfile:Tstringlist;
i:integer;
begin
  report:=0;
  tmob:=edit3.text;
  content:=memo1.text;
  se:=smsgate1.Sendsms(content,tmob,report);
  IF se = 'y' Then
      StatusBar1.Panels.Items[1].Text:='信息发送成功'
  Else
      StatusBar1.Panels.Items[1].Text:='信息发送失败';
end;

procedure TForm1.Button3Click(Sender: TObject);
var
se:string;
tmob,content,report:OleVariant;
begin
tmob:=edit3.Text;
IF checkbox1.Checked Then
  report:=1
Else
  report:=0;
se:=smsgate1.SendAsc2(content,tmob,report);
IF se = 'y' Then
  GroupBox2.Caption:='发送信息-提示：信息以发送'
Else
  GroupBox2.Caption:='发送信息-提示：信息发送失败';
end;

procedure TForm1.Button4Click(Sender: TObject);
var
rewhy:Smallint;
re:string;
begin
rewhy:=strtoint(ComboBox3.text);
re:=Smsgate1.ReadMsg(rewhy);
memo2.Lines.Add(re);
end;

procedure TForm1.Button8Click(Sender: TObject);
var
Link:WordBool;
begin
Link:=Smsgate1.Link;
IF Link=True Then
showmessage('接连状态正常')
Else
showmessage('接连状态不正常');
end;

procedure TForm1.Button9Click(Sender: TObject);
begin
Smsgate1.ClosePort;
end;

procedure TForm1.Button5Click(Sender: TObject);
var
dinfo:Smallint;
info:OleVariant;
begin
dinfo:=strtoint(ComboBox4.Text);
info:=Smsgate1.DelSms(dinfo);
IF info='y' Then
Begin
IF dinfo=1 Then
showmessage('成功删除已读信息')
Else
showmessage('成功删除所有信息');
End;
end;

procedure TForm1.Button10Click(Sender: TObject);
var
phome:WideString;
time:Smallint;
begin
time:=33;
phome:=edit4.Text;
IF phome='' Then
  showmessage('请输入电话号码！')
Else
  Smsgate1.CallPhone(phome,time);
end;

procedure TForm1.Button11Click(Sender: TObject);
begin
Smsgate1.HangUpCall;
end;

procedure TForm1.Button12Click(Sender: TObject);
begin
Smsgate1.AnswerCall;
end;

procedure TForm1.Button19Click(Sender: TObject);
var
model:OleVariant;
begin
model:=Smsgate1.M_model;
showmessage('手机型号：'+model);
end;

procedure TForm1.Button18Click(Sender: TObject);
var
factory:OleVariant;
begin
factory:=Smsgate1.M_ltd;
showmessage('手机厂家名称：'+factory);
end;

procedure TForm1.Button17Click(Sender: TObject);
var
serv:OleVariant;
begin
serv:=Smsgate1.M_ServiceNo;
showmessage('服务中心号码：'+serv);
end;

procedure TForm1.Button16Click(Sender: TObject);
var
no,mc:OleVariant;
begin
no:=Smsgate1.ReadNB(mc);
showmessage('未接电话记录：'+Smsgate1.ReadNB(mc));
end;

procedure TForm1.Button15Click(Sender: TObject);
var
no,rc:OleVariant;
begin
no:=Smsgate1.ReadNB(rc);
showmessage('已接电话记录：'+no);
end;

procedure TForm1.Button14Click(Sender: TObject);
var
no,sm:OleVariant;
begin
no:=Smsgate1.ReadNB(sm);
showmessage('SIM卡电话溥：'+no);
end;

procedure TForm1.Button13Click(Sender: TObject);
var
no,me:OleVariant;
begin
no:=Smsgate1.ReadNB(me);
showmessage('读手机电话溥：'+no);
end;

procedure TForm1.Smsgate1StatusChange(Sender: TObject);
begin
 If Smsgate1.Isbusy = False Then
    StatusBar1.Panels.Items[0].Text:='    当前端口状态：空闲'
 Else
    StatusBar1.Panels.Items[0].Text:='    当前端口状态：工作中';
end;

procedure TForm1.Smsgate1RecvMsg(Sender: TObject);
var
ss:string;
textfile:tstringlist;
begin
super1:=super1+1;
StatusBar1.Panels.Items[1].Text:='您有新的信息'+inttostr(super1);
ss:=Smsgate1.NewMsg;
memo2.Lines.Add(ss);
            textfile:=tstringlist.Create;
            textfile.LoadFromFile('c:\data.txt');

            textfile.Add(ss);
            textfile.SaveToFile('c:\data.txt');
end;

procedure TForm1.Smsgate1RevReport(Sender: TObject);
begin
StatusBar1.Panels.Items[1].Text:=Smsgate1.NewReport;
end;

procedure TForm1.Smsgate1Call(ASender: TObject; var PhoneNo: OleVariant);
begin
StatusBar1.Panels.Items[1].Text:='有电话来啦：' + PhoneNo;

end;


procedure TForm1.CheckBox2Click(Sender: TObject);
begin
IF checkbox2.Checked Then
  Smsgate1.RevAuto
Else
  Smsgate1.RevAutoClose;
end;

Procedure PhoneSendInfoSeting(item:integer;Flag:boolean;Phone:string;
            Content:string;N:integer;S:integer;Count:integer;
            Second:integer);
begin
    PhoneSendInfo[item].Flag:=Flag;
    PhoneSendInfo[item].Phone:=Phone;
    PhoneSendInfo[item].Content:=Content;
    PhoneSendInfo[item].N:=N;
    PhoneSendInfo[item].S:=S;
    PhoneSendInfo[item].Count:=Count;
    PhoneSendInfo[item].Second:=Second;
end;


end.
