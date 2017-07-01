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
public class TP_Dsc_CharSet implements TP_Element_Inter {
    String m_pdu = "00";
    String m_charset = "gsm_default";
    public TP_Dsc_CharSet() {
    }

    /**
     * getPdu
     *
     * @return String
     * @todo Implement this org.cross.sms.msg._SMS_Element_Inter method
     */
    public String getPdu() {
        return m_pdu;
    }

    /**
     * getTotalLength
     *
     * @return int
     * @todo Implement this org.cross.sms.msg._SMS_Element_Inter method
     */
    public int getTotalLength() {
        return 2;
    }

    /**
     * readPdu
     *
     * @param Pdu String
     * @todo Implement this org.cross.sms.msg._SMS_Element_Inter method
     */
    public void readPdu(String Pdu) {
        setPdu(Pdu.substring(0,2));
    }
    public void setPdu(String pdu){
        m_pdu = pdu;
        int protocol = Integer.parseInt(m_pdu, 16);
        switch (protocol & 0x0C) {
        case 0:
            m_charset = "gsm_default";
            break;
        case 4:
            m_charset = "iso-8859-1";
            break;
        case 8:
            m_charset = "gsm_unicode";
            break;
        }
    }

    public String getMessageEncoding(){
        return m_charset;
    }

    public void setWap(){
        m_pdu = "F5";
        m_charset = "iso_8859_1";
    }

    public void setMessageEncoding(String messageEncoding){
        m_charset = messageEncoding;
        int protocol = Integer.parseInt(m_pdu, 16);
        protocol = protocol & 0xF3;
        if(m_charset.equals("gsm_default")){

        }else if(m_charset.equals("gsm_unicode")){
            protocol = protocol | 8;
        }else {
            protocol = protocol | 4;
        }
        m_pdu = _CharacterUtil.intToHexString(protocol);
    }
}
