package com.wfcake.manager;

import java.io.File;

import com.wfcake.common.Const;
import com.wfcake.common.Global;
import com.wfcake.common.WFCakeUtil;
import com.wfcake.domain.SaleRecord;

/**
 * �����ļ��ļ�¼����, ��¼�������ļ���
 *
 * @author WFCake
 * @version 1.0
 *
 */
public class SaleManagerWithFile  extends FileDBManager{

	/**
	 * �ù������ж��ļ��Ƿ���� ���ļ�������, ���½��ĵ�, ���ļ�����, �������м�¼���ڴ���
	 *
	 */
	public SaleManagerWithFile() {
		File fileDB = new File(Const.FILEDBNAME);
		
		if (!fileDB.exists()) {   //�ж��ļ��Ƿ����
			try {
				fileDB.createNewFile();
			} catch (Exception ex) {
				ex.printStackTrace();
			}
		}
		
		FileDBManager fdm = new FileDBManager();
		
		//���������ݱ��浽Global.saleList��
		fdm.readFileDB();
	}
	

	/**
	 * ���һ�����ۼ�¼
	 *
	 * @param cakeCode
	 * @param weight
	 */
	@SuppressWarnings("unchecked")
	public void addSaleRecord(String cakeCode, float weight) {
		WFCakeUtil wFCakeUtil = new WFCakeUtil();
		//��ȡ��Ӧ��ŵļ۸�
		float price = (wFCakeUtil.getPrice(cakeCode));
		//��ȡ��ǰ��ʱ��
		String saleDate = wFCakeUtil.getCurrentDate();
		//�����ܼ۸�
		float saleSum = price * weight;
		
		SaleRecord obj = new SaleRecord();
		
		//���½���obj�з�����ֵ
		obj.setSaleId(Global.saleIdCounter++);
		obj.setSaleDate(saleDate);
		obj.setCakeCode(cakeCode);
		obj.setPrice(price);
		obj.setWeight(weight);
		obj.setSaleSum(saleSum);
		
        //���µ����ۼ�¼������ӵ�������
		Global.saleList.add(obj);
	}


	/**
	 * �����м�¼���浽�ļ���
	 *
	 */
	public void saveSaleRecord() {
		FileDBManager fdm = new FileDBManager();
		fdm.writeFileDB();
	}
	/**
	 * ��ӡ��ʾ��������
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
