package org.cross.sms.msg;

import java.util.*;
import java.util.regex.Pattern;
import java.util.regex.Matcher;

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
public class _CharacterUtil {
    /**
     * 将整形转换为标准的两位表示的十六进制串
     * @param len int
     * @return String
     */
    public static String intToHexString(int len){
        String str1 = Integer.toHexString(len);
        if (str1.length() < 2) {
            str1 = "0" + str1;
        }
        return str1;
    }

    public static String getPduHexLength(String pdu, String encoding) {
        if (encoding.equals("gsm_default")) {
            int len = pdu.length()/2;//得到编码后的8位字符数
            len = len*8;//得到编码后的位数
            int l = len/7;//得到七位字符的数目，如果不能整除的话，也是因为补位导致的
            return intToHexString(l);
        } else {
            return intToHexString(pdu.length() / 2);
        }
    }

    /**
     * 将字符串转换为bcd编码格式，8421码，根据低位在前的原则，所以需要奇偶位互换
     * 如果长度为奇数，在尾部补"F"
     * 然后将字符串的奇偶位互换
     * @param s String
     * @return String
     */
    public static String toBCDFormat(String s) {
        String bcd;
        int i;

        if ((s.length() % 2) != 0) {
            s = s + "F";
        }
        bcd = "";
        for (i = 0; i < s.length(); i += 2) {
            bcd = bcd + s.charAt(i + 1) +
                  s.charAt(i);
        }
        return bcd;
    }


    /**
     * 将16进制表示的字符串转换为相应字符集的字符串
     * @param hex String
     * @param charSet String
     * @return String
     */

