package com.wfcake.swing;

import java.awt.BorderLayout;
import java.awt.Container;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextField;
import javax.swing.JToolBar;
import javax.swing.WindowConstants;

import com.wfcake.common.WFCakeUtil;
import com.wfcake.manager.SaleManagerWithFile;

/**
 * Swing界面主窗口
 * 
 * @author WFCake
 * @version 1.0
 * 
 */
public class MainWindow extends JFrame implements ActionListener {

	private static final long serialVersionUID = 2379651436127339428L;
	JMenuBar jMenuBarOne;// 菜单栏
	JMenu saleMenu, helpMenu;// 一级菜单
	JMenuItem inputRecord, viewList, exitSystem, aboutInfo;// 二级菜单

	JLabel lCakeCode, lWeight;
	JTextField tCakeCode, tWeight;
	JButton btSubmit, btReset;

	JPanel myPanel1, myPanel2, myPanel3;

	String cakeCode;
	float iWeight;
	SaleManagerWithFile saleManager = new SaleManagerWithFile();

	private JButton tool_inputRecord = new JButton();

	private JButton tool_viewList = new JButton();

	private ImageIcon tool_inputRecord_image = new ImageIcon(MainWindow.class
			.getResource("tool_inputRecord.gif"));

	private ImageIcon tool_viewList_image = new ImageIcon(MainWindow.class
			.getResource("tool_viewList.gif"));

	JDialog customDialog;

	public MainWindow() {
		// 一级菜单定义
		jMenuBarOne = new JMenuBar();
		saleMenu = new JMenu("收银管理");
		helpMenu = new JMenu("帮助");
		// 二级菜单定义
		inputRecord = new JMenuItem("收银录入");
		viewList = new JMenuItem("报表显示");
		exitSystem = new JMenuItem("退出");
		aboutInfo = new JMenuItem("关于...");
		// 菜单添加
		this.setJMenuBar(jMenuBarOne);
		jMenuBarOne.add(saleMenu);
		jMenuBarOne.add(helpMenu);
		saleMenu.add(inputRecord);
		saleMenu.add(viewList);
		saleMenu.addSeparator();
		saleMenu.add(exitSystem);
		helpMenu.add(aboutInfo);

		// 定义工具栏的图标
		tool_inputRecord.setToolTipText("收银录入");
		tool_inputRecord.setIcon(tool_inputRecord_image);
		tool_viewList.setToolTipText("报表显示");
		tool_viewList.setIcon(tool_viewList_image);

		myPanel1 = new JPanel();
		myPanel2 = new JPanel();
		myPanel3 = new JPanel();

		lCakeCode = new JLabel("商品编号: ");
		lWeight = new JLabel("商品重量: ");
		tCakeCode = new JTextField(8);
		tWeight = new JTextField(8);
		btSubmit = new JButton("确定");
		btReset = new JButton("重输");

		tool_viewList.addActionListener(this);
		tool_inputRecord.addActionListener(this);
		btSubmit.addActionListener(this);
		viewList.addActionListener(this);
		exitSystem.addActionListener(this);
		aboutInfo.addActionListener(this);
		btReset.addActionListener(this);

		// 布局管理
		Container con = getContentPane();
		con.setLayout(null);
		myPanel1.add(lCakeCode);
		myPanel1.add(tCakeCode);
		myPanel2.add(lWeight);
		myPanel2.add(tWeight);
		myPanel3.add(btSubmit);
		myPanel3.add(btReset);

		myPanel1.setBounds(120, 51, 180, 30);
		myPanel2.setBounds(120, 91, 180, 30);
		myPanel3.setBounds(120, 131, 180, 40);

		con.add(myPanel1);
		con.add(myPanel2);
		con.add(myPanel3);

		// 定义窗口属性
		this.setTitle("收银系统");
		this.setBounds(100, 100, 450, 350);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		final JToolBar toolBar = new JToolBar();
		toolBar.add(tool_inputRecord);
		toolBar.add(tool_viewList);
		this.getContentPane().add(toolBar);
		toolBar.setBounds(0, 0, 442, 24);
		this.setVisible(true);
		this.validate();
	}

	/**
	 * 事件处理
	 */
	public void actionPerformed(ActionEvent e) {
		/**
		 * 重输入数据
		 */
		if (e.getSource() == btReset) {
			tCakeCode.setText("");
			tWeight.setText("");
		}
		/**
		 * 退出系统
		 */
		if (e.getSource() == exitSystem) {
			System.exit(0);
		}
		/**
		 * 关于...
		 */
		if (e.getSource() == aboutInfo) {
			JFrame jf = new JFrame();
			Container con = jf.getContentPane();
			con.setLayout(null);
			JLabel name = new JLabel("软件名称: 蛋糕门店收银系统");
			name.setBounds(10, 10, 180, 30);
			JLabel ver = new JLabel("版本: 1.0");
			ver.setBounds(10, 50, 180, 30);
			con.add(name);
			con.add(ver);
			jf.setTitle("关于...");
			jf.setBounds(220, 200, 250, 130);
			jf.setVisible(true);
			jf.setDefaultCloseOperation(WindowConstants.HIDE_ON_CLOSE);
		}
		/**
		 * 报表显示
		 */
		if (e.getSource() == viewList || e.getSource() == tool_viewList) {
			Object[] title = { "序号", "日期", "名称", "价格", "重量", "金额" };// 创建Object类对象数组保存列名
			SwingUtil su = new SwingUtil();
			Object[][] value = su.getRecords();
			JTable table = new JTable(value, title);
			JFrame jf = new JFrame();
			Container con = jf.getContentPane();
			con.add(new JScrollPane(table), BorderLayout.CENTER);
			jf.setTitle("记录报表");
			jf.setBounds(120, 120, 650, 350);
			jf.setVisible(true);
			jf.setDefaultCloseOperation(WindowConstants.HIDE_ON_CLOSE);
		}
		
		/**
		 * 录入记录
		 */
		if (e.getSource() == btSubmit || e.getSource() == tool_inputRecord) {
			customDialog = new JDialog(this, "金额计算");
			JPanel dialogTop = new JPanel();
			JPanel dialogCenter = new JPanel();
			JPanel dialogBottom = new JPanel();

			// 得到商品编号对应的价格
			WFCakeUtil util = new WFCakeUtil();
			cakeCode = tCakeCode.getText().toUpperCase();
			float price = (util.getPrice(tCakeCode.getText().toUpperCase()));
					
			iWeight = Float.parseFloat(tWeight.getText());
			float saleSum = price * iWeight;

			// 版面布局
			JLabel lSum = new JLabel("应付金额为 " + saleSum + " 元, 请输入实付金额: ");
			dialogTop.add(lSum);
			JTextField tMoney = new JTextField(8);
			dialogCenter.add(tMoney);
			JButton btOK = new JButton("确认");
			dialogBottom.add(btOK);
			btOK.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					save_ActionPerformed(e);
				}
			});
			customDialog.setBounds(220, 200, 250, 130);
			customDialog.getContentPane().setLayout(new BorderLayout());
			customDialog.getContentPane().add("North", dialogTop);
			customDialog.getContentPane().add("Center", dialogCenter);
			customDialog.getContentPane().add("South", dialogBottom);
			customDialog.setVisible(true);
		}
	}

	/**
	 * 把数据输入到文件中去
	 * 
	 * @param e
	 */
	void save_ActionPerformed(ActionEvent e) {
		saleManager.addSaleRecord(cakeCode, iWeight);
		saleManager.saveSaleRecord();
		customDialog.setVisible(false);
	}
}
