package com.wfcake.common;

import java.text.SimpleDateFormat;



/**
 * 工具类
 * 
 * @author WFCake
 * @version 1.0
 * 
 */

public class WFCakeUtil {
	/**
	 * 得到对应序号的价格
	 * 
	 * @return 对应序号的价格
	 */
	public int getPrice(String cakeCode) {
		//取得蛋糕对象内容数组的长度
		int cakeSize = Const.CAKEINFO.length;
		int price = 0;
		//在蛋糕对象内容数组数组中查找对应编号的价格
		for (int n = 0; n < cakeSize; n++) {
			if (cakeCode.equals(Const.CAKEINFO[n][0])) {
				price = Integer.parseInt(Const.CAKEINFO[n][2]);
			}
		}
		return price;
	}
	/**
	 * 得到对应序号的名称
	 * 
	 * @return 对应序号的名称
	 */
	public String getCakeName(String cakeCode) {
		//取得蛋糕对象内容数组的长度
		int cakeSize = Const.CAKEINFO.length;
		String cakeName = null;
		//在蛋糕对象内容数组数组中查找对应编号的名称
		for (int n = 0; n < cakeSize; n++) {
			if (cakeCode.equals(Const.CAKEINFO[n][0])) {
				cakeName = Const.CAKEINFO[n][1];
			}
		}
		return cakeName;
	}	
	

	/**
	 * 得到当前日期
	 * 
	 * @return
	 */
	public String getCurrentDate() {
		//定义输出日期的格式
		SimpleDateFormat tempDate = new SimpleDateFormat("yyyy-MM-dd");
		//获取当前的时间，并且按照规定的格式保存到dateTime中
		String dateTime = tempDate.format(new java.util.Date());
		return dateTime;
	}

	/**
	 * 格式化显示字段
	 * 
	 * @param content
	 * @param formatLength
	 * @return
	 */
	public String formatTable(String content, int formatLength) {
		int oldLength = content.length();
		if (oldLength < formatLength) {
			int margin = formatLength - oldLength;
			for (int n = 0; n < margin; n++) {
				content = content + " ";
			}
		}
		return content;
	}
}