    public static String hexToString(String hex, String charSet) {
        if (charSet.equalsIgnoreCase("gsm_unicode")) {
            return hexToString_UniCode(hex);
        }

        if (charSet.equalsIgnoreCase("gsm_default")) {
            return hexToString_GSMDefault(hex);
        }

        try {
            byte[] by = new byte[hex.length() / 2];
            for (int i = 0; i < hex.length() / 2; i++) {
                int j = Integer.parseInt("" + hex.charAt(i * 2) +
                                         hex.charAt(i * 2 + 1), 16);
                by[i] = (byte) j;
            }
            return new String(by, charSet);
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }

    /**
     * 将字节数组转换为16进制表示的字符串
     * @param bys byte[]
     * @return String
     */
    public static String bytesToHexString(byte[] bys) {
        int len = bys.length;
        String pdu = "";
        String str1;
        for (int i = 0; i < len; i++) {
            str1 = Integer.toHexString((int) bys[i]);
            if (str1.length() != 2) {
                str1 = "0" + str1;
            }
            str1 = str1.substring(str1.length() - 2, str1.length());
            pdu += str1;
        }
        return pdu;

    }

    /**
     * 将字符串转换为相应字符集表示的16进制字符串
     * 为了对UniCode进行处理，特别增加了一种字符集支持gsm_encode
     * @param str String
     * @param charSet String
     * @return String
     */
    public static String stringToHexString(String str, String charSet) {
        if (charSet.equalsIgnoreCase("gsm_unicode")) {
            return stringToHexString_UniCode(str);
        }

        if (charSet.equalsIgnoreCase("gsm_default")) {
            return stringToHexString_GSMDefault(str);
        }

        try {
            byte[] by = str.getBytes(charSet);
            return bytesToHexString(by);
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }

    public static void main(String[] args) {
            String pdu = "0500032C020162B219AD66BBE172B0986C46ABD96EB81CCC65DFBCD8723DDBB5668FC96C7A1E9D56EBF46CB98BCDCE87E372759ACD66B3CB65366A1E168FC965F619AD56AFD968F71B1E97CFE9F5FA1D9FD787C56316996D9EA337A9F47AEC46CBE372FB9C5EB7DFF179BD5D3C26B3D5EF313BBD1EA3E57571DC3E4FDBD977BC5DEF168FC965F579F34AA7D7";
            _CharacterUtil.hexToString(pdu,"gsm_default");
            pdu = "0500032C020100B0986C46ABD96EB81C2C269BD16AB61B2E078BC966B49AED86CBC162B219AD66BBE172B0986C46ABD96EB81C2C269BD16AB61B2E078BC966B49AED86CBC162B219AD66BBE172B0986C46ABD96EB81C2C269BD16AB61B2E078BC966B49AED86CBC162B219AD66BBE172B0986C46ABD96EB81C2C269BD16AB61B2E078BC966B49AED86CBC162";
            _CharacterUtil.hexToString(pdu,"gsm_default");
//        System.out.println(stringToHexString("刘", "iso-8859-1"));
//        System.out.println(hexToString("3f", "iso-8859-1"));
//        System.out.println(stringToHexString("1", "gsm_unicode"));
            pdu = stringToHexString("12345678","gsm_default");
            System.out.println(pdu);
            System.out.println(hexToString(pdu,"gsm_default"));
    }

    /**
         字符串替换，全部匹配的进行替换

         @param	text	the initial text.
         @param	symbol	the string to be substituted.
         @param	value	the string that the "symbol" will be substituted with, in the "text" (all occurences).

         @return	the changed text.
     */
    public static String replaceSymbol(String text, String symbol,
                                       String value) {
        StringBuffer buffer;

        while (text.indexOf(symbol) >= 0) {
            buffer = new StringBuffer(text);
            buffer.replace(text.indexOf(symbol),
                           text.indexOf(symbol) + symbol.length(), value);
            text = buffer.toString();
        }
        return text;
    }

    /**
     * 将pdu串转换为byte数组
     * @param pdu String
     * @return byte[]
     */
    private static byte[] hexStringToBytes(String pdu){
        byte oldBytes[];
        int i;
        oldBytes = new byte[pdu.length() / 2];
        //将相应字节数组中的字节置为16进制表示的字节形态
        for (i = 0; i < pdu.length() / 2; i++) {
            oldBytes[i] = (byte) (Integer.parseInt(pdu.substring(i * 2,
                    (i * 2) + 1), 16) * 16);
            oldBytes[i] +=
                    (byte) Integer.parseInt(pdu.substring((i * 2) + 1,
                    (i * 2) + 2), 16);
        }
        return oldBytes;
    }


    /**
     * 专为gsm_default字符转换所做的准备，对应字符集gsm_default
     * @param pdu String
     * @return String
     */
    private static String hexToString_GSMDefault(String pdu) {
        String text;
        byte oldBytes[], newBytes[];
        BitSet bitSet;
        int i, j, value1, value2;
        //获取字节数组长度
        oldBytes = hexStringToBytes(pdu);

        //简单将byte数组转换为bitSet,并没做任何位移
        bitSet = new BitSet(pdu.length() / 2 * 8);
        value1 = 0;
        for (i = 0; i < pdu.length() / 2; i++) {
            for (j = 0; j < 8; j++) {
                value1 = (i * 8) + j;
                if ((oldBytes[i] & (1 << j)) != 0) {
                    bitSet.set(value1);
                }
            }
        }
        value1++;

//        for(int m =0;m<bitSet.length();m++){
//            System.out.print(bitSet.get(m)?"1":"0");
//        }
//        System.out.println("");

        //计算新的字节数组需要的长度
        value2 = value1 / 7;
        if (value2 == 0) {
            value2++;
        }

        //构建新数组，并将bitSet中的内容逐渐通过移位的方式，一位一位的置进去
        newBytes = new byte[value2];
        for (i = 0; i < value2; i++) {
            for (j = 0; j < 7; j++) {
                if ((value1 + 1) > (i * 7 + j)) {
                    if (bitSet.get(i * 7 + j)) {
                        newBytes[i] |= (byte) (1 << j);
                    }
                }
            }
        }
        //如果最后一个字符为0，丢弃
        if (newBytes[value2 - 1] == 0) {
            text = new String(newBytes, 0, value2 - 1);
        } else {
            text = new String(newBytes);
        }
        return text;

    }

    /**
     * 专为Unicode字符转换所做的准备，对应字符集gsm_unicode
     * @param pdu String
     * @return String
     */
    private static String hexToString_UniCode(String pdu) {
        int i, j, index = 0;
        String rtn = "";
        while (index < pdu.length()) {
            try {
                i = Integer.parseInt("" + pdu.charAt(index) +
                                     pdu.charAt(index + 1), 16);
                j = Integer.parseInt("" + pdu.charAt(index + 2) +
                                     pdu.charAt(index + 3), 16);
                rtn = rtn + (char) ((i * 256) + j);
                index += 4;
            } catch (Exception ex) {
                break;
            }
        }
        return rtn;
    }

    /**
     * 专门对中文Unicode字符进行处理，这里的Unicode字符指16位，
     * 也就是两个字节表示的Unicode字符，由于没有直接转换的方法，
     * 自己编写了这样的处理方法
     * @param str String
     * @return String
     */
    private static String stringToHexString_UniCode(String str) {
        String str2 = "";
        for (int i = 0; i < str.length(); i++) {
            char c = str.charAt(i);
            int high = (int) (c / 256);
            int low = c % 256;
            str2 = str2 +
                   ((Integer.toHexString(high).length() < 2) ?
                    "0" + Integer.toHexString(high) : Integer.toHexString(high));
            str2 = str2 +
                   ((Integer.toHexString(low).length() < 2) ?
                    "0" + Integer.toHexString(low) : Integer.toHexString(low));
        }
        return str2;

    }

    public static String offsetHexString(String pdu,int offsetNumber){
        byte[] oldBytes, newBytes;
        BitSet bitSet;
        int i, j, value1, value2;

        //分解老串为bitset
        oldBytes = hexStringToBytes(pdu);
        //对bitSet进行处理
        bitSet = new BitSet(pdu.length()*8+offsetNumber);
        //简单将byte数组转换为bitSet,并没做任何位移
        value1 = 0+offsetNumber;
        for (i = 0; i < pdu.length() / 2; i++) {
            for (j = 0; j < 8; j++) {
                value1 = (i * 8) + j+offsetNumber;
                if ((oldBytes[i] & (1 << j)) != 0) {
                    bitSet.set(value1);
                }
            }
        }
        value1++;

        //计算新的字节数组需要的长度
        value2 = value1 / 8;
        if(value1 % 8 != 0){
            value2++;
        }
        if (value2 == 0) {
            value2++;
        }

        //构建新数组，并将bitSet中的内容逐渐通过移位的方式，一位一位的置进去
        newBytes = new byte[value2];
        for (i = 0; i < value2; i++) {
            for (j = 0; j < 8; j++) {
                if ((value1 + 1) > (i * 8 + j)) {
                    if (bitSet.get(i * 8 + j)) {
                        newBytes[i] |= (byte) (1 << j);
                    }
                }
            }
        }

//        //如果最后一个字符为0，丢弃
        pdu = bytesToHexString(newBytes);
        while(pdu.length()>0&&(pdu.substring(pdu.length()-2).equals("00"))){
            pdu = pdu.substring(0,pdu.length()-2);
        }
        return pdu;
    }

    /**
     * 发送内容为7位英文编码的时候
     * @param text String
     * @return String
     */
    private static String stringToHexString_GSMDefault(String text) {
        byte[] oldBytes, newBytes;
        BitSet bitSet;
        int i, j, value1, value2;

        //分解老串为bitset
        oldBytes = text.getBytes();
        bitSet = new BitSet(text.length() * 8);

        //将bitSet置位，进行从8位串到七位字符的表示转换
        value1 = 0;
        for (i = 0; i < text.length(); i++) {
            for (j = 0; j < 7; j++) {
                value1 = (i * 7) + j;
                if ((oldBytes[i] & (1 << j)) != 0) {
                    bitSet.set(value1);
                }
            }
        }
        value1++;

        //判断字符串移位之后是否需要补0，计算新串的字节长度
        if (((value1 / 56) * 56) != value1) {
            value2 = (value1 / 8) + 1;
        } else {
            value2 = (value1 / 8);
        }
        if (value2 == 0) {
            value2 = 1;
        }

        //形成新串字节数组
        newBytes = new byte[value2];
        for (i = 0; i < value2; i++) {
            for (j = 0; j < 8; j++) {
                if ((value1 + 1) > ((i * 8) + j)) {
                    if (bitSet.get(i * 8 + j)) {
                        newBytes[i] |= (byte) (1 << j);
                    }
                }
            }
        }

        //将新串的字节数组转换为16进制串
        return bytesToHexString(newBytes);
    }

    public static boolean isContainsChineseChar(String text) {
        //通过传入的字符判断是否具有中文，有的话设置中文字符集
        Pattern p = Pattern.compile("[\u4E00-\u9FA5]");
        Matcher m = p.matcher(text);
        if (m.find()) {
            return true;
        }
        return false;
    }

}
