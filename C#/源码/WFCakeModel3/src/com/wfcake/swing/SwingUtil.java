package com.wfcake.swing;

import com.wfcake.common.Global;
import com.wfcake.common.WFCakeUtil;
import com.wfcake.domain.SaleRecord;

/**
 * Swing窗口辅助类
 * @author WFCake
 * @version 1.0
 *
 */
public class SwingUtil {
	
	/**
	 * 将所有记录转成多维数组
	 * @return
	 */
	public Object[][] getRecords(){
		Object[][] value = null;
		int size = Global.saleList.size();
		value = new String[size][6];
		for (int n = 0; n < size; n++) {
			SaleRecord obj = (SaleRecord) Global.saleList.get(n);
			int saleId = obj.getSaleId();
			String saleDate = obj.getSaleDate();
			String cakeCode = obj.getCakeCode();
			WFCakeUtil util = new WFCakeUtil();
			String cakeName =  util.getCakeName(cakeCode);		
			float weight = obj.getWeight();
			float price = obj.getPrice();
			float saleSum = obj.getSaleSum();
			value[n][0] = saleId + "";
			value[n][1] = saleDate;
			value[n][2] = cakeName;
			value[n][3] = price + "";
			value[n][4] = weight + "";
			value[n][5] = saleSum + "";
		}
		return value;		
	}

}
