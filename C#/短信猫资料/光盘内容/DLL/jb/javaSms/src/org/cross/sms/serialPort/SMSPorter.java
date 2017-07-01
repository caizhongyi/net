//

package org.cross.sms.serialPort;

import java.io.*;
import java.util.*;

import javax.comm.*;

import org.cross.sms.msg.*;


/**
 This class handles the operation the serial port.
 <br><br>
 This class contains all the necessary (low-level) functions that handle COMM API
 and are responsible for the serial communication with the GSM device.
 <br><br>
 Comments left to be added in next release.
 */
public class SMSPorter implements SerialPortEventListener {
    /**
     Timeout period for the phone to respond to jSMSEngine.
     */
    private static final int RECV_TIMEOUT = 3 * 1000;

    /**
     Input/Output buffer size for serial communication.
     */
    private static final int BUFFER_SIZE = 8192;
    /**
     Delay (20ms) after each character sent. Seems that some mobile phones
     get confused if you send them the commands without any delay, even
     in slow baud rate.
     */
    private static final int DELAY_BETWEEN_CHARS = 5;

    private String port;
    private Object _SYNC_ = new Object();

    private SerialPort serialPort;
    private InputStream inStream;
    private OutputStream outStream;
    private int baud = 9600;
    long timeStamp = 0;


    public SMSPorter(String port) {
        this.port = port;
//        this.baud = baud;
    }

    public boolean open() throws Exception {
        boolean result = false;
        Enumeration portList;

        portList = CommPortIdentifier.getPortIdentifiers();
        while (portList.hasMoreElements()) {
            CommPortIdentifier portId = (CommPortIdentifier) portList.
                                        nextElement();
            if (portId.getPortType() == CommPortIdentifier.PORT_SERIAL) {
                if (portId.getName().equalsIgnoreCase(port)) {
                    serialPort = (SerialPort) portId.open("Modemn", 5000);
                    System.out.println("serial name is :" + serialPort.getName());
                    serialPort.notifyOnDataAvailable(true);
                    serialPort.notifyOnOutputEmpty(true);
                    serialPort.notifyOnBreakInterrupt(true);
                    serialPort.notifyOnFramingError(true);
                    serialPort.notifyOnOverrunError(true);
                    serialPort.notifyOnParityError(true);
                    serialPort.setFlowControlMode(SerialPort.FLOWCONTROL_NONE);
                    serialPort.addEventListener(this);

                    serialPort.setSerialPortParams(baud, SerialPort.DATABITS_8, // 数据位数
                            SerialPort.STOPBITS_1, // 停止位
                            SerialPort.PARITY_NONE); // 奇偶位

                    serialPort.setInputBufferSize(BUFFER_SIZE);
                    serialPort.setOutputBufferSize(BUFFER_SIZE);
                    serialPort.enableReceiveTimeout(RECV_TIMEOUT);
                    inStream = serialPort.getInputStream();
                    outStream = serialPort.getOutputStream();

                    //
                    result = true;
                }
            }
        }
        return result;
    }

    public void close() {

        try {
            serialPort.close();
        } catch (Exception e) {}
    }

    public void serialEvent(SerialPortEvent event) {
        switch (event.getEventType()) {
        case SerialPortEvent.BI:
            break;
        case SerialPortEvent.OE:

            break;
        case SerialPortEvent.FE:

            break;
        case SerialPortEvent.PE:

            break;
        case SerialPortEvent.CD:
            break;
        case SerialPortEvent.CTS:
            break;
        case SerialPortEvent.DSR:
            break;
        case SerialPortEvent.RI:
            break;
        case SerialPortEvent.OUTPUT_BUFFER_EMPTY:
            break;
        case SerialPortEvent.DATA_AVAILABLE:
            break;
        }
    }

    public void clearBuffer() throws Exception {
        while (dataAvailable()) {
            inStream.read();
        }
    }

    private void send(String s) throws Exception {

//        for (int i = 0; i < s.length(); i++) {
//            try {
//                Thread.sleep(DELAY_BETWEEN_CHARS);
//            } catch (Exception e) {}
//            outStream.write((byte) s.charAt(i));
//            outStream.flush();
//        }
//        System.out.println(s);
        long newtime = new Date().getTime();
        if(newtime-timeStamp<1000){
            Thread.sleep(1000+timeStamp-newtime);
        }
        timeStamp = newtime;
        PrintWriter pw;
        if (s != null) {
            pw = new PrintWriter(outStream);
            pw.println(s);
            pw.flush();
            System.out.println(s);
        }
    }

