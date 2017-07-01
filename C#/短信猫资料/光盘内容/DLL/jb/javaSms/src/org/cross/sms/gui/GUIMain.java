package org.cross.sms.gui;

import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import java.awt.*;
import java.awt.datatransfer.*;
import org.cross.sms.*;
import java.util.*;
import java.io.*;

public class GUIMain extends JFrame {
    SmsServer m_server = null;
    _guiThread m_thread;
    AddressList m_add = new AddressList();
    JPanel panel_cont = new JPanel();
    JPanel panel_res = new JPanel();
    JLabel ltypetip = new JLabel();
    JLabel lstatus = new JLabel();
    JButton btn_Send = new JButton();
    JTextArea ta_res = new JTextArea();
    JTextArea ta_cont = new JTextArea();
    JScrollPane scrollPane_res = new JScrollPane();
    JScrollPane scrollPane_cont = new JScrollPane();
    BorderLayout borderLayout1 = new BorderLayout();
    BorderLayout borderLayout2 = new BorderLayout();
    JPanel panel_cont_header = new JPanel();
    java.awt.BorderLayout borderLayout3 = new BorderLayout();
    JPanel panel_cont_cent = new JPanel();
    java.awt.BorderLayout borderLayout4 = new BorderLayout();
    JPanel panel_cont_bot = new JPanel();
    java.awt.BorderLayout borderLayout5 = new BorderLayout();
    JComboBox cb_phone = new JComboBox();
    public GUIMain() {
        m_server = new SmsServer();
        try {
            jbInit();
        } catch (Exception exception) {
            exception.printStackTrace();
        }
    }

    protected boolean isConnected() {
        if (m_server == null) {
            return false;
        }
        return true;
    }

