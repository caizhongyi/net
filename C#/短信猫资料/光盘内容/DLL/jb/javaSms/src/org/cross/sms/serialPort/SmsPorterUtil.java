package org.cross.sms.serialPort;


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
class SmsPorterUtil {
    public SmsPorterUtil() {
    }

    /**
     * 判断接收到的数据是否最后是以"OK"结束的
     *
     * @param data
     * @return
     */
    public static boolean isRecOK(String data) {
        final String OK_FLAG = "OK";
        int index1 = 0;

        if (data != null) {
            index1 = data.indexOf(OK_FLAG);

            if (index1 >= 0 && index1 + 4 <= data.length()) {
                String t = data.substring(index1 + 2);
                byte[] b = t.getBytes();
                if (b.length >= 2) {
                    if (b[0] == 0x0D && b[1] == 0x0A) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /**
     * 发送短消息是否成功.
     * <p>
     * 判断依据: 收到回应的消息中有+CMGS:<space><number>,紧接着是两个换行回车(0x0D,0x0A,0x0D,0x0A),
     * 然后是OK,最后是一个回车换行(0x0D,0x0A)
     *
     * @param data
     * @return
     */
    public static boolean isSendOK(String data) {
        final String FLAG = "+CMGS:";
        int index = -1;
        int index2 = -1;

        if (data != null) {
            index = data.indexOf(FLAG);
            if (index > 0) {
                index += 6;
                if (index < data.length()) {
                    String temp = data.substring(index);
                    index = 0;
                    byte[] b = temp.getBytes();
                    for (int i = 0; i < b.length; i++) {
                        if (b[i] == 0x0D) {
                            index2 = i;
                            break;
                        }
                    }

                    if (index2 < temp.length() && index2 > index + 1) {
                        String t1 = temp.substring(index + 1, index2);

                        try {
                            int seqid = Integer.parseInt(t1);
                            System.out.println("seqID:" + seqid);

                            if (index2 + 8 == temp.length()) {
                                // 两个回车换行符
                                if (b[index2] == 0x0D && b[++index2] == 0x0A
                                    && b[++index2] == 0x0D
                                    && b[++index2] == 0x0A) {
                                    if (b[++index2] == 0x4F
                                        && b[++index2] == 0x4B) { // OK
                                        if (b[++index2] == 0x0D
                                            && b[++index2] == 0x0A) { // 一个回车换行
                                            return true;
                                        }
                                    }
                                }
                            }
                        } catch (NumberFormatException e) {
                            e.printStackTrace();
                            return false;
                        }
                    }
                }
            }
        }

        return false;
    }

    /**
     * 判断接收到的字符串最后是否是以"ERROR"结束的
     *
     * @param data
     * @return
     */
    public static boolean isRecError(String data) {

        final String FLAG = "ERROR";

        int index1 = 0;

        if (data != null) {
            index1 = data.indexOf(FLAG);

            if (index1 >= 0 && index1 + 7 <= data.length()) {
                String t = data.substring(index1 + 5);
                byte[] b = t.getBytes();
                if (b.length >= 2) {
                    if (b[0] == 0x0D && b[1] == 0x0A) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    /**
     * 是否接收到手机发来的完整数据,上传的数据是以"+CMT:"开头
     *
     * @param data
     * @return
     */
    public static boolean isRecData(String data) {
        final String BEGIN_FLAG = "+CMT:";
        int index0 = -1;

        if (data != null) {
            index0 = data.indexOf(BEGIN_FLAG);
            if (index0 >= 0 && index0 < data.length()) {
                return true;

            }
        }
        return false;
    }
}
