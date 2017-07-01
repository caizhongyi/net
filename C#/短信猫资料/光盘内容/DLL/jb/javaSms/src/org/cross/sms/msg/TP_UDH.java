package org.cross.sms.msg;
import java.util.*;
/**
 * <p>Title: </p>
 *
 * <p>Description: �û�����ͷ</p>
 *
 * <p>Copyright: Copyright (c) 2005</p>
 *
 * <p>Company: </p>
 *
 * @author not attributable
 * @version 1.0
 */
public class TP_UDH implements TP_Element_Inter {
    String m_pdu = "00";
    Vector m_eis = new Vector();
    int m_len = 0;
    public TP_UDH() {
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
        return m_len*2 + 2;
    }

    /**
     * readPdu û����ȫʵ��
     *
     * @param Pdu String
     * @todo Implement this org.cross.sms.msg.TP_Element_Inter method
     */
    public void readPdu(String Pdu) {
        m_len = Integer.parseInt(Pdu.substring(0,2),16);
        m_pdu = Pdu.substring(0,m_len*2+2);
        //readPdu
        throw new RuntimeException(this.getClass().getName()+" readPdu method not implement");
    }

    /**
     * ����һ��ͷԪ��
     * @param ele TP_Element_Inter
     */
    public void addEI(TP_Element_Inter ele){
        m_eis.add(ele);
        if(m_pdu.length() >=2){
            m_pdu = m_pdu.substring(2);
        }
        m_pdu += ele.getPdu();
        m_pdu = _CharacterUtil.intToHexString(m_pdu.length()/2)+m_pdu;
    }
}
