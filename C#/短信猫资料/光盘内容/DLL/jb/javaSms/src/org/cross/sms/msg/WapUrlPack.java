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


public class WapUrlPack {
    String m_url;
    String m_text;
    String m_pdu;
    public WapUrlPack(String text, String url) {
        m_text = text;
        m_url = url;
        buildPdu();
    }
    private void buildPdu(){
        String pdu = "";
        pdu = pdu + "29060603AE81EA8DCA"; // WSP
        pdu = pdu + "02"; // 标记位
        pdu = pdu + "05"; // -//WAPFORUM//DTD SI 1.0//EN
        pdu = pdu + "6A"; // UTF-8
        pdu = pdu + "00"; // 标记开始
        pdu = pdu + "45"; // <si>
        pdu = pdu + "C6"; // <indication
        pdu = pdu + "08"; // <action=signal-high>

        pdu = pdu + "0C"; // href="http://
        pdu = pdu + "03"; // 字符串开始
        pdu = pdu + _CharacterUtil.stringToHexString(m_url, "utf-8");
//        pdu = pdu + "3231312e3133362e3135332e33302f776170707573682f70757368496e6465782e6a73703f7075736849643d3035303531313134313630353231"; // URL
        pdu = pdu + "00"; // URL 字符串结束
        pdu = pdu + "01"; // >
        pdu = pdu + "03"; // 内容描述字符串开始
        pdu = pdu + _CharacterUtil.stringToHexString(m_text, "utf-8");
//        pdu = pdu + "E8AFB7E782B9E587BBE4BBA5E4B88BE993BEE68EA5E88EB7E58F96E5BDA9E4BFA1E58685E5AEB9";// 内容描述字符串

        pdu = pdu + "00"; // 内容描述字符串结束
        pdu = pdu + "01"; // </si>
        pdu = pdu + "01"; // </indication>
        m_pdu = pdu;
    }

    public String getPDU() {
       return m_pdu;
    }

    private int maxLength = 128;//140-12去除数据报头的长度之后得到的数据最大长度

    public int getPackCount(){
        int len =m_pdu.length()/2;
        int count = len/maxLength;
        if(len % maxLength >0)
            count ++;
        return count;
    }

    public String getPackByNo(int no){
        int count =getPackCount();
        if(no >=count){
            throw new RuntimeException("out of index");
        }
        if(count-1 == no){
            return m_pdu.substring(no*2*128);
        }
        return m_pdu.substring(no*2*128,no*2*128+256);
    }
}
