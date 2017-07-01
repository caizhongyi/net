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
public interface TP_Element_Inter {
    int getTotalLength();
    void readPdu(String Pdu);
    String getPdu();
}
