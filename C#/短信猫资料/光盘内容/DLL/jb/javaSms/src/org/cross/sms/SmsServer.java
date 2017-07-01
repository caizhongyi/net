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
    private Vector m_errBuf = new Vector();//����ʧ�ܵĶ���

    /**
     * ��ʼ��Server������com������Ҫ�İ�
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
        //Ϊ�˳����Ž��д���
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
     * ����server,�����ú�com��֮��
     * ������֮ǰ���������ò����ʣ�Ĭ��Ϊ9600
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
     * ��������server,��ִ��һ��ʱ��֮��
     * server��Ч�ʻή�ͣ����ǿ��Խ�����������server
     */
    public void restart() {
        stop();
        start();
    }
    /**
     * ֹͣserver,֪ͨ�����߳�ֹͣ������
     */
    public void stop() {
//        thread.m_port.close();
            if(thread != null){
                thread.setStop();
            }
    }

    String m_comName = "";
    /**
     * ����com������
     * @param comName String
     */
    public void setComName(String comName) {
        m_comName = comName;
    }

    int m_baut = 9600;
    /**
     * ���ò����ʣ�Ĭ��Ϊ9600
     * @param baut int
     */
    public void setBaut(int baut) {
        m_baut = baut;
    }
}
