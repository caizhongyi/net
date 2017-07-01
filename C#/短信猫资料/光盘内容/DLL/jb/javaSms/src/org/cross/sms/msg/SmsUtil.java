package org.cross.sms.msg;

import javax.comm.*;
import java.util.Vector;


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
public class SmsUtil {
    public static String SMSC_CODE = "";
//            "+8613800100500";
    public static int msgIndicator = 1;
    /**
     * 加载Lib库
     */
    public static void loadLib() {
        //初始化加载dll
        String driverName = "com.sun.comm.Win32Driver";
        CommDriver driver = null;

        try {
            System.loadLibrary("win32com");
            driver = (CommDriver) Class.forName(driverName).newInstance();
            driver.initialize();
        } catch (InstantiationException e1) {
            System.out.println("1:" + e1.getMessage());

        } catch (IllegalAccessException e1) {
            System.out.println("2:" + e1.getMessage());

        } catch (ClassNotFoundException e1) {
            System.out.println(e1.getMessage());
        }
    }

    public static int getSMSSize(String pdu) {
        int j = pdu.length();
        j /= 2;
        if (SMSC_CODE == null) {
            ;
        } else if (SMSC_CODE.length() == 0) {
            j--;
        } else {
            j -= ((SMSC_CODE.length() - 1) / 2);
            j -= 2;
        }

        /////
        j = j - 1;
        return j;
    }

    /**
     * Checks if the message is SMS-DELIVER.
     *
     * @author George Karadimas
     * @param pdu the message pdu
     * @return true if the message is SMS-DELIVER
     */
    public static boolean isSMSMsgIn(String pdu) {
        int index, i;

        i = Integer.parseInt(pdu.substring(0, 2), 16);
        index = (i + 1) * 2;

        i = Integer.parseInt(pdu.substring(index, index + 2), 16);
        if ((i & 0x03) == 0) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * 构建不可以拼接的长短信
     * @param receiver String
     * @param text String
     * @return Vector
     */
    public static Vector buildMsgOut(String receiver, String text,boolean concacencted) {
        Vector v = new Vector();
        int maxLen = 160;
        String encoding = "gsm_default";
        //中文处理
        if (_CharacterUtil.isContainsChineseChar(text)) {
            //中文不能超过70个字符长度
            maxLen = 70;
            encoding = "gsm_unicode";
        }

        //如果需要构建可拼接短信
        if(text.length()>maxLen && concacencted){
            return buildConcatenatedMsgOut(receiver,text);
        }

        //否则按照普通短信进行处理
        //统一进行处理
        String tmp = text;
        while (tmp != null) {
            if (tmp.length() > maxLen) {
                String str = tmp.substring(0, maxLen);
                tmp = tmp.substring(maxLen);
                SMSMsgOut m = new SMSMsgOut(receiver, str);
                m.setMessageEncoding(encoding);
                v.add(m);
            } else {
                SMSMsgOut m = new SMSMsgOut(receiver, tmp);
                m.setMessageEncoding(encoding);
                v.add(m);
                tmp = null;
            }
        }
        return v;
    }
    /**
     * 构建可以拼接的长短信
     * @param receiver String
     * @param text String
     * @return Vector
     */
    public static Vector buildConcatenatedMsgOut(String receiver, String text) {
        Vector v = new Vector();
        int maxLen = 152;
        String encoding = "gsm_default";
        //中文处理
        if (_CharacterUtil.isContainsChineseChar(text)) {
            //中文不能超过70个字符长度
            maxLen = 67;
            encoding = "gsm_unicode";
        }
        //统一进行处理
        String tmp = text;
        int max = text.length()/maxLen;
        if(text.length()%maxLen >0 ){
            max++;
        }
        //获取标识
        int indication = 0;
        if(max>0){
            indication = getNextMsgIndicator();
        }
        int no = 1;
        while (tmp != null) {
            if (tmp.length() > maxLen) {
                String str = tmp.substring(0, maxLen);
                tmp = tmp.substring(maxLen);
                SMSMsgOut m = new SMSMsgOut(receiver, str);
                m.setMessageEncoding(encoding);
                m.m_withUserHeader = true;//指明需要数据报头
                m.indication = indication;
                m.max = max;
                m.no = no++;
                v.add(m);
            } else {
                SMSMsgOut m = new SMSMsgOut(receiver, tmp);
                m.setMessageEncoding(encoding);
                if(max >0){
                    m.m_withUserHeader = true; //指明需要数据报头
                    m.indication = indication;
                    m.max = max;
                    m.no = no++;
                }
                v.add(m);
                tmp = null;
            }
        }
        return v;
    }

    public static Vector buildWapMsg(String receiver,String url,String text){
        Vector v = new Vector();
        WapUrlPack p = new WapUrlPack(text,url);
        int count = p.getPackCount();
        int ind =getNextMsgIndicator();
        for(int i=0;i<count;i++){
            SMSMsgWapPush m = new SMSMsgWapPush(receiver,text,url);
            m.indication = ind;
            m.max = count;
            m.no = i+1;
            m.m_udpdu = p.getPackByNo(i);
            v.add(m);
        }
        return v;
    }

    private static int getNextMsgIndicator(){
        if(msgIndicator>254)msgIndicator=1;
        return msgIndicator++;

    }

}
