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
     * �жϽ��յ��������Ƿ��������"OK"������
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
     * ���Ͷ���Ϣ�Ƿ�ɹ�.
     * <p>
     * �ж�����: �յ���Ӧ����Ϣ����+CMGS:<space><number>,���������������лس�(0x0D,0x0A,0x0D,0x0A),
     * Ȼ����OK,�����һ���س�����(0x0D,0x0A)
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
                                // �����س����з�
                                if (b[index2] == 0x0D && b[++index2] == 0x0A
                                    && b[++index2] == 0x0D
                                    && b[++index2] == 0x0A) {
                                    if (b[++index2] == 0x4F
                                        && b[++index2] == 0x4B) { // OK
                                        if (b[++index2] == 0x0D
                                            && b[++index2] == 0x0A) { // һ���س�����
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
     * �жϽ��յ����ַ�������Ƿ�����"ERROR"������
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
     * �Ƿ���յ��ֻ���������������,�ϴ�����������"+CMT:"��ͷ
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
