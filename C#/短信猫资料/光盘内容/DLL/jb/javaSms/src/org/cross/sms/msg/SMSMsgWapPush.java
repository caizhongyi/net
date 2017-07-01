package org.cross.sms.msg;

import java.util.*;


/**
 * <p>Title: </p>
 *
 * <p>Description: </p>
 *
 * <p>Copyright: Copyright (c) 2005</p>
 *
 * <p>Company: </p>
 *
 * @author not attributable
 * @version 1.0
 */
public class SMSMsgWapPush extends SMSMsgOut {
    String m_udpdu = "";

    public SMSMsgWapPush(String receipt,String text,String url){
        super(receipt,text+url);
        this.m_withUserHeader = true;
        this.messageEncoding = "iso-8859-1";
    }

    public String getTCSPdu(){
        TP_Dsc_CharSet charset = new TP_Dsc_CharSet();
        charset.setWap();
        return charset.getPdu();
    }

    public String getUserDataPdu() {
        return m_udpdu;
    }

    public String getDataHeader(){
        TP_UDH h = new TP_UDH();
        //����sarԪ��
        TP_UDH_EI_Sar sar = new TP_UDH_EI_Sar();
//        sar.max = max;sar.no=no;sar.refno = indication;
        sar.set(max,no,indication);
        h.addEI(sar);

        //����WDPԪ��
        TP_UDH_EI_Wdp wdp = new TP_UDH_EI_Wdp();
        h.addEI(wdp);
        return h.getPdu();
    }

    public static void main(String[] args){
//        SMSMsgWapPush w = new SMSMsgWapPush("+8613811256356","�Ѻ��ܲ��ų������ʹ��͵���������뿪�ļ��㷨�׶�  �������ֵ�����","wap.sohu.com");
//        System.out.print(w.getPdu());
    }
}
