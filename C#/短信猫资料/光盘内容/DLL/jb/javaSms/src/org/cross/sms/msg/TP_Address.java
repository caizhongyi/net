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
public abstract class TP_Address implements TP_Element_Inter{
    String m_code ;
    String m_type ;
    String m_pdu;
    int m_len;

    public TP_Address() {
        init();
    }

    public void readPdu(String pdu) {
        m_len = Integer.parseInt(pdu.substring(0, 2), 16);
        m_pdu = pdu.substring(0, getTotalLength());
        if (m_len > 0) {
            m_type = m_pdu.substring(2, 4);
            m_code = m_pdu.substring(4);

            //特殊处理，针对地址类型进行处理
            // Type of Address，处理bitmask
            int addr = Integer.parseInt(m_type, 16);
            if ((addr & (1 << 6)) != 0 && (addr & (1 << 5)) == 0 &&
                (addr & (1 << 4)) != 0) {
                //Alphanumeric, (coded according to GSM TS 03.38 7-bit default alphabet)
                String str1 = _CharacterUtil.hexToString(m_code, "gsm_default");
                String originator = "";
                for (int i = 0; i < str1.length(); i++) {
                    if ((int) str1.charAt(i) == 27) {
                        originator +=
                                SMSGSMEncoding.hex2ExtChar((int) str1.charAt(++
                                i),
                                SMSGSMEncoding.GSM7BITDEFAULT);
                    } else {
                        originator +=
                                SMSGSMEncoding.hex2Char((int) str1.charAt(i),
                                SMSGSMEncoding.GSM7BITDEFAULT);
                    }
                }
                m_code = originator;
            }
            //else if ( (addr & (1 << 6)) == 0 && (addr & (1 << 5)) == 0 && (addr & (1 << 4)) != 0) originator = "+" + originator;
            //最为简单的号码类型
            else {
                m_code = _CharacterUtil.toBCDFormat(m_code);
                if (m_code.endsWith("F"))
                    m_code = m_code.substring(0,m_code.length() - 1);

            }
        }
    }

    public String getPdu() {
        return m_pdu;
    }

    protected void init(){
        m_code = "";
        m_len = 0;
        m_type = "";
        m_pdu = "00";

    }
    public abstract int getTotalLength() ;
    public abstract void setPhone(String phone);

}
