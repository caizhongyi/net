package org.cross.sms;

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
import java.util.*;

import org.cross.sms.log.*;
import org.cross.sms.msg.*;
import org.cross.sms.serialPort.*;

class _ServerSmsThread extends Thread {
    SMSPorter m_port;
    SmsServer m_server;
    boolean m_stillRun = true;
    public _ServerSmsThread(SMSPorter port, SmsServer server) {
        m_port = port;
        m_server = server;
    }

    public void run() {
        int j = 0;
        while (true && m_stillRun) {
            try {
                Thread.sleep(2000);
                m_port.CMD_getUnReadRecvMsg();
                LinkedList ll = null;
                ll = m_port.readPDUMsg();
                j++; //开始设置循环次数
                //开始接收短信
                for (int i = 0; i < ll.size(); i++) {
                    SMSMsgIn msg = (SMSMsgIn) ll.get(i);

                    m_server.pushInMsg(msg);
                    SmsLogger.getInstance().logInSmsMsg(msg);
                }
                //处理发送队列
                SMSMsg omsg = m_server.popOutMsg();
                while (omsg != null) {
                    m_port.CMD_setPDUMode();
                    m_port.getResponse();
                    //处理出错时的信息
                    boolean isok = m_port.sendPduText(omsg.getPdu().toUpperCase());
//                    m_port.getResponse();
                    if(!isok){
                        m_server.pushErrMsg(omsg);
                    }else{
                        SmsLogger.getInstance().logOutSmsMsg(omsg);
                    }
                    omsg = m_server.popOutMsg();
                }
                //当循环次数大于15
                if (j > 50) {
                    System.out.println("clear the readed buffer*****************");
                    m_port.CMD_delAllMsg();
                    j = 0;
                }

            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
        m_port.close();
    }
    public void setStop(){
        m_stillRun = false;
    }

}
