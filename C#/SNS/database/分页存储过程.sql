CREATE  PROCEDURE [NTP_Page] 
@IndexField varchar(50)='id',
@AllFields varchar(1000)='*', --��Ҫ���ص��� 
@TablesAndWhere varchar(1000)='', -- ����������,from�����,��Ҫfrom,Ҫһ��where
@OrderFields varchar(255)='',-- ������ֶ���
@PageSize int = 10, -- ҳ�ߴ�
@PageIndex int = 1, -- ҳ��
@RecordCount int output,
@PageCount int output
AS
if @PageSize < 1
set @PageSize = 10
declare @strSQL nvarchar(4000) -- �����
set @strSQL = 'select @RecordCount=count('+ @IndexField +') from '+ @TablesAndWhere
exec sp_executesql @strSQL,N'@RecordCount int output',@RecordCount out
if @RecordCount % @PageSize = 0
set @PageCount = @RecordCount/@PageSize
else
set @PageCount = @RecordCount/@PageSize+1
if(@PageIndex > @PageCount)
set @PageIndex = @PageCount
if @PageIndex < 1
set @PageIndex = 1
if @PageIndex = 1
set @strSQL='select top ' + CAST(@PageSize as nvarchar) + ' ' + @AllFields + ' from '+ @TablesAndWhere + ' ' + @OrderFields
else
begin
declare @start int
set @start = (@PageIndex - 1) * @PageSize
set @strSQL= 'select top ' + CAST(@PageSize as nvarchar) + ' ' +  @AllFields  + ' from ' +  @TablesAndWhere + ' and '+ @IndexField  + ' not in (select top ' + CAST(@start as nvarchar) + ' ' + @IndexField + ' from ' + @TablesAndWhere + ' ' + @OrderFields +') '+ @OrderFields
end
exec sp_executesql @strSQL