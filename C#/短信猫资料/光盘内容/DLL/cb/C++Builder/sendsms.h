//---------------------------------------------------------------------------

#ifndef sendsmsH
#define sendsmsH
//---------------------------------------------------------------------------

#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Classes.hpp>
#include <ExtCtrls.hpp>



//---------------------------------------------------------------------------
class TNoahSoft : public TForm
{
__published:	// IDE-managed Components
        TLabel *Label2;
        TEdit *Edit1;
        TLabel *Label3;
        TGroupBox *GroupBox1;
        TButton *Button1;
        TButton *Button4;
        TGroupBox *GroupBox2;
        TButton *Button2;
        TButton *Button6;
        TLabel *Label6;
        TGroupBox *GroupBox3;
        TLabel *Label1;
        TLabel *Label4;
        TEdit *Edit3;
        TButton *Button3;
        TLabel *Label7;
        TGroupBox *GroupBox4;
        TButton *Button5;
        TGroupBox *GroupBox5;
        TLabel *Label5;
        TEdit *Edit5;
        TButton *Button7;
        TButton *Button8;
        TMemo *Memo1;
        TMemo *Memo2;
        TTimer *Timer1;
        TLabel *Label10;


        void __fastcall Button2Click(TObject *Sender);
        void __fastcall Button3Click(TObject *Sender);
        void __fastcall Button1Click(TObject *Sender);
        void __fastcall Button6Click(TObject *Sender);
        void __fastcall Button4Click(TObject *Sender);
        void __fastcall Button5Click(TObject *Sender);
        void __fastcall Button7Click(TObject *Sender);
        void __fastcall Timer1Timer(TObject *Sender);
        void __fastcall Button8Click(TObject *Sender);

private:	// User declarations

         HINSTANCE     DllSmsInst;                //动态连接库实例名




public:		// User declarations
        __fastcall TNoahSoft(TComponent* Owner);
        __fastcall ~TNoahSoft( );
};
//---------------------------------------------------------------------------
extern PACKAGE TNoahSoft *NoahSoft;
//---------------------------------------------------------------------------
#endif


