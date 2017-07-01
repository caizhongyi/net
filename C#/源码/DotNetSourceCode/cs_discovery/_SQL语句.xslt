<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--

    根据表结构XML文档创建创建数据表，新增，修改和删除表记录的SQL语句
    
    编制 袁永福 2008-1-17
    
-->
	<xsl:output method="xml" indent="yes" />
	<xsl:template match="/*">
		<xsl:for-each select="//Table">
			<xsl:call-template name="table" />
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="table">
----------- 数据表 <b><xsl:value-of select="Name" /></b> 的SQL语句 <xsl:value-of select="count(Fields/Field)" />个字段 --------
<br />创建表<br />
<textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd;width:100%'>
CREATE TABLE <xsl:value-of select="Name" />( <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
     </xsl:text>
			<xsl:value-of select="Name" />
			<xsl:text> </xsl:text>
			<xsl:value-of select="FieldType" />
			<xsl:if test="IsString='true'">(<xsl:value-of select="FieldWidth" />)</xsl:if>
			<xsl:if test="Nullable='false'"> NOT NULL</xsl:if>
			<xsl:if test="Nullable='true'"> NULL</xsl:if>
			<xsl:if test="position() != last()"> ,</xsl:if>
		</xsl:for-each> )
</textarea>		
<br />查询表<br />
<textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd;width:100%'>
Select <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
    </xsl:text>
			<xsl:value-of select="Name" />
			<xsl:if test="position() != last()"> , </xsl:if>
		</xsl:for-each> 
From <xsl:value-of select="Name" />
</textarea>
<br />插入记录<br />
<textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd;width:100%'>
Insert <xsl:value-of select="Name" /> ( <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
    </xsl:text>
			<xsl:value-of select="Name" />
			<xsl:if test="position() != last()"> , </xsl:if>
		</xsl:for-each>
		<xsl:text> )
 Values ( </xsl:text>
		<xsl:for-each select="Fields/Field">
			<xsl:text> ? </xsl:text>
			<xsl:if test="position() != last()"> , </xsl:if>
		</xsl:for-each>
		<xsl:text> ) </xsl:text>
</textarea>
<br />更新记录<br />
<textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd;width:100%'>
Update <xsl:value-of select="Name" /> set <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
    </xsl:text>
			<xsl:value-of select="Name" />
			<xsl:text> = ? </xsl:text>
			<xsl:if test="position() != last()"> , </xsl:if>
		</xsl:for-each> 
</textarea>
<br />删除记录<br />
<textarea wrap='off' readonly='1' style='border:1 solid black;overflow=visible;background-color:#dddddd;width:100%'>
Delete From <xsl:value-of select="Name" />
</textarea>
<p />
 </xsl:template>
</xsl:stylesheet>