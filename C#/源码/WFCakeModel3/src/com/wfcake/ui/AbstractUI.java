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
	 * �˻���������<br>
	 * ע��, ��δ����ݴ���, ����: <br>
	 * 1. ������Ϊ{A01,A02,B01,B02,B03,C01,C02}, ���ִ�Сд;<br>
	 * 2. ��������Ϊ{1,2.5,3,4}��.
	 * 
	 */
	public void showInterface() {
		// ��ʾ��ӭ��
		System.out.print("\n            ��ӭ��������ϵͳ!\n\n");

/*		// �˻���������Begin
		System.out.print("�����뵰����: ");
		Scanner sacnner = new Scanner(System.in);
		String cakeCode = sacnner.next().trim().toUpperCase();
        //��ȡ�û���������
		System.out.print("�����뵰������: ");
		// ����û�����ݴ���, �����������׼ȷ, ����ᱨ��ָ���쳣
		sacnner = new Scanner(System.in);
		float weight = Float.parseFloat(sacnner.next().trim());

		WFCakeUtil aUtil = new WFCakeUtil();
		float price = aUtil.getPrice(cakeCode);
		float saleSum = price * weight;

		// ��ʾӦ��, ʵ�ռ�������
		System.out.println("Ӧ��: " + saleSum + " Ԫ.");
		System.out.print("������ʵ�ս��: ");
		sacnner = new Scanner(System.in);
		int infactMoney = Integer.parseInt(sacnner.next().trim());
		System.out.println("����: " + (infactMoney - saleSum) + " Ԫ.");

		//��������¼���浽List
		saleManager.addSaleRecord(cakeCode, weight);
		//�����¼��SaleDate.csv��
		saleManager.saveSaleRecord();	
		//��ʾ��¼
		saleManager.showSale();*/
		
		SaleManagerWithFile fm = new SaleManagerWithFile();
		for(int i=0; i<Global.saleList.size(); i++){
			SaleRecord sal =(SaleRecord) Global.saleList.get(i);
			System.out.println(fm.convertObjToStr(sal));
		}
		
	}

}
