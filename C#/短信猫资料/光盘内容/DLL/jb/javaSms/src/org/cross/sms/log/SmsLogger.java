package org.cross.sms.log;

import java.io.*;

import org.cross.sms.msg.*;

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
public class SmsLogger {
    private BufferedWriter in;
    private BufferedWriter out;
    private static SmsLogger m_instance;
    public static SmsLogger getInstance() {
        if (m_instance == null) {
            m_instance = new SmsLogger();
        }
        return m_instance;
    }

    private SmsLogger() {
        super();
        try {
            in = new BufferedWriter(new FileWriter("in", true));

            out = new BufferedWriter(new FileWriter("out", true));
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public void logInSmsMsg(SMSMsg m) {
        try {
            in.write("Sender:" + m.getOriginator());
            in.write(" Date:" + m.getDate());
            in.newLine();
            in.write("pdu:" + m.getPdu());
            in.newLine();
            in.write("text:" + m.getText());
            in.newLine();
            in.flush();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public void logOutSmsMsg(SMSMsg m) {
        try {
            out.write("receiver:" + m.getRecipient());
            out.write(" Date:" + m.getDate());
            out.newLine();
            out.write("pdu:" + m.getPdu());
            out.newLine();
            out.write("text:" + m.getText());
            out.newLine();
            out.flush();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}
