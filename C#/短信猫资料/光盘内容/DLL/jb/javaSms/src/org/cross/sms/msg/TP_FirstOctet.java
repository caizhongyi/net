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
public class TP_FirstOctet implements TP_Element_Inter {
    private String m_pdu = "11";
    private boolean m_withHeader = false;
    private boolean m_isInMsg = false;
    public TP_FirstOctet() {
    }

    /**
     * getPdu
     *
     * @return String
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public String getPdu() {
        return m_pdu;
    }

    /**
     * getTotalLength
     *
     * @return int
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public int getTotalLength() {
        return 2;
    }

    /**
     * readPdu
     *
     * @param Pdu String
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public void readPdu(String Pdu) {
        m_pdu = Pdu.substring(0,2);
        int type = Integer.parseInt(m_pdu,16) & 0x03;
        if(type == 0){
            m_isInMsg = true;
        }else if(type == 1){
            m_isInMsg = false;
        }
    }

    public void set(boolean withHeader,boolean isInMsg){
        m_withHeader = withHeader;
        m_isInMsg = isInMsg;
        int typepdu = 0;
        //build pdu
        if(!m_isInMsg){
            typepdu=0x11;
        }

        if(m_withHeader){
            typepdu = typepdu | 0x40;
        }
        m_pdu = _CharacterUtil.intToHexString(typepdu);
    }
}
