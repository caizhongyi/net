package org.cross.sms.msg;

/**
 * <p>Title: </p>
 *
 * <p>Description: 由于关于ei部分的内容不明确，所以不能写出完整的意义 </p>
 *
 * <p>Copyright: Copyright (c) 2005</p>
 *
 * <p>Company: </p>
 *
 * @author not attributable
 * @version 1.0
 */
public class TP_UDH_EI_Wdp implements TP_Element_Inter {
    public TP_UDH_EI_Wdp() {
    }

    /**
     * getPdu
     *
     * @return String
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public String getPdu() {
        return "05040B8423F0";
    }

    /**
     * getTotalLength
     *
     * @return int
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public int getTotalLength() {
        return 12;
    }

    /**
     * readPdu
     *
     * @param Pdu String
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public void readPdu(String Pdu) {
        //do nothing
    }
}
