package com.wfcake.common;

import java.util.ArrayList;
import java.util.List;

/**
 * 全局变量
 *
 * @author WFCake
 * @version 1.0
 *
 */
public class Global {
	// 销售记录列表
	public static List<Object> saleList = new ArrayList<Object>();;
	// 销售记录ID序列号, 序号自增长
	public static int saleIdCounter = 1;
}
