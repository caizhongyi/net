package org.cross.sms;

import org.cross.sms.msg.*;

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
public class testChar {
    public testChar() {
        super();
    }

    public static void main(String[] args) {
        String pdu = "0891683110102105F011000B813118216553F60000FF0AE630796E66A7EBF835";
        SMSMsgIn m = new SMSMsgIn(pdu, 1);
        System.out.println(m);
//        System.out.println(m.getText());
//        String src = "123456";
//        System.out.println(src.substring(0,3));
//         String pdu = "0500032C020162B219AD66BBE172B0986C46ABD96EB81CCC65DFBCD8723DDBB5668FC96C7A1E9D56EBF46CB98BCDCE87E372759ACD66B3CB65366A1E168FC965F619AD56AFD968F71B1E97CFE9F5FA1D9FD787C56316996D9EA337A9F47AEC46CBE372FB9C5EB7DFF179BD5D3C26B3D5EF313BBD1EA3E57571DC3E4FDBD977BC5DEF168FC965F579F34AA7D7";

    }
}
