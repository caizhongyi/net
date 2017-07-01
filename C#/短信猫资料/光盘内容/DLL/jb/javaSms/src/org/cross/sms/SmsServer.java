package org.cross.sms;

import java.util.*;

import org.cross.sms.msg.*;
import org.cross.sms.serialPort.*;

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
public class SmsServer {
    _ServerSmsThread thread;
    private Vector m_inBuf = new Vector();
    private Vector m_outBuf = new Vector();
    private Vector m_errBuf = new Vector();//发送失败的短信

    /**
     * 初始化Server，加载com口所需要的包
     */
    public SmsServer() {
        SmsUtil.loadLib();
    }

    protected void pushErrMsg(SMSMsg msg){
        m_errBuf.add(msg);
    }

    protected SMSMsg popErrMsg(){
        int len = m_errBuf.size();
        if (len > 0) {
            SMSMsg msg = (SMSMsg) m_errBuf.get(0);
            m_errBuf.remove(msg);
            return msg;
        }
        return null;
    }

    protected void pushInMsg(SMSMsg msg) {
        m_inBuf.add(msg);
    }

    protected SMSMsg popInMsg() {
        int len = m_inBuf.size();
        if (len > 0) {
            SMSMsg msg = (SMSMsg) m_inBuf.get(0);
            m_inBuf.remove(msg);
            return msg;
        }
        return null;
    }

    protected void pushOutMsg(SMSMsg msg) {
        m_outBuf.add(msg);
    }

    protected SMSMsg popOutMsg() {
        int len = m_outBuf.size();
        if (len > 0) {
            SMSMsg msg = (SMSMsg) m_outBuf.get(0);
            m_outBuf.remove(msg);
            return msg;
        }
        return null;
    }

    public void send(String receiver, String text) {
//        SMSMsgOut out = new SMSMsgOut(receiver, text);
        //为了长短信进行处理
        Vector v = SmsUtil.buildMsgOut(receiver,text,true);
//                   buildConcatenatedMsgOut(receiver,text);

        for(int i=0;i<v.size();i++){
            SMSMsgOut out = (SMSMsgOut)v.get(i);
            pushOutMsg(out);
        }
    }

    public void sendWapPush(String receiver,String text,String url){
        Vector v = SmsUtil.buildWapMsg(receiver,url,text);
        for(int i=0;i<v.size();i++){
            SMSMsgOut out = (SMSMsgOut)v.get(i);
            pushOutMsg(out);
        }
    }

    public SimpleMsg readInMsg(){
        SMSMsg m = popInMsg();
        if(m != null)
            return new SimpleMsg(m);
        return null;
    }

    public SimpleMsg readErrMsg(){
        SMSMsg m = popErrMsg();
        if(m != null)
            return new SimpleMsg(m);
        return null;
    }

    /**
     * 启动server,在设置好com口之后，
     * 在启动之前还可以设置波特率，默认为9600
     */
    public void start() {
        SMSPorter port = new SMSPorter(m_comName);
        port.setBaud(m_baut);
        try {
            port.open();
            port.CMD_setStorageToMT();
            thread = new _ServerSmsThread(port, this);
            thread.start();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    /**
     * 重新启动server,在执行一段时间之后，
     * server的效率会降低，我们可以进行重新启动server
     */
    public void restart() {
        stop();
        start();
    }
    /**
     * 停止server,通知工作线程停止动作。
     */
    public void stop() {
//        thread.m_port.close();
            if(thread != null){
                thread.setStop();
            }
    }

    String m_comName = "";
    /**
     * 设置com口名称
     * @param comName String
     */
    public void setComName(String comName) {
        m_comName = comName;
    }

    int m_baut = 9600;
    /**
     * 设置波特率，默认为9600
     * @param baut int
     */
    public void setBaut(int baut) {
        m_baut = baut;
    }
}
