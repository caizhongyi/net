package org.cross.sms.msg;

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
class TP_Scts_time implements TP_Element_Inter{
    Calendar m_cal ;
    String m_scts = "";
    public TP_Scts_time() {
        super();
    }
    public int getTotalLength(){
        return 14;
    }

    public void readPdu(String pdu){
        String scts = pdu.substring(0,14);
        setSCTS(scts);
    }

    public String getPdu(){
        return getScts();
    }
    /**
     * 设置scts串，一般用于接收到的短信的时间戳处理对应gsm0340——9.2.3.11段TP_SCTS段
     * 串长度为14
     */
    public void setSCTS(String scts){
        m_scts = scts;
        int index =0;
        int year = Integer.parseInt("" + scts.charAt(index + 1) + scts.charAt(index));
        index += 2;
        int month = Integer.parseInt("" + scts.charAt(index + 1) + scts.charAt(index));
        index += 2;
        int day = Integer.parseInt("" + scts.charAt(index + 1) + scts.charAt(index));
        index += 2;
        int hour = Integer.parseInt("" + scts.charAt(index + 1) + scts.charAt(index));
        index += 2;
        int min = Integer.parseInt("" + scts.charAt(index + 1) + scts.charAt(index));
        index += 2;
        int sec = Integer.parseInt("" + scts.charAt(index + 1) + scts.charAt(index));
        index += 4;
        m_cal = Calendar.getInstance();
        m_cal.set(Calendar.YEAR, year + 2000);
        m_cal.set(Calendar.MONTH, month - 1);
        m_cal.set(Calendar.DAY_OF_MONTH, day);
        m_cal.set(Calendar.HOUR_OF_DAY, hour);
        m_cal.set(Calendar.MINUTE, min);
        m_cal.set(Calendar.SECOND, sec);
    }

    public void setDate(Date d){
        m_cal = Calendar.getInstance();
        m_cal.setTime(d);
        m_scts = "";
        m_scts += toNetByteString(m_cal.get(Calendar.YEAR)-2000);
        m_scts += toNetByteString(m_cal.get(Calendar.MONTH)+1);
        m_scts += toNetByteString(m_cal.get(Calendar.DAY_OF_MONTH));
        m_scts += toNetByteString(m_cal.get(Calendar.HOUR_OF_DAY));
        m_scts += toNetByteString(m_cal.get(Calendar.MINUTE));
        m_scts += toNetByteString(m_cal.get(Calendar.SECOND));
        m_scts += "23";//时区段转换方法不明
        m_scts = m_scts.toUpperCase();
    }

    /**
     * 高低位互换，网络模式，注意不是16进制串，而是10进制串
     * @param i int
     * @return String
     */
    private String toNetByteString(int i){
        String hex = ""+i;
        if(hex.length()<2)hex = 0+hex;
        StringBuffer b = new StringBuffer();
        for(int j=hex.length();j>0;j--){
            b.append(hex.charAt(j-1));
        }
        return b.toString();
    }

    public Date getDate(){
        return m_cal.getTime();
    }

    public String getScts(){
        return m_scts;
    }

    public static void main(String[] args) {
        try{
            Date d = new Date();
            System.out.println(d);
            TP_Scts_time m = new TP_Scts_time();
            m.setDate(d);
            System.out.println(m.getScts());
            m.setSCTS(m.getScts());
            System.out.println(m.getDate());
        }catch(Exception ex){
            ex.printStackTrace();
        }
    }
}
