package com.wfcake.common;

/**
 * 常量
 * @author WFCake
 * @version 1.0
 *
 */
public class Const {

	/**
	 * 蛋糕对象内容设置, 包括代码/名称/每磅的价格
	 */
	public static final String[][] CAKEINFO = {
	        { "A01", "法式蛋糕        ", "35" },
	        { "A02", "意式蛋糕        ", "40" },
	        { "B01", "巧克力蛋糕      ", "45" },
	        { "B02", "冰淇淋蛋糕      ", "50" },
	        { "B03", "奶油水果蛋糕    ", "50" },
	        { "C01", "草莓芝士蛋糕    ", "70" },
	        { "C02", "朗姆酒木司蛋糕  ", "80" },
	    };

	/**
	 * 记录文件名
	 * 扩展名为.csv, 可以用Excel打开
	 */
	public static final String FILEDBNAME = "src/SaleData.csv";
}
