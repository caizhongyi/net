//===============================================
//��������������������\\\|///                      
//��������������������\\��- -��//                   
//��������������������  ( @ @ )                    
//��������������������oOOo-(_)-oOOo��������          
//��                                     ��
//��             �� �� ԭ ����           ��
//��      lenlong ��Ʒ���뱣������Ϣ��   ��
//��      ** lenlenlong@hotmail.com **   ��
//��                                     ��
//����������������������������Dooo��     ��
//�������������������� oooD��-(�� )��������
//�������������������� (  )��  ) /
//����������������������\ (�� (_/
//���������������������� \_)
//===============================================
using System;
using System.Text;
namespace WebSite
{
    public class WebSiteConfig
    {
        public void SetWebSiteSession()
        {
            DosOrg.Systems.SystemsInfoHelper.Instance().WebSiteSessionStart();
        }
        public void SetWebSiteApplicationStart()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (IDoNetBbs.GetConfiguration("ConnectionType").ToString() == "Access")
            {
                DataProviders.DataConnection.AccessData = IDoNetBbs.GetHttpContext().Server.MapPath("~" + IDoNetBbs.GetConfiguration("AccessData").ToString());
            }
            DosOrg.Systems.SystemsInfoHelper.Instance().WebSiteApplicationStart();
        }
        public void SetWebSiteApplicationEnd()
        {
            DosOrg.Systems.SystemsInfoHelper.Instance().WebSiteApplicationEnd();
        }
    }
}
