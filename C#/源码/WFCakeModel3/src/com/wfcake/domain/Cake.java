package com.wfcake.domain;

/**
 * �������
 * @author WFCake
 * @version 1.0
 *
 */
public class Cake {
	
	//������
	private String cakeCode;
	//��������
	private String cakeName;
	//����۸�
	private float price;
	
	public String getCakeId() {
		return cakeCode;
	}
	public void setCakeId(String cakeCode) {
		this.cakeCode = cakeCode;
	}
	public String getCakeName() {
		return cakeName;
	}
	public void setCakeName(String cakeName) {
		this.cakeName = cakeName;
	}
	public float getPrice() {
		return price;
	}
	public void setPrice(float price) {
		this.price = price;
	}

}
