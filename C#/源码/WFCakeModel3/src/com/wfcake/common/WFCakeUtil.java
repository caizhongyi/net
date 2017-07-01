package com.wfcake.common;

import java.text.SimpleDateFormat;



/**
 * ������
 * 
 * @author WFCake
 * @version 1.0
 * 
 */

public class WFCakeUtil {
	/**
	 * �õ���Ӧ��ŵļ۸�
	 * 
	 * @return ��Ӧ��ŵļ۸�
	 */
	public int getPrice(String cakeCode) {
		//ȡ�õ��������������ĳ���
		int cakeSize = Const.CAKEINFO.length;
		int price = 0;
		//�ڵ�������������������в��Ҷ�Ӧ��ŵļ۸�
		for (int n = 0; n < cakeSize; n++) {
			if (cakeCode.equals(Const.CAKEINFO[n][0])) {
				price = Integer.parseInt(Const.CAKEINFO[n][2]);
			}
		}
		return price;
	}
	/**
	 * �õ���Ӧ��ŵ�����
	 * 
	 * @return ��Ӧ��ŵ�����
	 */
	public String getCakeName(String cakeCode) {
		//ȡ�õ��������������ĳ���
		int cakeSize = Const.CAKEINFO.length;
		String cakeName = null;
		//�ڵ�������������������в��Ҷ�Ӧ��ŵ�����
		for (int n = 0; n < cakeSize; n++) {
			if (cakeCode.equals(Const.CAKEINFO[n][0])) {
				cakeName = Const.CAKEINFO[n][1];
			}
		}
		return cakeName;
	}	
	

	/**
	 * �õ���ǰ����
	 * 
	 * @return
	 */
	public String getCurrentDate() {
		//����������ڵĸ�ʽ
		SimpleDateFormat tempDate = new SimpleDateFormat("yyyy-MM-dd");
		//��ȡ��ǰ��ʱ�䣬���Ұ��չ涨�ĸ�ʽ���浽dateTime��
		String dateTime = tempDate.format(new java.util.Date());
		return dateTime;
	}

	/**
	 * ��ʽ����ʾ�ֶ�
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
