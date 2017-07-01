/**
 * 
 */
package com.wfcake.ui;

import com.wfcake.manager.SaleManagerWithFile;
import com.wfcake.common.*;
import com.wfcake.domain.SaleRecord;

/**
 * @author WFCake
 * 
 */
public class AbstractUI {
	
	/**
	 * 人机交互界面<br>
	 * 注意, 尚未添加容错处理, 所以: <br>
	 * 1. 蛋糕编号为{A01,A02,B01,B02,B03,C01,C02}, 不分大小写;<br>
	 * 2. 蛋糕重量为{1,2.5,3,4}等.
	 * 
	 */
	public void showInterface() {
		// 显示欢迎词
		System.out.print("\n            欢迎进入收银系统!\n\n");

/*		// 人机操作界面Begin
		System.out.print("请输入蛋糕编号: ");
		Scanner sacnner = new Scanner(System.in);
		String cakeCode = sacnner.next().trim().toUpperCase();
        //获取用户输入重量
		System.out.print("请输入蛋糕重量: ");
		// 这里没有做容错处理, 所以输入务必准确, 否则会报空指针异常
		sacnner = new Scanner(System.in);
		float weight = Float.parseFloat(sacnner.next().trim());

		WFCakeUtil aUtil = new WFCakeUtil();
		float price = aUtil.getPrice(cakeCode);
		float saleSum = price * weight;

		// 显示应收, 实收及找零金额
		System.out.println("应收: " + saleSum + " 元.");
		System.out.print("请输入实收金额: ");
		sacnner = new Scanner(System.in);
		int infactMoney = Integer.parseInt(sacnner.next().trim());
		System.out.println("找零: " + (infactMoney - saleSum) + " 元.");

		//将该条记录保存到List
		saleManager.addSaleRecord(cakeCode, weight);
		//保存记录到SaleDate.csv中
		saleManager.saveSaleRecord();	
		//显示记录
		saleManager.showSale();*/
		
		SaleManagerWithFile fm = new SaleManagerWithFile();
		for(int i=0; i<Global.saleList.size(); i++){
			SaleRecord sal =(SaleRecord) Global.saleList.get(i);
			System.out.println(fm.convertObjToStr(sal));
		}
		
	}

}
