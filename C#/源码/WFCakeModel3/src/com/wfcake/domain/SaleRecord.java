package com.wfcake.domain;

/**
 * ���ۼ�¼
 *
 * @author WFCake
 * @version 1.0
 *
 */
public class SaleRecord {
	// ���ۼ�¼ID
	private int saleId;

	// ��������
	private String saleDate;

	// �������
	private String cakeCode;

	// ��������
	private float weight;

	// ����۸�
	private float price;

	// �������۵Ľ��
	private float saleSum;

	public String getCakeCode() {
		return cakeCode;
	}

	public void setCakeCode(String cakeCode) {
		this.cakeCode = cakeCode;
	}

	public float getPrice() {
		return price;
	}

	public void setPrice(float price) {
		this.price = price;
	}

	public String getSaleDate() {
		return saleDate;
	}

	public void setSaleDate(String saleDate) {
		this.saleDate = saleDate;
	}

	public int getSaleId() {
		return saleId;
	}

	public void setSaleId(int saleId) {
		this.saleId = saleId;
	}

	public float getSaleSum() {
		return saleSum;
	}

	public void setSaleSum(float saleSum) {
		this.saleSum = saleSum;
	}

	public float getWeight() {
		return weight;
	}

	public void setWeight(float weight) {
		this.weight = weight;
	}

}
