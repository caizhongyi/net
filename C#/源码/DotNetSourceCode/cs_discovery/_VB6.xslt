<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--

    根据表结构XML文档创建影射数据库字段的 VB6 代码
    
    编制 袁永福 2008-1-17
    
-->
    <xsl:output method="xml" indent="yes" omit-xml-declaration="no" />
    <xsl:template match="/*">
		<xsl:if test="name(.) != 'Table' ">
			<font color="red">本模板只能用于单表</font>
			<br />
		</xsl:if>
        <textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd'>    
            <xsl:variable name="classname">
                <xsl:value-of select="concat('DB_' , Name)" />
            </xsl:variable>
'*****************************************************************************
'  文件名 <xsl:value-of select="Name" />.cls
'*****************************************************************************

' 数据库表 <xsl:value-of select="Name" />
                <xsl:if test="Remark!=''">
                    <xsl:value-of select="concat(' [',Remark,']')" />
                </xsl:if> 操作对象
' 
' 该表有<xsl:value-of select="count(Fields/Field)" />个字段
' 编制： 代码生成器
' 时间：
'
public class <xsl:value-of select="$classname" />
{   
    ' 返回数据表名称 <xsl:value-of select="Name" />
    Public Const TableName As String = "<xsl:value-of select="Name" />" 
    
    ' 返回字段名列表
    Public Const FieldNames As String = "<xsl:for-each select="Fields/Field"><xsl:if test="position()>1">,</xsl:if><xsl:value-of select="Name" /></xsl:for-each>"
    
    ' 定义数据库字段变量及属性 //////////////////////////////////////////
    
    <xsl:for-each select="Fields/Field">
    <xsl:variable name="vbtype">
		<xsl:choose>
			<xsl:when test="ValueTypeName='System.Boolean'">Boolean</xsl:when>
			<xsl:when test="ValueTypeName='System.Int32'">Integer</xsl:when>
			<xsl:when test="ValueTypeName='System.DateTime'">Date</xsl:when>
			<xsl:when test="ValueTypeName='System.Double'">Double</xsl:when>
			<xsl:when test="ValueTypeName='System.Byte[]'">byte()</xsl:when>
			<xsl:when test="ValueTypeName='System.String'">String</xsl:when>
			<xsl:otherwise>Variant</xsl:otherwise>
		</xsl:choose>
    </xsl:variable>
    <xsl:variable name="remark">
		<xsl:if test="string-length( Remark ) > 0 ">(<xsl:value-of select="Remark" />)</xsl:if>
    </xsl:variable>
    
    ' 字段 <xsl:value-of select="Name" /><xsl:value-of select="$remark" />
    Private m_<xsl:value-of select="Name" /> As <xsl:value-of select="$vbtype" />
    ' 获得字段 <xsl:value-of select="Name" /><xsl:value-of select="$remark" /> 的值
    Public Property Get <xsl:value-of select="Name" />() As <xsl:value-of select="$vbtype" /><xsl:text>
        </xsl:text><xsl:value-of select="Name" /> = m_<xsl:value-of select="Name" />
    End Property
    ' 设置字段 <xsl:value-of select="Name" /><xsl:value-of select="$remark" /> 的值
    Public Property Set <xsl:value-of select="Name" />( Byval Value As <xsl:value-of select="$vbtype" />)
        m_<xsl:value-of select="Name" /> = Value
    End Property
    </xsl:for-each>
    
    '** 定义从数据库记录集获得数据的方法 **
    Public Function ReadRecordset(ByVal rs As ADODB.Recordset) As Boolean
        On Error GoTo SelectErr
        SelectRS = False
        <xsl:for-each select="Fields/Field"><xsl:text>
        </xsl:text>m_<xsl:value-of select="Name" /> = rs.Fields(<xsl:value-of select="position()-1" />).Value  '** 字段 <xsl:value-of select="Name" />
        </xsl:for-each>
        ReadRecordset = True
        Exit Function
SelectErr:
        ReadRecordset = False
    End Function
  
</textarea>
    </xsl:template>
</xsl:stylesheet>