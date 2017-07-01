package com.wfcake.manager;

import java.io.File;

import com.wfcake.common.Const;
import com.wfcake.common.Global;
import com.wfcake.common.WFCakeUtil;
import com.wfcake.domain.SaleRecord;

/**
 * 基于文件的记录管理, 记录保存在文件中
 *
 * @author WFCake
 * @version 1.0
 *
 */
public class SaleManagerWithFile  extends FileDBManager{

	/**
	 * 用构造器判断文件是否存在 若文件不存在, 则新建文档, 若文件存在, 则导入已有记录到内存中
	 *
	 */
	public SaleManagerWithFile() {
		File fileDB = new File(Const.FILEDBNAME);
		
		if (!fileDB.exists()) {   //判断文件是否存在
			try {
				fileDB.createNewFile();
			} catch (Exception ex) {
				ex.printStackTrace();
			}
		}
		
		FileDBManager fdm = new FileDBManager();
		
		//将表中数据保存到Global.saleList中
		fdm.readFileDB();
	}
	

	/**
	 * 添加一条销售记录
	 *
	 * @param cakeCode
	 * @param weight
	 */
	@SuppressWarnings("unchecked")
	public void addSaleRecord(String cakeCode, float weight) {
		WFCakeUtil wFCakeUtil = new WFCakeUtil();
		//获取对应编号的价格
		float price = (wFCakeUtil.getPrice(cakeCode));
		//获取当前的时间
		String saleDate = wFCakeUtil.getCurrentDate();
		//计算总价格
		float saleSum = price * weight;
		
		SaleRecord obj = new SaleRecord();
		
		//往新建的obj中分配数值
		obj.setSaleId(Global.saleIdCounter++);
		obj.setSaleDate(saleDate);
		obj.setCakeCode(cakeCode);
		obj.setPrice(price);
		obj.setWeight(weight);
		obj.setSaleSum(saleSum);
		
        //将新的销售记录对象添加到数组中
		Global.saleList.add(obj);
	}


	/**
	 * 将所有记录保存到文件中
	 *
	 */
	public void saveSaleRecord() {
		FileDBManager fdm = new FileDBManager();
		fdm.writeFileDB();
	}
	/**
	 * 打印显示表中数据
	 * 
	 * @return
	 */
	public void showSale(){
		
		FileDBManager fdm= new FileDBManager();
		
		SaleRecord saleR= new SaleRecord();
		
		for(int i=0; i< Global.saleList.size() ; i++){
			
			saleR =(SaleRecord)Global.saleList.get(i);
			
			System.out.println(fdm.convertObjToStr(saleR));
		}
	}
}
