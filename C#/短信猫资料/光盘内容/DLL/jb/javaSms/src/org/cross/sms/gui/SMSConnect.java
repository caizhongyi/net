package org.cross.sms.gui;

import java.awt.event.*;
import java.util.*;

import javax.comm.*;
import javax.swing.*;
import org.cross.sms.msg.SmsUtil;

import com.borland.jbcl.layout.*;

public class SMSConnect extends JDialog {
    public String m_com;
    public int m_baud;
    public boolean isok;

    JLabel jLabel1 = new JLabel();
    JButton btn_ok = new JButton();
    JButton btn_cancel = new JButton();
    JPanel panel_port = new JPanel();
    JPanel jPanel2 = new JPanel();
    VerticalFlowLayout verticalFlowLayout1 = new VerticalFlowLayout();
    JLabel jLabel2 = new JLabel();
    JComboBox cb_list = new JComboBox();
    JPanel panel_baud = new JPanel();
    JLabel jLabel3 = new JLabel();
    JComboBox cb_baud = new JComboBox();
    JPanel panel_mobileType = new JPanel();
    JLabel jLabel4 = new JLabel();
    JComboBox cb_mobileType = new JComboBox();
    String[] cmsc_center = new String[]{"+8613800100500",
                           "+8613800791500",
                           "+8613010101500"
//    ,"",""
    };
    public SMSConnect(JFrame fr) {
        super(fr, true);
        try {
            jbInit();
        } catch (Exception exception) {
            exception.printStackTrace();
        }
    }

    private void jbInit() throws Exception {
        getContentPane().setLayout(verticalFlowLayout1);
        this.setSize(400, 200);
        this.setLocation(200, 300);
        jLabel1.setText("    请选择连接串口:      ");
        btn_cancel.setText("取消");
        btn_cancel.addActionListener(new SMSConnect_btn_cancel_actionAdapter(this));
        btn_ok.setText("确定");
        btn_ok.addActionListener(new SMSConnect_btn_ok_actionAdapter(this));
        jLabel2.setToolTipText("");
        jLabel2.setText("                                                 ");
        this.setDefaultCloseOperation(3);
        this.setResizable(true);
        cb_list.setToolTipText("");
        jLabel3.setText("请选择连接波特率：");
        jLabel4.setText("请选择短信中心：");
        //        cb_list.setPopupVisible(true);
        panel_port.add(jLabel1);
        panel_port.add(cb_list);
        panel_baud.add(jLabel3);
        panel_baud.add(cb_baud);
        jPanel2.add(jLabel2);
        jPanel2.add(btn_ok);
        jPanel2.add(btn_cancel);
        this.getContentPane().add(panel_mobileType);
        this.getContentPane().add(panel_port, null);
        this.getContentPane().add(panel_baud);
        this.getContentPane().add(jPanel2, null);
        panel_mobileType.add(jLabel4);
        panel_mobileType.add(cb_mobileType);
        Vector vect = this.getComPort();
        for (int i = 0; i < vect.size(); i++) {
            cb_list.addItem(vect.get(i).toString());
        }

        cb_baud.addItem("9600");
        cb_baud.addItem("19200");
        cb_baud.addItem("38400");
        cb_baud.addItem("57600");
        cb_baud.addItem("115200");
        cb_baud.addItem("1152000");
        cb_baud.addItem("4000000");
        cb_mobileType.setEditable(false);
//        cb_mobileType.addItem("内置短信中心");
        cb_mobileType.addItem("移动（手机）");
        cb_mobileType.addItem("移动(九江)");
        cb_mobileType.addItem("联通（手机）");
//        cb_mobileType.setSelectedIndex(1);
//        cb_mobileType.addItem("电信（小灵通）");
//        cb_mobileType.addItem("网通（小灵通）");
    }

    public void btn_ok_actionPerformed(ActionEvent e) {
//        m_com = txt_com.getText().trim();
        m_com = cb_list.getSelectedItem().toString();
        m_baud = Integer.parseInt(cb_baud.getSelectedItem().toString());
        SmsUtil.SMSC_CODE = cmsc_center[cb_mobileType.getSelectedIndex()];
        isok = true;
        setVisible(false);
    }

    public void btn_cancel_actionPerformed(ActionEvent e) {
        isok = false;
        setVisible(false);
    }

    public Vector getComPort() {
        Vector rtn = new Vector();
        Enumeration en = CommPortIdentifier.getPortIdentifiers();

        CommPortIdentifier portId;
        System.out.println("begin list");

        while (en.hasMoreElements())

        {

            portId = (CommPortIdentifier) en.nextElement();

            /*如果端口类型是串口，则打印出其端口信息*/
//           System.out.println(portId.getName());

            if (portId.getPortType() == CommPortIdentifier.PORT_SERIAL)

            {
                if(!rtn.contains(portId.getName()))
                    rtn.add(portId.getName());


            }

        }
        return rtn;
    }
}


class SMSConnect_btn_cancel_actionAdapter implements ActionListener {
    private SMSConnect adaptee;
    SMSConnect_btn_cancel_actionAdapter(SMSConnect adaptee) {
        this.adaptee = adaptee;
    }

    public void actionPerformed(ActionEvent e) {
        adaptee.btn_cancel_actionPerformed(e);
    }
}


class SMSConnect_btn_ok_actionAdapter implements ActionListener {
    private SMSConnect adaptee;
    SMSConnect_btn_ok_actionAdapter(SMSConnect adaptee) {
        this.adaptee = adaptee;
    }

    public void actionPerformed(ActionEvent e) {
        adaptee.btn_ok_actionPerformed(e);
    }
}
