unit Send;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleCtrls, ShouYan_SmsGate61_TLB,  ComCtrls, ExtCtrls;

type
  SendMsg = class(TThread)
  private
    constructor create(StartThread:boolean);overload;
  protected
    procedure Execute; override;
  end;

implementation

uses Unit1;

constructor  SendMsg.create(StartThread: boolean);
begin
 create(StartThread);
end;


procedure SendMsg.Execute;
{var
se:string;
tmob,content:Widestring;
report:Smallint;

rewhy:Smallint;
re:string;
FileHandle:integer;
textfile:Tstringlist;
i:integer;}
begin
{  report:=0;
  sum:=0;
  control:=true;
  textfile:=tstringlist.Create;

if not FileExists('c:\data.txt') then  //�����ļ�
  begin
    FileHandle:= FileCreate('c:\data.txt');
    FileClose(FileHandle);
  end;
  while (control) do
    begin
      application.ProcessMessages;
      if sum>8 then   //������ȡ���洢
        begin
          form1.memo3.Lines.Add('���ڽ�����Ϣ');
          rewhy:=strtoint(form1.ComboBox3.text);
          re:=form1.Smsgate1.ReadMsg(rewhy);
          textfile.LoadFromFile('c:\data.txt');
          textfile.Add(re);
          textfile.SaveToFile('c:\data.txt');
          form1.memo2.Lines.Add(re);
       //   memo1.Lines.Add(re);

          sum:=0;
          super1:=0;
        end;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�߷�
    if  PhoneSendInfo[5].s<PhoneSendInfo[5].Second then
        begin
          tmob:=PhoneSendInfo[5].Phone;
          content:=PhoneSendInfo[5].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[5].Second:=0;
              form1.edit31.Text:=inttostr(0);
              sum:=sum+1;
            end
          Else
            form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;
//91��~991��~991��~991��~991��~991��~991��~991��~991��~991��~991��~9
      if ((PhoneSendInfo[0].N>PhoneSendInfo[0].Count) and (form1.checkbox3.Checked)) then
      // if checkbox3.Checked then
        begin
          tmob:=PhoneSendInfo[0].Phone;
          content:=PhoneSendInfo[0].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[0].Count:=PhoneSendInfo[0].count+1;
              sum:=sum+1;
            end
          Else
             form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�߷�
      if PhoneSendInfo[5].s<PhoneSendInfo[5].Second then
        begin
          tmob:=PhoneSendInfo[5].Phone;
          content:=PhoneSendInfo[5].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[5].Second:=0;
              form1.edit31.Text:=inttostr(0);
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;
//������~������~������~������~������~������~������~������~������~������~
       if ((PhoneSendInfo[1].N>PhoneSendInfo[1].Count) and (form1.checkbox4.Checked)) then
       //if checkbox4.Checked then
        begin
          tmob:=PhoneSendInfo[1].Phone;
          content:=PhoneSendInfo[1].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[1].Count:=PhoneSendInfo[1].count+1;
              sum:=sum+1;
            end
          Else
             form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�߷�
      if  PhoneSendInfo[5].s<PhoneSendInfo[5].Second then
        begin
          tmob:=PhoneSendInfo[5].Phone;
          content:=PhoneSendInfo[5].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[5].Second:=0;
              form1.edit31.Text:=inttostr(0);
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;
//����~����~����~����~����~����~����~����~����~����~����~����~����~����~
     if ((PhoneSendInfo[2].N>PhoneSendInfo[2].Count) and (form1.checkbox5.Checked)) then
      //if  checkbox5.Checked then
        begin
          tmob:=PhoneSendInfo[2].Phone;
          content:=PhoneSendInfo[2].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[2].Count:=PhoneSendInfo[2].count+1;
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
         end;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�߷�
      if  PhoneSendInfo[5].s<PhoneSendInfo[5].Second then
        begin
          tmob:=PhoneSendInfo[5].Phone;
          content:=PhoneSendInfo[5].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[5].Second:=0;
              form1.edit31.Text:=inttostr(0);
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;

//�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~�Ʒ�~
      if ((PhoneSendInfo[3].N>PhoneSendInfo[3].Count) and (form1.checkbox6.Checked)) then
      //if  checkbox6.Checked then
        begin
          tmob:=PhoneSendInfo[3].Phone;
          content:=PhoneSendInfo[3].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[3].Count:=PhoneSendInfo[3].count+1;
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
         end;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�߷�
      if  PhoneSendInfo[5].s<PhoneSendInfo[5].Second then
        begin
          tmob:=PhoneSendInfo[5].Phone;
          content:=PhoneSendInfo[5].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[5].Second:=0;
              form1.edit31.Text:=inttostr(0);
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
        end;
//����~����~����~����~����~����~����~����~����~����~����~����~����~����~
      if ((PhoneSendInfo[4].N>PhoneSendInfo[4].Count) and (form1.checkbox7.Checked)) then
      //if  checkbox7.Checked then
        begin
          tmob:=PhoneSendInfo[4].Phone;     //             ��������
          content:=PhoneSendInfo[4].Content;
          se:=form1.smsgate1.Sendsms(content,tmob,report);
          IF se = 'y' Then
            begin
              form1.memo3.Lines.Add('��Ϣ���ͳɹ���'+tmob+'��');
              PhoneSendInfo[4].Count:=PhoneSendInfo[4].count+1;
              sum:=sum+1;
            end
          Else
              form1.memo3.Lines.Add('��Ϣ����ʧ�ܣ�'+tmob+'��');
         end;
    end; }
end;

end.
