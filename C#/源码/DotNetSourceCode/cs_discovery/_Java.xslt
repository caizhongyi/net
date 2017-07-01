<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--

    根据表结构XML文档创建影射数据库字段的 Java 代码
    
    编制 袁永福 2008-1-17
    
-->
    <xsl:output method="xml" indent="yes" omit-xml-declaration="no" />
    <xsl:template match="/*">
		<xsl:if test="name(.) != 'Table' ">
			<font color="red">本模板只能用于单表</font>
			<br />
		</xsl:if>
        <textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd'>
-------------- 文件名 <xsl:value-of select="Name" />.java -----------------------------
package com.skytech.struct;
import java.sql.*;
import java.io.*;
/**   <xsl:value-of select="Remark" />
 *    @author 代码生成器 */
public class <xsl:value-of select="Name" /> implements Serializable{

        // 返回数据表名的函数
        public static final String getTableName(){
            return "<xsl:value-of select="Name" />" ;
        }
        
        // 返回字段名数组
        public static final String[] getFieldNames(){
             return new string[]{ <xsl:for-each select="Fields/Field"><xsl:text>
                                 </xsl:text><xsl:if test="position()>1">,</xsl:if>"<xsl:value-of select="Name" />"</xsl:for-each>
                               };
        }
        
        // 定义数据库字段变量 /////////////////////////////////////////////////
        <xsl:for-each select="Fields/Field">
        <xsl:variable name="javatype">
            <xsl:choose>
                <xsl:when test="ValueTypeName='System.Boolean'">Boolean</xsl:when>
                <xsl:when test="ValueTypeName='System.Int32'">int</xsl:when>
                <xsl:when test="ValueTypeName='System.DateTime'">DateTime</xsl:when>
                <xsl:when test="ValueTypeName='System.Double'">double</xsl:when>
                <xsl:when test="ValueTypeName='System.Byte[]'">byte[]</xsl:when>
                <xsl:when test="ValueTypeName='System.String'">String</xsl:when>
                <xsl:otherwise>Object</xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="remark">
			<xsl:if test="string-length( Remark ) > 0 ">(<xsl:value-of select="Remark" />)</xsl:if>
		</xsl:variable>
        // 字段 <xsl:value-of select="Name" /><xsl:value-of select="$remark" />
        private <xsl:value-of select="$javatype" /><xsl:text> </xsl:text>m_<xsl:value-of select="Name" /> ;
        // 获得字段 <xsl:value-of select="Name" /><xsl:value-of select="$remark" /> 值
        public <xsl:value-of select="$javatype" /> get<xsl:value-of select="Name" />(){
            return m_<xsl:value-of select="Name" />;
        }
        // 设置字段 <xsl:value-of select="Name" /><xsl:value-of select="$remark" /> 值
        public void set<xsl:value-of select="Name" />( <xsl:value-of select="$javatype" /> Value ){
            m_<xsl:value-of select="Name" /> = Value ;
        }
		</xsl:for-each>
        public String toString (){
            return <xsl:for-each select="Fields/Field">"<xsl:if test="position() != 1">,</xsl:if><xsl:value-of select="Name" />="+ m_<xsl:value-of select="Name" /><xsl:if test="position() != last()"> 
                   + </xsl:if> </xsl:for-each>;
        }
} // 类 <xsl:value-of select="Name" /> 定义结束
</textarea>
    </xsl:template>
</xsl:stylesheet>