    public void open(String com) {
//        close();
        m_add.init();
        fillAddList();
        m_server.setComName(com);
        try {
            m_server.start();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        m_thread = new _guiThread(m_server, this);
        m_thread.start();
    }

    public void close() {
        if (m_server != null) {
            m_server.stop();
            m_server = null;
        }
    }

    private void jbInit() throws Exception {
        getContentPane().setLayout(borderLayout1);

        panel_cont.setPreferredSize(new Dimension(270, 160));
        panel_cont.setLayout(borderLayout3);
        panel_cont.setAlignmentX((float) 0.5);
        panel_cont.setAlignmentY((float) 0.5);
        panel_cont.setBorder(BorderFactory.createLineBorder(Color.black));

        ltypetip.setText("手机号码: +86");
        cb_phone.setPreferredSize(new Dimension(120, 22));
        cb_phone.setEditable(true);

        ta_cont.setBorder(BorderFactory.createLineBorder(Color.black));
        ta_cont.setLineWrap(true);
        ta_cont.addKeyListener(new GUIMain_ta_content_keyAdapter(this));
        ta_cont.setAutoscrolls(true);
        btn_Send.setText("发送");
        btn_Send.addActionListener(new SMSClient_btn_Send_actionAdapter(this));
        btn_Send.setPreferredSize(new Dimension(60,25));
        lstatus.setPreferredSize(new Dimension(0, 15));
        lstatus.setToolTipText("");
        panel_cont_cent.setLayout(borderLayout4);
        panel_cont_bot.setLayout(borderLayout5);
        panel_cont_cent.add(scrollPane_cont, java.awt.BorderLayout.CENTER);
        panel_cont_header.add(ltypetip);
        panel_cont_header.add(cb_phone);

        panel_cont.add(panel_cont_cent, java.awt.BorderLayout.CENTER);
        scrollPane_cont.getViewport().add(ta_cont, null);
        panel_cont.setAutoscrolls(true);

        panel_res.setBorder(BorderFactory.createLineBorder(Color.black));
        panel_res.setLayout(borderLayout2);
        ta_res.setBorder(BorderFactory.createLineBorder(Color.black));
        ta_res.setLineWrap(true);
        ta_res.setEditable(false);
        panel_res.setAutoscrolls(true);
        scrollPane_res.getViewport().add(ta_res,null);
        panel_res.add(scrollPane_res, java.awt.BorderLayout.CENTER);

        this.setDefaultCloseOperation(EXIT_ON_CLOSE);
        this.addComponentListener(new GUIMain_this_componentAdapter(this));
        this.addWindowListener(new GUIMain_this_windowAdapter(this));
        this.getContentPane().add(panel_cont, java.awt.BorderLayout.SOUTH);
        this.getContentPane().add(panel_res, java.awt.BorderLayout.CENTER);
        panel_cont.add(panel_cont_bot, java.awt.BorderLayout.SOUTH);
        panel_cont.add(panel_cont_header, java.awt.BorderLayout.NORTH);
        panel_cont_bot.add(btn_Send, java.awt.BorderLayout.EAST);
        panel_cont_bot.add(lstatus, java.awt.BorderLayout.CENTER);
        this.setSize(292, 470);
        this.setLocation(100, 100);
    }

    private void fillAddList(){
        cb_phone.removeAllItems();
        for(int i=0;i<m_add.size();i++){
            cb_phone.addItem(m_add.getPhone(i));
        }
    }

    public static void main(String[] args) {
        GUIMain cl = new GUIMain();
        SMSConnect con = new SMSConnect(cl);
        con.setVisible(true);
        if (con.isok) {
            cl.open(con.m_com);
            cl.setVisible(true);
        } else {
            System.exit(0);
        }
    }


    public void doSend(){
        try {
            String phone = cb_phone.getSelectedItem().toString();
//                    "+86" + txt_phone.getText().trim();
            m_server.send(phone,ta_cont.getText().trim());
            String cont = ta_cont.getText();
            if(cont.trim().equals("")){
                JOptionPane.showMessageDialog(this,"没有信息");
                return;
            }
            //发送后拷贝到剪贴板中
            StringSelection ss = new StringSelection(cont);
            Clipboard cl = getToolkit().getSystemClipboard();
            cl.setContents(ss,ss);
            appendResp("我说：\n");
            appendResp(cont+"\n");
            ta_cont.setText("");
            ta_cont.requestFocus();
            new Sound(Sound.SEND).start();
            //将新号码加入到地址列表中
            if(!m_add.contains(phone)){
                m_add.add(0, phone);
            }
            if(!phone.equals(m_add.get(0))){
                m_add.remove(phone);
                m_add.add(0,phone);
                fillAddList();
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }

    }
    public void btn_Send_actionPerformed(ActionEvent e) {
        doSend();
    }

    public void appendResp(String rsp) {
        ta_res.append(rsp);
        ta_res.setCaretPosition(ta_res.getText().length());
    }

    public void this_windowActivated(WindowEvent e) {
        ta_cont.requestFocus();
    }

    public void ta_content_keyTyped(KeyEvent e) {
        if(e.isAltDown()){
            if(e.getKeyChar()=='s'){
                doSend();
            }
        }
    }

    public void ta_content_keyReleased(KeyEvent e) {
        int len =
                    ta_cont.getText().length();

        lstatus.setText("长度为"+len);
    }

    public void this_componentResized(ComponentEvent e) {
        int y=this.getHeight();
        int x=this.getWidth();
        System.out.println("x="+x+" y="+y);
        if(x<290||y<470){
            if(x<290)x=290;
            if(y<470)y=470;
            this.setSize(new Dimension(x,y));
        }
//        x = x - 20;
//        y = y - 40;
//        int cy = Math.round(y*35/100);
//        int ry = y-cy;
//
//        jPanel1.setSize(new Dimension(x,cy));
//        jPanel2.setSize(new Dimension(x,ry));
//        this.repaint();
    }

    public void this_windowClosing(WindowEvent e) {
        m_add.save();
    }
}
class GUIMain_ta_content_keyAdapter extends KeyAdapter {
    private GUIMain adaptee;
    GUIMain_ta_content_keyAdapter(GUIMain adaptee) {
        this.adaptee = adaptee;
    }

    public void keyTyped(KeyEvent e) {
        adaptee.ta_content_keyTyped(e);
    }
    public void keyReleased(KeyEvent e) {
        adaptee.ta_content_keyReleased(e);
    }

}


class GUIMain_this_windowAdapter extends WindowAdapter {
    private GUIMain adaptee;
    GUIMain_this_windowAdapter(GUIMain adaptee) {
        this.adaptee = adaptee;
    }

    public void windowActivated(WindowEvent e) {
        adaptee.this_windowActivated(e);
    }

    public void windowClosing(WindowEvent e) {
        adaptee.this_windowClosing(e);
    }
}


class GUIMain_this_componentAdapter extends ComponentAdapter {
    private GUIMain adaptee;
    GUIMain_this_componentAdapter(GUIMain adaptee) {
        this.adaptee = adaptee;
    }

    public void componentResized(ComponentEvent e) {
        adaptee.this_componentResized(e);
    }
}


class SMSClient_btn_Send_actionAdapter implements ActionListener {
    private GUIMain adaptee;
    SMSClient_btn_Send_actionAdapter(GUIMain adaptee) {
        this.adaptee = adaptee;
    }

    public void actionPerformed(ActionEvent e) {
        adaptee.btn_Send_actionPerformed(e);
    }
}

