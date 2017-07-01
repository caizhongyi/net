package com.wfcake.manager;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.FileWriter;

import com.wfcake.common.Const;
import com.wfcake.common.Global;
import com.wfcake.domain.SaleRecord;

/**
 * 记录文件操作
 * 
 * @author WFCake
 * @version 1.0
 * 
 */
public class FileDBManager {
	/**
	 * 将对象转换成记录字符串 例如: 1,2008-08-10,A01,35,2,70
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
		// 为了保证每行只有一条记录, 在记录尾加入回车符
		line = line + obj.getSaleSum() + "\n";
		return line;
	}

	/**
	 * 将记录字符串转换成对象 例如: 1,2008-08-10,A01,35,2,70
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
	 * 从文件中读取记录，并且保存到Global.saleList列表中
	 * 
	 */
	@SuppressWarnings("unchecked")
	public void readFileDB() {
		try {
			FileReader fr = new FileReader(Const.FILEDBNAME);
			BufferedReader br = new BufferedReader(fr);
			//从Const.FILEDBNAME的文件中每次读取一行记录
			String line = br.readLine();
			/**
			 * 得到当前最大记录号
			 */
			int maxSaleIdCounter = 0;

			while (line != null) {
				if (line.length() == 0) {
					break;
				}
				maxSaleIdCounter++;
				// 将一行变成一个记录对象
				SaleRecord obj = this.convertStrToObj(line);
				//将字符串转化成的对象保存到Global.saleList列表中
				Global.saleList.add(obj);
				//读取下一行
				line = br.readLine();
			}
			//销售记录ID序列号的数值更新
			Global.saleIdCounter = maxSaleIdCounter + 1;
			
			br.close();
			fr.close();

		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * 将记录写入文件
	 * 
	 */
	public void writeFileDB() {
		try {
			FileWriter fw = new FileWriter(Const.FILEDBNAME);
			int size = Global.saleList.size();
			
			for (int i = 0; i < size; i++) {
				
				SaleRecord obj = (SaleRecord) Global.saleList.get(i);
				
				//把从Global.saleList读取的记录转化成字符串
				String line = this.convertObjToStr(obj);

				//将读取的字符串保存到Const.FILEDBNAME对应的文件中
				fw.write(line);
			}
			
			fw.close();
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}
}
