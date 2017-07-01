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
     * ������ת��Ϊ��׼����λ��ʾ��ʮ�����ƴ�
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
            int len = pdu.length()/2;//�õ�������8λ�ַ���
            len = len*8;//�õ�������λ��
            int l = len/7;//�õ���λ�ַ�����Ŀ��������������Ļ���Ҳ����Ϊ��λ���µ�
            return intToHexString(l);
        } else {
            return intToHexString(pdu.length() / 2);
        }
    }

    /**
     * ���ַ���ת��Ϊbcd�����ʽ��8421�룬���ݵ�λ��ǰ��ԭ��������Ҫ��żλ����
     * �������Ϊ��������β����"F"
     * Ȼ���ַ�������żλ����
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
     * ��16���Ʊ�ʾ���ַ���ת��Ϊ��Ӧ�ַ������ַ���
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
     * ���ֽ�����ת��Ϊ16���Ʊ�ʾ���ַ���
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
     * ���ַ���ת��Ϊ��Ӧ�ַ�����ʾ��16�����ַ���
     * Ϊ�˶�UniCode���д����ر�������һ���ַ���֧��gsm_encode
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
//        System.out.println(stringToHexString("��", "iso-8859-1"));
//        System.out.println(hexToString("3f", "iso-8859-1"));
//        System.out.println(stringToHexString("1", "gsm_unicode"));
            pdu = stringToHexString("12345678","gsm_default");
            System.out.println(pdu);
            System.out.println(hexToString(pdu,"gsm_default"));
    }

    /**
         �ַ����滻��ȫ��ƥ��Ľ����滻

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
     * ��pdu��ת��Ϊbyte����
     * @param pdu String
     * @return byte[]
     */
    private static byte[] hexStringToBytes(String pdu){
        byte oldBytes[];
        int i;
        oldBytes = new byte[pdu.length() / 2];
        //����Ӧ�ֽ������е��ֽ���Ϊ16���Ʊ�ʾ���ֽ���̬
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
     * רΪgsm_default�ַ�ת��������׼������Ӧ�ַ���gsm_default
     * @param pdu String
     * @return String
     */
    private static String hexToString_GSMDefault(String pdu) {
        String text;
        byte oldBytes[], newBytes[];
        BitSet bitSet;
        int i, j, value1, value2;
        //��ȡ�ֽ����鳤��
        oldBytes = hexStringToBytes(pdu);

        //�򵥽�byte����ת��ΪbitSet,��û���κ�λ��
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

        //�����µ��ֽ�������Ҫ�ĳ���
        value2 = value1 / 7;
        if (value2 == 0) {
            value2++;
        }

        //���������飬����bitSet�е�������ͨ����λ�ķ�ʽ��һλһλ���ý�ȥ
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
        //������һ���ַ�Ϊ0������
        if (newBytes[value2 - 1] == 0) {
            text = new String(newBytes, 0, value2 - 1);
        } else {
            text = new String(newBytes);
        }
        return text;

    }

    /**
     * רΪUnicode�ַ�ת��������׼������Ӧ�ַ���gsm_unicode
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
     * ר�Ŷ�����Unicode�ַ����д��������Unicode�ַ�ָ16λ��
     * Ҳ���������ֽڱ�ʾ��Unicode�ַ�������û��ֱ��ת���ķ�����
     * �Լ���д�������Ĵ�����
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

        //�ֽ��ϴ�Ϊbitset
        oldBytes = hexStringToBytes(pdu);
        //��bitSet���д���
        bitSet = new BitSet(pdu.length()*8+offsetNumber);
        //�򵥽�byte����ת��ΪbitSet,��û���κ�λ��
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

        //�����µ��ֽ�������Ҫ�ĳ���
        value2 = value1 / 8;
        if(value1 % 8 != 0){
            value2++;
        }
        if (value2 == 0) {
            value2++;
        }

        //���������飬����bitSet�е�������ͨ����λ�ķ�ʽ��һλһλ���ý�ȥ
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

//        //������һ���ַ�Ϊ0������
        pdu = bytesToHexString(newBytes);
        while(pdu.length()>0&&(pdu.substring(pdu.length()-2).equals("00"))){
            pdu = pdu.substring(0,pdu.length()-2);
        }
        return pdu;
    }

    /**
     * ��������Ϊ7λӢ�ı����ʱ��
     * @param text String
     * @return String
     */
    private static String stringToHexString_GSMDefault(String text) {
        byte[] oldBytes, newBytes;
        BitSet bitSet;
        int i, j, value1, value2;

        //�ֽ��ϴ�Ϊbitset
        oldBytes = text.getBytes();
        bitSet = new BitSet(text.length() * 8);

        //��bitSet��λ�����д�8λ������λ�ַ��ı�ʾת��
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

        //�ж��ַ�����λ֮���Ƿ���Ҫ��0�������´����ֽڳ���
        if (((value1 / 56) * 56) != value1) {
            value2 = (value1 / 8) + 1;
        } else {
            value2 = (value1 / 8);
        }
        if (value2 == 0) {
            value2 = 1;
        }

        //�γ��´��ֽ�����
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

        //���´����ֽ�����ת��Ϊ16���ƴ�
        return bytesToHexString(newBytes);
    }

    public static boolean isContainsChineseChar(String text) {
        //ͨ��������ַ��ж��Ƿ�������ģ��еĻ����������ַ���
        Pattern p = Pattern.compile("[\u4E00-\u9FA5]");
        Matcher m = p.matcher(text);
        if (m.find()) {
            return true;
        }
        return false;
    }

}