    private void send(char c) throws Exception {
        outStream.write((byte) c);
        outStream.flush();
    }

    private void skipBytes(int numOfBytes) throws Exception {
        int count, c;

        count = 0;
        while (count < numOfBytes) {
            c = inStream.read();
            if (c != -1) {
                count++;
            }
        }
    }

    private boolean dataAvailable() throws Exception {
        return (inStream.available() > 0 ? true : false);
    }

    public String getResponse() throws Exception {
        final int RETRIES = 3;
        final int WAIT_TO_RETRY = 1000;
        StringBuffer buffer;
        int c, retry;

        retry = 0;
        buffer = new StringBuffer(256);

        while (retry < RETRIES) {
            try {
                while (true) {
                    c = inStream.read();
                    if (c == -1) {
                        buffer.delete(0, buffer.length());
                        break;
                    }
                    buffer.append((char) c);

                    if ((buffer.toString().indexOf("OK\r") > -1) ||
                        ((buffer.toString().indexOf("ERROR") > -1) &&
                         (buffer.toString().lastIndexOf("\r") >
                          buffer.toString().indexOf("ERROR")) ||
                         ((buffer.toString().indexOf("CPIN") > -1) &&
                          (buffer.
                           toString().indexOf("\r",
                                              buffer.toString().indexOf("CPIN")) >
                           -1)))) {
                        break;
                    }
                }
                retry = RETRIES;
            } catch (Exception e) {
                if (retry < RETRIES) {
                    Thread.sleep(WAIT_TO_RETRY);
                    retry++;
                } else {
                    throw e;
                }
            }
        }

        // Following line is to skip the remaining "\n" char...
        if (dataAvailable()) {
            skipBytes(1);
        }
        if (buffer.length() > 0) {
            while ((buffer.charAt(0) == 13) || (buffer.charAt(0) == 10)) {
                buffer.
                        delete(0, 1);
            }
        }
        String response = buffer.toString();
//        System.out.println("response**********");
        System.out.println("response#" + response);
//        System.out.println("**********");

        return buffer.toString();
    }


    public String waitResponse() throws Exception {
        final int RETRIES = 2;
        final int WAIT_TO_RETRY = 1000;
        StringBuffer buffer;
        int c, retry;

        retry = 0;
        buffer = new StringBuffer(256);

        while (retry < RETRIES) {
            try {
                while (true) {
                    c = inStream.read();
                    if (c == -1) {
                        buffer.delete(0, buffer.length());
                        break;
                    }
                    buffer.append((char) c);

                    if ((buffer.toString().indexOf("\r") > -1) ||
                        ((buffer.toString().indexOf("ERROR") > -1) &&
                         (buffer.toString().lastIndexOf("\r") >
                          buffer.toString().indexOf("ERROR")) ||
                         ((buffer.toString().indexOf("CPIN") > -1) &&
                          (buffer.
                           toString().indexOf("\r",
                                              buffer.toString().indexOf("CPIN")) >
                           -1)))) {
                        break;
                    }
                }
                retry = RETRIES;
            } catch (Exception e) {
                if (retry < RETRIES) {
                    Thread.sleep(WAIT_TO_RETRY);
                    retry++;
                } else {
                    throw e;
                }
            }
        }

        // Following line is to skip the remaining "\n" char...
        if (dataAvailable()) {
            skipBytes(1);
        }
        //while ((buffer.charAt(0) == 13) || (buffer.charAt(0) == 10)) buffer.delete(0, 1);
        return buffer.toString();
    }

