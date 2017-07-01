package org.cross.sms.msg;

import java.util.*;
import java.util.regex.*;

/**
 �ⷢ����Ϣ
 */
public class SMSMsgOut extends SMSMsg {
    private Date dispatchDate;
    public static int msgIndicator = 1;
    boolean m_withUserHeader = false;
    int max;
    int no;
    int indication;

    public SMSMsgOut() {
        super(TYPE_OUTGOING, null, null, null, null, -1);
        setSendDate(null);
        setDate(new Date());
    }

    public SMSMsgOut(String recipient, String text) {
        super(TYPE_OUTGOING, new Date(), null, recipient, text, -1);
        if (_CharacterUtil.isContainsChineseChar(text)) {
            this.setMessageEncoding("gsm_unicode");
        }
        setSendDate(null);
        setDate(new Date());
    }

    public void setSendDate(Date date) {
        this.dispatchDate = date;
    }

    public Date getSendDate() {
        return dispatchDate;
    }


    public String getPdu() {
        String pdu = getSMSHeader(); ;
        String str1, str2;
        //׼���û����ݲ��֣���������
        if (m_withUserHeader) {
            str2 = this.getDataHeader();
            str2 += getUserDataPdu();
        } else {
            str2 = getUserDataPdu();
        }
        str1 = _CharacterUtil.getPduHexLength(str2, messageEncoding);
        pdu = pdu + str1 + str2;
        return pdu.toUpperCase();
    }

    public String getSMSHeader() {
        String pdu = "";
        TP_Address_SMSC smsc = new TP_Address_SMSC();
        smsc.setPhone(SmsUtil.SMSC_CODE);
        pdu = pdu + smsc.getPdu();
        //�ж��Ƿ���Ҫ���ӱ�ͷ
        TP_FirstOctet first = new TP_FirstOctet();
        first.set(m_withUserHeader, false);
        pdu = pdu + first.getPdu();

        pdu = pdu + "00"; //sms refno
        //Ŀ���ַ
        TP_Address_Da_Oa da = new TP_Address_Da_Oa();
        da.setPhone(recipient);
        pdu = pdu + da.getPdu();

        pdu = pdu + "00"; //tp_pid

        pdu = pdu + getTCSPdu();
        //tp_pv��������
        pdu = pdu + "FF";
        return pdu;
    }

    public String getTCSPdu(){
            //�ַ���tp_dcs
            TP_Dsc_CharSet encoding = new TP_Dsc_CharSet();
            encoding.setMessageEncoding(messageEncoding);
            return encoding.getPdu();
    }

    public String getDataHeader() {
        TP_UDH h = new TP_UDH();
        //����sarԪ��
        TP_UDH_EI_Sar sar = new TP_UDH_EI_Sar();
        sar.max = max;
        sar.no = no;
        sar.refno = indication;
        h.addEI(sar);
        return h.getPdu();
    }

    public String getUserDataPdu() {
        String tmp = _CharacterUtil.stringToHexString(text, messageEncoding);
        if (m_withUserHeader && messageEncoding.equals("gsm_default")) {
            //��һλƫ���Ա�֤��֧�ֶ���ƴ�ӵ��ֻ��ܹ��յ�
            tmp = _CharacterUtil.offsetHexString(tmp, 1);
        }
        return tmp;
    }

    public static void main(String[] args) {
        SMSMsgOut m = new SMSMsgOut("", "liuxk");
        System.out.println(m.getMessageEncoding());
    }

//    private boolean statusReport =false;
//    /**
//     Sets if a status report is requested.
//
//     @param statusReport True if a status report is requested. Default is false (no status report).
//     */
//    public void setStatusReport(boolean statusReport) {
//        this.statusReport = statusReport;
//    }
}
