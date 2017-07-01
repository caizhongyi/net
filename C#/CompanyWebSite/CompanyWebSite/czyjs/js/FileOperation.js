//Drive对象负责收集系统中的物理或逻辑驱动器资源内容，它具有如下属性：
// TotalSize：以字节（byte）为单位计算的驱动器大小。
// AvailableSpace或FreeSpace：以字节（byte）为单位计算的驱动器可用空间。
// DriveLetter：驱动器字母。
// DriveType：驱动器类型，取值为：removable（移动介质）、fixed（固定介质）、network（网络资源）、CD-ROM或者RAM盘。
// SerialNumber：驱动器的系列码。
// FileSystem：所在驱动器的文件系统类型，取值为FAT、FAT32和NTFS。
// IsReady：驱动器是否可用。
// ShareName：共享名称。
// VolumeName：卷标名称。
// Path和RootFolder：驱动器的路径或者根目录名称。

function GetFsObj()
{
  return new ActiveXObject("Scripting.FileSystemObject");
}
//创建文件
function CreateFile(path)
{
  var fso=GetFsObj();
  return fso.createtextfile(path,true);
}
//获取文件对像
function GetFile(path)
{
  var fso=GetFsObj();
  return fso.GetFile(path);
}
//写入文件
function FileWrite(path,str)
{
var fso, tf;
fso = GetFsObj();
// 创建新文件
tf = fso.CreateTextFile(path, true);
// 填写数据，并增加换行符
tf.WriteLine(str) ;
// 增加3个空行
//tf.WriteBlankLines(3) ;
// 填写一行，不带换行符
//tf.Write ("This is a test.");
// 关闭文件
tf.Close();
}

function GetDriverMessage(driverPath)
{
  var fso, fldr, s = "";
// 创建FileSystemObject对象实例
  fso=GetFsObj();
// 获取Drive 对象
fldr = fso.GetFolder(driverPath);
// 显示父目录名称
//alert("Parent folder name is: " + fldr + "\n");
// 显示所在drive名称
return fldr.Drive ;
// 判断是否为根目录
//if (fldr.IsRootFolder)
//alert("This is the root folder.");
//else
//alert("This folder isn't a root folder.");
//alert("\n\n");
// 创建新文件夹
//fso.CreateFolder ("C:\\Bogus");
//alert("Created folder C:\\Bogus" + "\n");
// 显示文件夹基础名称，不包含路径名
//alert("Basename = " + fso.GetBaseName("c:\\bogus") + "\n");
// 删除创建的文件夹
//fso.DeleteFolder ("C:\\Bogus");
//alert("Deleted folder C:\\Bogus" + "\n");
}



//----------------------------------------------------------------------------------------
//BuildPath()
//生成一个文件路径
//CopyFile() 复制文件
//CopyFolder() 复制目录
//CreateFolder() 创建新目录
//CreateTextFile() 生成一个文件
//DeleteFile() 删除一个文件
//DeleteFolder() 删除一个目录
//DriveExists() 检验盘符是否存在
//Drives 返回盘符的集合
//FileExists() 检验文件是否存在
//FolderExists 检验一个目录是否存在
//GetAbsolutePathName() 取得一个文件的绝对路径
//GetBaseName() 取得文件名
//GetDrive() 取得盘符名
//GetDriveName() 取得盘符名
//GetExtensionName() 取得文件的后缀
//GetFile() 生成文件对象
//GetFileName() 取得文件名
//GetFolder() 取得目录对象
//GetParentFolderName 取得文件或目录的父目录名
//GetSpecialFolder() 取得特殊的目录名
//GetTempName() 生成一个临时文件对象
//MoveFile() 移动文件
//MoveFolder() 移动目录
//OpenTextFile() 打开一个文件流

//f.Files //目录下所有文件集合
//f.attributes //文件属性
//Case 0 Str="普通文件。没有设置任何属性。 "
//Case 1 Str="只读文件。可读写。 "
//Case 2 Str="隐藏文件。可读写。 "
//Case 4 Str="系统文件。可读写。 "
//Case 16 Str="文件夹或目录。只读。 "
//Case 32 Str="上次备份后已更改的文件。可读写。 "
//Case 1024 Str="链接或快捷方式。只读。 "
//Case 2048 Str=" 压缩文件。只读。"
//f.Datecreated // 创建时间
//f.DateLastAccessed //上次访问时间
//f.DateLastModified // 上次修改时间
//f.Path //文件路径
//f.Name //文件名称
//f.Type //文件类型
//f.Size // 文件大小（单位：字节）
//f.ParentFolder //父目录
//f.RootFolder // 根目录
//实例说明
//BuildPath(路径,文件名) //这个方法会对给定的路径加上文件，并自动加上分界符
function BuildPath(path,fileName)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
var newpath = fso.BuildPath(path, fileName); //生成 c:\tmp\51js.txt的路径
return newpath;
}
//CopyFile(源文件, 目标文件, 覆盖) //复制源文件到目标文件，当覆盖值为true时，如果目标文件存在会把文件覆盖
function CopyFile(formPath,toPath)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
var newpath = fso.CopyFile(formPath, toPath);
return newpath;
}
//CopyFolder(对象目录,目标目录 ,覆盖) //复制对象目录到目标目录，当覆盖为true时，如果目标目录存在会把文件覆盖
function CopyFolder(formFolder,toFolder)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
fso.CopyFolder(formFolder, toFolder); //把C盘的Desktop目录复制到D盘的根目录
}