    public boolean CMD_setPDUMode() {
        try {
            synchronized (_SYNC_) {
                send(SMSATCmds.AT_PDU_MODE);
                String response = getResponse();
                if ("OK".equalsIgnoreCase(response)) {
                    return true;
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return false;
    }

    public boolean CMD_setStorageToMT(){
        try {
            synchronized (_SYNC_) {
                send(SMSATCmds.AT_SIEMENS_SMS_STORAGE);
                String response = getResponse();
                if ("OK".equalsIgnoreCase(response)) {
                    return true;
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return false;
    }

    public void CMD_getUnReadRecvMsg() {
        try {
            synchronized (_SYNC_) {
                send(
                        _CharacterUtil.replaceSymbol(
                                SMSATCmds.AT_LIST,
                                "{1}",
                                "0"));
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public void CMD_getAllRecvMsg() {
        try {
            send(
                    _CharacterUtil.replaceSymbol(
                            SMSATCmds.AT_LIST,
                            "{1}",
                            "4"));
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public void CMD_getReadedRecvMsg() {
        try {
            synchronized (_SYNC_) {
                send(
                        _CharacterUtil.replaceSymbol(
                                SMSATCmds.AT_LIST,
                                "{1}",
                                "1"));
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public void CMD_getSendedRecvMsg() {
        try {
            synchronized (_SYNC_) {
                send(
                        _CharacterUtil.replaceSymbol(
                                SMSATCmds.AT_LIST,
                                "{1}",
                                "3"));
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }


    public boolean CMD_delMsg(int memIndex) {
        String response;

        synchronized (_SYNC_) {
            if (memIndex > 0) {
                try {
                    send(
                            _CharacterUtil.replaceSymbol(
                                    SMSATCmds.AT_DELETE_MESSAGE,
                                    "{1}",
                                    "" + memIndex));
                    response = getResponse();
                    if (response.indexOf(SMSATCmds.AT_OK) > -1) {
                        return true;
                    } else {
                        return false;
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                    return false;
                }
            } else {
                return false;
            }
        }
    }

    public void CMD_delAllMsg() {
        CMD_getReadedRecvMsg();
        LinkedList ll = readPDUMsg();
        if (ll != null) {
            for (int i = 0; i < ll.size(); i++) {
                CMD_delMsg(((SMSMsg) ll.get(i)).memIndex);
            }
        }
        CMD_getSendedRecvMsg();
        ll = readPDUMsg();
        if (ll != null) {
            for (int i = 0; i < ll.size(); i++) {
                CMD_delMsg(((SMSMsg) ll.get(i)).memIndex);
            }
        }
    }


    public LinkedList readPDUMsg() {
        int i, j, memIndex;
        LinkedList ll = new LinkedList();
        try {
            String response = "";
            synchronized (_SYNC_) {
                response = getResponse();
            }
            BufferedReader reader =
                    new BufferedReader(new StringReader(
                            response));
            String line = reader.readLine();
            if (line != null) {
                line = line.trim();
            }
            //处理命名行被置入输出的问题
            while (line != null &&
                   (line.toLowerCase().startsWith("at+cmgl") ||
                   line.length() == 0)) {
                line = reader.readLine();
                if (line != null) {
                    line = line.trim();
                }
            }
            while ((line != null)
                     && (line.length() > 0)
                     && (!line.equalsIgnoreCase("OK"))
                     && (!line.equalsIgnoreCase("ERROR"))) {
                i = line.indexOf(':');
                j = line.indexOf(',');
                if(i>0&&j>0){
                    memIndex =
                            Integer.parseInt(
                                    line.substring(i + 1, j).trim());
                    String pdu = reader.readLine();
                    if (SmsUtil.isSMSMsgIn(pdu)) {
                        ll.add(
                                new SMSMsgIn(pdu, memIndex));
//                                    deviceInfo.getStatistics().incTotalIn();
                    }
//                                else if (isStatusReportMessage(pdu)) {
//                                    messageList.add(
//                                            new StatusReportMessage(pdu,
//                                            memIndex));
//                                    deviceInfo.getStatistics().incTotalIn();
//                                }
                }
                line = reader.readLine().trim();
            }
            reader.close();
            return ll;
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return null;
    }

    public boolean sendText(String phone, String text) {
        SMSMsgOut m = new SMSMsgOut(phone, text);
        String pdu = m.getPdu();
        return sendPduText(pdu);
    }

    public boolean sendPduText(String pdu) {
        int size = SmsUtil.getSMSSize(pdu);
        try {
            synchronized (_SYNC_) {
                String at_comm_send =
                        _CharacterUtil.replaceSymbol(
                                SMSATCmds.AT_SEND_MESSAGE,
                                "\"{1}\"",
                                "" + size);
                send(at_comm_send);
                while (!dataAvailable()) {
                    Thread.sleep(10);
                } while (dataAvailable()) {
                    skipBytes(1);
                }

                //发送数据
                pdu = pdu + (char) Integer.parseInt("1a",
                        16) ;
//                      + "z";
                send(pdu);
                String response = getResponse();
                int i=0;
                while(response.equals("") && i<3){
                    Thread.sleep(500);
                    i++;
                    response = getResponse();
                }
                if (response.indexOf(SMSATCmds.AT_OK)
                    > -1) {
                    return true;
                } else {
                    return false;
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return false;
    }

    public void setBaud(int baud) {
        this.baud = baud;
    }
}
