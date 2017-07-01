package org.cross.sms.msg;

import java.util.*;


/**
 This class encapsulates the basic characteristics of an SMS message. A message
 is further subclassed to an "Incoming" message and an "Outgoing" message.
 <br><br>
 This class is <strong>never</strong> used directly. Please use one of its descendants.

 @see	IncomingMessage
 @see	StatusReportMessage
 @see	OutgoingMessage
 @see	CPhoneBook
 */
public abstract class SMSMsg {
    public static final int TYPE_INCOMING = 1;
    public static final int TYPE_OUTGOING = 2;
    public static final int TYPE_STATUS_REPORT = 3;
    public static final int TYPE_WAP_PUSH = 4;

    private int type;
    public int memIndex;
    protected Date date;
    protected String originator;
    protected String recipient;
    protected String text;
    protected String messageEncoding;

    /**
     Default constructor of the class.

     @param	type	the type (incoming/outgoing) of the message.
     @param	date	the creation date of the message.
     @param	originator	the originator's number. Applicable only for incoming messages.
     @param	recipient	the recipient's number. Applicable only for outgoing messages.
     @param	text	the actual text of the message.
     @param	memIndex		the index of the memory location in the GSM device where
         this message is stored. Applicable only for incoming messages.

     <br><br>Notes:<br>
     <ul>
      <li>Phone numbers are represented in their international format (e.g. +306974... for Greece).</li>
      <li>"Recipient" may be an entry from the phonebook.</li>
     </ul>
     */
    public SMSMsg(int type, Date date, String originator, String recipient,
                  String text, int memIndex) {
        this.type = type;
        this.date = date;
        this.originator = originator;
        this.recipient = recipient;
        this.text = text;
        this.memIndex = memIndex;
        this.messageEncoding = "gsm_default";
    }

    /**
     Returns the type of the message. Type is either incoming or outgoing, as denoted
     by the class' static values INCOMING and OUTGOING.

     @return  the type of the message.
     */
    public int getType() {
        return type;
    }


    /**
     Returns the memory index of the GSM device, where the message is stored.
     Applicable only for incoming messages.

     @return  the memory index of the message.
     */
    public int getMemIndex() {
        return memIndex;
    }

    /**
     Returns the date of the message. For incoming messages, this is the sent date.
     For outgoing messages, this is the creation date.

     @return  the date of the message.
     */
    public Date getDate() {
        return date;
    }

    /**
     Returns the Originator of the message.

     @return  the originator of the message.
     */
    public String getOriginator() {
        return originator;
    }

    /**
     Returns the Recipient of the message.

     @return  the recipient of the message.
     */
    public String getRecipient() {
        return recipient;
    }

    /**
     Returns the actual text of the message (ASCII).

     @return  the text of the message.
     */
    public String getText() {
        return text;
    }

    /**
     Returns the text of the message, in hexadecimal format.

     @return  the text of the message (HEX format).
     */
    public String getHexText() {
//        return SMSGSMEncoding.text2Hex(text, SMSGSMEncoding.GSM7BITDEFAULT);
        return _CharacterUtil.stringToHexString(text, getMessageEncoding());
    }

    /**
     Returns the encoding method of the message. Returns of the constants
     MESSAGE_ENCODING_7BIT, MESSAGE_ENCODING_8BIT, MESSAGE_ENCODING_UNICODE.
     This is meaningful only when working in PDU mode.

     @return  the message encoding.
     */
    public String getMessageEncoding() {
        return messageEncoding;
    }

    public String getPdu() {
        return "";
    }


    /**
     Set the text of the message.

     @param	text	the text of the message.
     */
    public void setText(String text) {
        this.text = text;
    }

    /**
     Set the phone number of the recipient. Applicable to outgoing messages.

     @param	recipient	the recipient's phone number (international format).
     */
    public void setRecipient(String recipient) {
        this.recipient = recipient;
    }


    /**
     Set the date of the message.

     @param	date	the date of the message.
     */
    public void setDate(Date date) {
        this.date = date;
    }

    /**
     Set the message encoding. Should be one of the constants
     MESSAGE_ENCODING_7BIT, MESSAGE_ENCODING_8BIT, MESSAGE_ENCODING_UNICODE.
     This is meaningful only when working in PDU mode - default is 7bit.

     @param	messageEncoding	one of the message encoding contants.
     */
    public void setMessageEncoding(String messageEncoding) {
        this.messageEncoding = messageEncoding;
    }

    public String toString() {
        String str;

        str = "** GSM MESSAGE **\n";
        str += "  Type: " +
                (type == TYPE_INCOMING ? "Incoming." :
                 (type == TYPE_OUTGOING ? "Outgoing." : "Status Report.")) +
                "\n";
//        str += "  Id: " + id + "\n";
        str += "  Memory Index: " + memIndex + "\n";
        str += "  Date: " + date + "\n";
        str += "  Originator: " + originator + "\n";
        str += "  Recipient: " + recipient + "\n";
        str += "  Text: " + text + "\n";
        str += "  Hex Text: " + getHexText() +
                "\n";
        str += "  Encoding: " + messageEncoding + "\n";
        str += "***\n";
        return str;
    }
//    protected String id;
//
//    /**
//     Set the id of the message.
//
//     @param	id	the id of the message.
//     */
//    public void setId(String id) {
//        this.id = id;
//    }
//
//    /**
//     Returns the id of the message.
//
//     @return  the id of the message.
//     */
//    public String getId() {
//        return id;
//    }

}