//CreateFolder(目录名) //创建一个新的目录
function CreateFolder(folderName)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
var newFolderName = fso.CreateFolder(folderName); //在C盘上创建一个51JS的目录
return newFolderName;
}
//CreateTextFile(文件名, 覆盖) //创建一个新的文件，如果此文件已经存在，你需要把覆盖值定为true
function CreateTextFile(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
var newFileObject = fso.CreateTextFile(path, true); //脚本将在C盘创建一个叫 autoexec51JS.bat的文件
return newFileObject;
}
//DeleteFile(文件名, 只读？) //删除一个文件，如果文件的属性是只读的话，你需要把只读值设为true
function DeleteFile(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject"); //为了安全我先把要删除的autoexec.bat备份到你的D盘
//var newpath = fso.CopyFile("c:\\autoexec.bat", "d:\\autoexec.bat"); //把C盘的autoexec.bat文件删除掉
fso.DeleteFile(path, true);
}
//DeleteFolder(文件名, 只读？)//删除一个目录，如果目录的属性是只读的话，你需要把只读值设为true
function DeleteFolder(folderPath)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
//fso.CopyFolder("c:\\WINDOWS\\Desktop", "d:\\"); //为了安全我先把你C盘的Desktop目录复制到你D盘的根目录
fso.DeleteFolder(folderPath, true); //把你的Desktop目录删除，但因为desktop是系统的东西，所以不能全部删除，但.........
}
//DriveExists(盘符) //检查一个盘是否存在，如果存在就返会真，不存在就返回.......
function DriveExists(driveName)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
hasDriveD = fso.DriveExists(driveName); //检查系统是否有D盘存在
//hasDriveZ = fso.DriveExists("z"); //检查系统是否有Z盘存在
//if (hasDriveD) alert("你的系统内有一个D盘");
if (!hasDriveD){return false;}else{return true;}//true存在
}
//FileExists(文件名) //检查一个文件是否存在，如果存在就返会真，不存在就返回.......
function FileExists(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
fileName = fso.FileExists(path);
if (fileName) {return true}else{return false}; //true为存在
}
//FolderExists(目录名) //检查一个目录是否存在，如果存在就返会真，不存在就返回.......
function FolderExists(folder)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
folderName = fso.FolderExists(folder);
if (folderName) {return ture;}else{return false;} //开个玩笑:)
}
//GetAbsolutePathName(文件对象) //返回文件对象在系统的绝对路径
function GetAbsolutePathName(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
pathName = fso.GetAbsolutePathName(path);
return pathName;
}
//GetBaseName(文件对象) //返回文件对象的文件名
function GetBaseName(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
baseName = fso.GetBaseName(path); //取得autoexec.bat的文件名autoexec
return baseName;
}
//GetExtensionName(文件对象) //文件的后缀
function GetExtensionName(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
exName = fso.GetExtensionName(path); //取得autoexec.bat后缀bat
return exName;
}

//GetParentFolderName(文件对象) //取得父级的目录名
function GetParentFolderName(path)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
parentName = fso.GetParentFolderName(path); //取得autoexec.bat的父级目录C盘
return parentName;
}

//GetSpecialFolder(目录代码) //取得系统中一些特别的目录的路径，目录代码有3个分别是 0:安装Window的目录 1:系统文件目录 2:临时文件目录
function GetSpecialFolder()
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
tmpFolder = fso.GetSpecialFolder(2); //取得系统临时文件目录的路径 如我的是 C:\windows\temp
return tmpFolder;
}

//GetTempName() //生成一个随机的临时文件对象，会以rad带头后面跟着些随机数，就好象一些软件在安装时会生成*.tmp
function GetTempName()
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
var tmpName = fso.GetTempName(); //我在测试时就生成了radDB70E.tmp
}
//MoveFile(源文件, 目标文件) //把源文件移到目标文件的位置
function MoveFile(fromPath,toPath)
{
var fso = new ActiveXObject("Scripting.FileSystemObject");
var newpath = fso.MoveFile(fromPath, toPath); //把C盘的autoexec.bat文件移移动到D盘
}