package org.cross.sms;
import org.cross.sms.msg.*;
import java.util.*;
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
public class SimpleMsg {
    SMSMsg m_msg;
    public SimpleMsg(SMSMsg m) {
        m_msg = m;
    }
    public String getText(){
        return m_msg.getText();
    }

    public String getSender(){
        return m_msg.getOriginator();
    }

    public String getReceiver(){
        return m_msg.getRecipient();
    }

    public Date getDate(){
        return m_msg.getDate();
    }
}
