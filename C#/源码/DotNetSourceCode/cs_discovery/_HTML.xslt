<?xml version="1.0" encoding="gb2312" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<!--

    ���ݱ�ṹXML�ĵ�������ʾ���ݱ��ֶ���Ϣ��HTML�ĵ�
    
    ���� Ԭ���� 2008-1-17
    
-->
	<xsl:output indent='yes' />
	<xsl:template match="/*">
		<xsl:for-each select="//Table">
			<xsl:call-template name="table" />
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="table">
		<div>
			<span>
				<span>���ݱ� </span>
				<b>
					<xsl:value-of select="Name" />
				</b>
				<span> �ṹ,�� </span>
				<xsl:value-of select="count( Fields/Field )" />
				<span> ���ֶ� </span>
				<xsl:value-of select="concat(' ', Remark)" />
				<xsl:if test="count( Fields/Field[PrimaryKey='true']) > 0 ">
					<span>�ؼ��ֶ�:</span>
					<b>
						<xsl:for-each select="Fields/Field[PrimaryKey='true']">
							<xsl:if test="position()> 1 ">,</xsl:if>
							<xsl:value-of select="Name" />
						</xsl:for-each>
					</b>
				</xsl:if>
			</span>
			<table style="border: black 1 solid"
					border="0"  width="100%"
					rules='all'	cellspacing="0"
					cellpadding="0" bordercolor="#000000">
				<tr bgcolor="#eeee99">
					<td width="150">�ֶ���</td>
					<td width="80">�ֶ�����</td>
					<td width="50">����</td>
					<td width="80">�ɷ�Ϊ��</td>
					<td width="80">�Ƿ�����</td>
					<td>˵��</td>
				</tr>
				<xsl:for-each select="Fields/Field">
					<xsl:sort select="Name" />
					<tr valign="top">
						<xsl:attribute name="bgcolor">
							<xsl:if test="position() mod 2 = 0">#eeeeee</xsl:if>
						</xsl:attribute>
						<td>
							<font>
								<xsl:if test="PrimaryKey='true'">
									<xsl:attribute name="color">red</xsl:attribute>
								</xsl:if>
								<xsl:value-of select="concat(' ' , Name)" />
							</font>
						</td>
						<td>
							<xsl:value-of select="FieldType" />
						</td>
						<td>
							<xsl:value-of select="FieldWidth" />
						</td>
						<td>
							<xsl:if test="Nullable='true'">��</xsl:if>
						</td>
						<td>
							<xsl:if test="Indexed='true'">��</xsl:if>
						</td>
						<td>
							<xsl:value-of select="Remark" />
							<xsl:if test="Description !=''">
								<br />
								<xsl:value-of select="Description" />
							</xsl:if>
						</td>
					</tr>
				</xsl:for-each>
			</table>
		</div>
		<p />
	</xsl:template>
</xsl:stylesheet>