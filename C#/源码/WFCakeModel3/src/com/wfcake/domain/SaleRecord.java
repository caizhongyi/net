package com.wfcake.domain;

/**
 * 销售记录
 *
 * @author WFCake
 * @version 1.0
 *
 */
public class SaleRecord {
	// 销售记录ID
	private int saleId;

	// 销售日期
	private String saleDate;

	// 蛋糕代码
	private String cakeCode;

	// 蛋糕重量
	private float weight;

	// 蛋糕价格
	private float price;

	// 本次销售的金额
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
