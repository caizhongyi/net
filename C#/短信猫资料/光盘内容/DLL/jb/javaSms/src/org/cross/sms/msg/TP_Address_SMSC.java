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
public class TP_Address_SMSC extends TP_Address {
    public TP_Address_SMSC() {
        super();
    }

    public int getTotalLength() {
        return m_len * 2 + 2;
    }

    public void setPhone(String phone){
        init();
        m_code = phone;
        String str1, str2, pdu = "";
        if (phone == null) {
            m_pdu ="";
            return;
        }
        if ((phone != null) && (phone.length() == 0)) {
            m_pdu = "00";
            return;
        }
        str1 = "91" + _CharacterUtil.toBCDFormat(phone.substring(1));//È¥³ý+ºÅ
        str2 = Integer.toHexString(str1.length() / 2);
        m_type = "91";
        m_len = str1.length()/2;
        if (str2.length() != 2) {
            str2 = "0" + str2;
        }
        pdu = pdu + str2 + str1;
        m_pdu = pdu;
    }


}
