package com.wfcake.manager;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.FileWriter;

import com.wfcake.common.Const;
import com.wfcake.common.Global;
import com.wfcake.domain.SaleRecord;

/**
 * ��¼�ļ�����
 * 
 * @author WFCake
 * @version 1.0
 * 
 */
public class FileDBManager {
	/**
	 * ������ת���ɼ�¼�ַ��� ����: 1,2008-08-10,A01,35,2,70
	 * 
	 * @return
	 */
	public String convertObjToStr(SaleRecord obj) {
		String line = "";
		line = line + obj.getSaleId() + ",";
		line = line + obj.getSaleDate() + ",";
		line = line + obj.getCakeCode() + ",";
		line = line + obj.getPrice() + ",";
		line = line + obj.getWeight() + ",";
		// Ϊ�˱�֤ÿ��ֻ��һ����¼, �ڼ�¼β����س���
		line = line + obj.getSaleSum() + "\n";
		return line;
	}

	/**
	 * ����¼�ַ���ת���ɶ��� ����: 1,2008-08-10,A01,35,2,70
	 * 
	 * @return
	 */
	public SaleRecord convertStrToObj(String line) {
		String[] tokens = line.split(",");
		SaleRecord obj = new SaleRecord();
		obj.setSaleId(Integer.parseInt(tokens[0]));
		obj.setSaleDate(tokens[1]);
		obj.setCakeCode(tokens[2]);
		obj.setPrice(Float.parseFloat(tokens[3]));
		obj.setWeight(Float.parseFloat(tokens[4]));
		obj.setSaleSum(Float.parseFloat(tokens[5]));
		return obj;
	}

	/**
	 * ���ļ��ж�ȡ��¼�����ұ��浽Global.saleList�б���
	 * 
	 */
	@SuppressWarnings("unchecked")
	public void readFileDB() {
		try {
			FileReader fr = new FileReader(Const.FILEDBNAME);
			BufferedReader br = new BufferedReader(fr);
			//��Const.FILEDBNAME���ļ���ÿ�ζ�ȡһ�м�¼
			String line = br.readLine();
			/**
			 * �õ���ǰ����¼��
			 */
			int maxSaleIdCounter = 0;

			while (line != null) {
				if (line.length() == 0) {
					break;
				}
				maxSaleIdCounter++;
				// ��һ�б��һ����¼����
				SaleRecord obj = this.convertStrToObj(line);
				//���ַ���ת���ɵĶ��󱣴浽Global.saleList�б���
				Global.saleList.add(obj);
				//��ȡ��һ��
				line = br.readLine();
			}
			//���ۼ�¼ID���кŵ���ֵ����
			Global.saleIdCounter = maxSaleIdCounter + 1;
			
			br.close();
			fr.close();

		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * ����¼д���ļ�
	 * 
	 */
	public void writeFileDB() {
		try {
			FileWriter fw = new FileWriter(Const.FILEDBNAME);
			int size = Global.saleList.size();
			
			for (int i = 0; i < size; i++) {
				
				SaleRecord obj = (SaleRecord) Global.saleList.get(i);
				
				//�Ѵ�Global.saleList��ȡ�ļ�¼ת�����ַ���
				String line = this.convertObjToStr(obj);

				//����ȡ���ַ������浽Const.FILEDBNAME��Ӧ���ļ���
				fw.write(line);
			}
			
			fw.close();
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}
}
