<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--

    根据表结构XML文档创建影射数据库字段的 C# 代码
    
    编制 袁永福 2008-1-17
    
-->
    <xsl:template match="/*">
		<xsl:if test="name(.) != 'Table' ">
			<font color="red">本模板只能用于单表</font>
			<br />
		</xsl:if>
        <textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd'>    
            <xsl:variable name="classname">
                <xsl:value-of select="concat('DB_' , Name)" />
            </xsl:variable>
//*****************************************************************************
//  文件名 <xsl:value-of select="Name" />.cs 
//*****************************************************************************
/// <summary>
/// 数据库表 <xsl:value-of select="Name" />
                <xsl:if test="Remark!=''">
                    <xsl:value-of select="concat(' [',Remark,']')" />
                </xsl:if> 操作对象
/// </summary>
/// <remark>
/// 该表有<xsl:value-of select="count(Fields/Field)" />个字段
/// 编制： 代码生成器
/// 时间：
///</remark>
[System.Serializable()]
public class <xsl:value-of select="$classname" />
{   
    ///<summary>返回数据表名称 <xsl:value-of select="Name" /></summary>
    public static string TableName
    {
        get{ return "<xsl:value-of select="Name" />" ; }
    }

    ///<summary>返回所有字段的名称</summary>    
    public static string[] FieldNames
    {
        get
        {
            return new string[]{ <xsl:for-each select="Fields/Field"><xsl:text>
                                 </xsl:text><xsl:if test="position()>1">,</xsl:if>"<xsl:value-of select="Name" />"</xsl:for-each>
                               };
                                 
        }
    }
    
    #region 定义数据库字段变量及属性 //////////////////////////////////////////
    
    <xsl:for-each select="Fields/Field">
    <!-- 定义默认值 -->
    <xsl:variable name="defaultvalue">
                    <xsl:choose>
                        <xsl:when test="ValueTypeName='System.Boolean'">true</xsl:when>
                        <xsl:when test="ValueTypeName='System.Byte' or ValueTypeName='System.Int32'">0</xsl:when>
                        <xsl:when test="ValueTypeName='System.Single' or ValueTypeName= 'System.Double'">0</xsl:when>
                        <xsl:when test="ValueTypeName='System.DateTime'">DateTime.Now</xsl:when>
                        <xsl:otherwise>null</xsl:otherwise>
                    </xsl:choose>
                </xsl:variable>
    <xsl:variable name="remark">
		<xsl:if test="string-length( Remark ) > 0 ">(<xsl:value-of select="Remark" />)</xsl:if>
	</xsl:variable>
    ///<summary>
    /// 字段值 <xsl:value-of select="Name" /><xsl:value-of select="$remark" />
    ///</summary>
    private <xsl:value-of select="ValueTypeName" /> m_<xsl:value-of select="Name" /> = <xsl:value-of select="$defaultvalue" /> ;
    ///<summary>
    /// 字段值 <xsl:value-of select="Name" /><xsl:value-of select="$remark" /><xsl:if test="PrimaryKey='true'">[关键字段]</xsl:if>
    ///</summary>
    public <xsl:value-of select="ValueTypeName" /><xsl:text> </xsl:text><xsl:value-of select="Name" />
    {
        get{ return m_<xsl:value-of select="Name" /> ;}
        set{ m_<xsl:value-of select="Name" /> = value;}
    }
    </xsl:for-each>
    #endregion
}// 数据库操作类 <xsl:value-of select="$classname" /> 定义结束
</textarea>
    </xsl:template>
</xsl:stylesheet>