package org.cross.sms.msg;

import java.util.*;


/**
 This class represents an incoming SMS message, i.e. message read from the GSM device.

 @see	SMSMessage
 @see	OutgoingMessage
 @see	SMSService#readMessages(LinkedList, int)
 */
public class SMSMsgIn extends SMSMsg {
    public static final int CLASS_ALL = 0;
    public static final int CLASS_REC_UNREAD = 1;
    public static final int CLASS_REC_READ = 2;
    public static final int CLASS_STO_UNSENT = 3;
    public static final int CLASS_STO_SENT = 4;
    private String m_pdu = "";

    /**
     Default constructor of the class.

     @param	 date	the creation date of the message.
     @param	 originator	the originator's number.
     @param	 text	the actual text of the message.
     @param	 memIndex	the index of the memory location in the GSM device where this message is stored.

     <br><br>Notes:<br>
     <ul>
      <li>Phone numbers are represented in their international format (e.g. +306974... for Greece).</li>
     </ul>
     */
    public SMSMsgIn(Date date, String originator, String text,
                    int memIndex) {
        super(TYPE_INCOMING, date, originator, null, text, memIndex);
    }

    /**
     Extra constructor of the class.
     This constructor is used for STATUS-REPORT messages.
     */
    public SMSMsgIn(int messageType, int memIndex) {
        super(messageType, null, null, null, null, memIndex);
    }

    public SMSMsgIn(String pdu, int memIndex) {
        super(TYPE_INCOMING, null, null, null, null, memIndex);
        m_pdu = pdu;
        int index;

        //����������ĵ�ַ
        TP_Address_SMSC smsc = new TP_Address_SMSC();
        smsc.readPdu(pdu);
        index = smsc.getTotalLength();//ָ��ֱ�������������ĵ�ַ���֣�

        index += 2;//����bitmask����

        //�����ߵ�ַ����
        TP_Address_Da_Oa oa = new TP_Address_Da_Oa();
        oa.readPdu(pdu.substring(index));
        index += oa.getTotalLength();
        this.originator = oa.m_code;//���÷����ߺ���

        index += 2;//����Э���ʶ

        //�ַ��������
        TP_Dsc_CharSet encoding = new TP_Dsc_CharSet();
        encoding.readPdu(pdu.substring(index));
        index += encoding.getTotalLength();


        //ʱ�������
        TP_Scts_time t = new TP_Scts_time();
        t.readPdu(pdu.substring(index));
        index +=t.getTotalLength();
        this.date = t.getDate();//����ʱ���

        this.text = _CharacterUtil.hexToString(pdu.substring(index + 2),
                                             encoding.getMessageEncoding());
    }

    public String getPdu() {
        return m_pdu;
    }

}
