package org.cross.sms.serialPort;

/**
 <br><br>
 <strong>

 </strong>
 */
class SMSATCmds {
    public static final String AT_OK = "OK\r";
    public static final String AT_AT = "AT\r";

    public static final String AT_ECHO_OFF = "ATE0\r";

    public static final String AT_CMD_MODE = "+++";

    public static final String AT_DISABLE_INDICATIONS = "AT+CNMI=0,0,0,0\r";


    public static final String AT_ENABLE_INDICATIONS = "AT+CNMI=3,1,0,0,0\r";


    public static final String AT_SIEMENS_SMS_STORAGE = "AT+CPMS=\"ME\"\r";


    public static final String AT_MANUFACTURER = "AT+CGMI\r";
    public static final String AT_MODEL = "AT+CGMM\r";
    public static final String AT_SERIALNO = "AT+CGSN\r";
    public static final String AT_IMSI = "AT+CIMI\r";
    public static final String AT_BATTERY = "AT+CBC\r";
    public static final String AT_SIGNAL = "AT+CSQ\r";
    public static final String AT_SOFTWARE = "AT+CGMR\r";


    public static final String AT_LIST_MOTO = "AT+MMGL={1}\r";
    public static final String AT_LIST = "AT+CMGL={1}\r";
    public static final String AT_SEND_MESSAGE = "AT+CMGS=\"{1}\"\r";
    public static final String AT_KEEP_LINK_OPEN = "AT+CMMS=1\r";

    public static final String AT_DELETE_MESSAGE = "AT+CMGD={1}\r";
    public static final String AT_READ_MESSAGE = "AT+CMGR={1}\r";

    public static final String AT_ASCII_MODE = "AT+CMGF=1\r";
    public static final String AT_PDU_MODE = "AT+CMGF=0\r";
    public static final String AT_CHARSET_HEX = "AT+CSCS=\"HEX\"\r";

    public static final String AT_CHECK_LOGIN = "AT+CPIN?\r";
    public static final String AT_LOGIN = "AT+CPIN=\"{1}\"\r";
    public static final String AT_READY = "READY\r";
}
