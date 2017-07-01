package org.cross.sms.msg;

/**
 * <p>Title:可拼接短信头拼接元素 </p>
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
public class TP_UDH_EI_Sar implements TP_Element_Inter {
    String m_pdu ="0003000101";
    int max,no,refno;
    public TP_UDH_EI_Sar() {
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
        return 10;
    }

    /**
     * readPdu
     *
     * @param Pdu String
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public void readPdu(String Pdu) {
        m_pdu = Pdu.substring(0,10);
        max = Integer.parseInt(m_pdu.substring(6,8),16);
        no = Integer.parseInt(m_pdu.substring(8,10),16);
        refno = Integer.parseInt(m_pdu.substring(4,6),16);
    }

    public void set(int max,int no,int refno){
        this.max = max;
        this.no = no;
        this.refno = refno;
        m_pdu = "0003";

        m_pdu = m_pdu + _CharacterUtil.intToHexString(refno);
        m_pdu = m_pdu + _CharacterUtil.intToHexString(max);
        m_pdu = m_pdu + _CharacterUtil.intToHexString(no);
        //位偏移动作移到getPdu的过程中去了
    }
}
