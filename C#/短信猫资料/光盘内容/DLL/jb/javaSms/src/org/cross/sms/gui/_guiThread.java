package org.cross.sms.gui;

import org.cross.sms.*;

class _guiThread extends Thread {
    SmsServer m_server;
    GUIMain m_frame;
    public _guiThread(SmsServer port, GUIMain frame) {
        m_server = port;
        m_frame = frame;
    }

    public void run() {
        int j = 0;
        while (true) {
            try {
                Thread.sleep(2000);
                SimpleMsg msg = m_server.readInMsg();
//                j++; //开始设置循环次数
                while (msg != null) {
                    StringBuffer buf = new StringBuffer();

                    buf.append(msg.getSender());
                    buf.append("\n");
                    buf.append(msg.getText());
                    buf.append("\n");
                    m_frame.appendResp(buf.toString());
                    new Sound(Sound.MSG).start();
                    m_frame.toFront();

                    Thread.sleep(1000);
                    msg = m_server.readInMsg();
                }

                msg = m_server.readErrMsg();
                while (msg != null) {
                    StringBuffer buf = new StringBuffer();
                    buf.append("以下短信发送失败：\n");
//                    buf.append(msg.getSender());
//                    buf.append("\n");
                    buf.append(msg.getText());
                    buf.append("\n");
                    m_frame.appendResp(buf.toString());
                    new Sound(Sound.MSG).start();
                    m_frame.toFront();

                    Thread.sleep(1000);
                    msg = m_server.readInMsg();
                }
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
    }

}
