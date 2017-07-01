package org.cross.sms;

//import com.rj.sms.msg.SMSUtils;
//import com.rj.sms.msg.SMSATCmds;
//import com.rj.sms.msg.SMSMsgWapPush;

import org.cross.sms.msg.*;
import org.cross.sms.serialPort.*;
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
public class TestSendSI {
    public TestSendSI() {
    }

    public static void main(String[] args) {
        sendMsg();
    }

    static void sendMsg() {
        try {
            SmsUtil.loadLib();
            SMSPorter port = new SMSPorter("COM4");
            port.open();
            //begin process the msg
            String phoneno =
                    "+8613811256356";
            //get pdu text
            SMSMsgOut msg = new SMSMsgOut(phoneno, "我测试我测试");
            port.getResponse();

//            port.CMD_setPDUMode();
//            port.getResponse();
//            port.sendPduText(msg.getPdu());
//            port.getResponse();
//                Thread.sleep(500);
//            port.getResponse();

            port.close();
        } catch (Exception ex) {
            ex.printStackTrace();
        }

    }

    //I will try to send msg in si model
    static void sendSI() {
        try {
//            SMSUtils.loadLib();
//                SmsServer s = new SmsServer();
//                s.setComName("COM8");
//                s.start();
            SmsUtil.loadLib();
            SMSPorter port = new SMSPorter("COM8");
            port.open();
            //begin process the msg
            String phoneno =
                    //"+8613331118522";
//                    "+8613811256356";
                    "+8613811585934";
//                    "+8613436538348";
//                    "+8613910226916";
            String url =
                    "211.136.153.30/wappush/pushIndex.jsp?pushId=05051114160521";
            url = "219.142.171.117/1.gif";
            //get pdu text
            SMSMsgWapPush msg = new SMSMsgWapPush(phoneno, "测试了", url);

            Vector v = SmsUtil.buildWapMsg("+8613811256356", "wap.sohu.com",
                                           "搜狐总裁张朝阳大发送大发送到风大撒发离开的计算法伦敦  发到三分第三的");

            for (int i = 0; i < v.size(); i++) {
                port.CMD_setPDUMode();
//                port.getResponse();
                SMSMsg m = (SMSMsg) v.get(i);
                port.sendPduText(m.getPdu());
//                port.getResponse();
                Thread.sleep(500);
            }
//            port.sendPduText(msg.getPdu());
//            port.getResponse();
//            port.sendPduText(SMSMsgWapPush.encloser("29060603AE81EA8DCA02056A0045C6080C037761702e736f68752e636f6d000103e6909ce78b90e680bbe8a381e5bca0e69c9de998b3e5a4a7e7acace4b889e58f91e98081e5a4a7e58f91e98081e588b0e9a38ee5a4a7e69292e58f91e7a6bbe5bc80",
//                             phoneno,2,1));
//            port.getResponse();
//            port.sendPduText(SMSMsgWapPush.encloser("e79a84e8aea1e7ae97e6b395e4bca6e695a62020e58f91e588b0e4b889e58886e7acace4b889e79a84000101",
//                             phoneno,2,2));
//            port.getResponse();

            port.close();
        } catch (Exception ex) {
            ex.printStackTrace();
        }

    }
}
