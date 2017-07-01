package org.cross.sms.msg;

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
public class TP_Address_Da_Oa extends TP_Address{
    public TP_Address_Da_Oa() {
        super();
    }
    public int getTotalLength() {
        int len = m_len;
        if(len % 2>0){
            len++;
        }
        return len +4;
    }

    public void setPhone(String phone){
        init();
        m_code = phone;

        if (phone == null) {
            m_pdu ="";
            return;
        }
        if ((phone != null) && (phone.length() == 0)) {
            m_pdu = "00";
            return;
        }

        String str1, str2, pdu = "";
        str1 = phone;

        if (str1.charAt(0) == '+') {
            str1 = _CharacterUtil.toBCDFormat(str1.substring(1));
            str2 = Integer.toHexString(phone.length() - 1);
            str1 = "91" + str1; //手机
            m_type = "91";
        } else {
            str1 = _CharacterUtil.toBCDFormat(str1);
            str2 = Integer.toHexString(phone.length());
            str1 = "81" + str1; //小灵通
            m_type = "81";
        }
        m_len = phone.length();
        if (str2.length() != 2) {
            str2 = "0" + str2;
        }

        pdu = pdu + str2 + str1;
        m_pdu = pdu;

    }

}
