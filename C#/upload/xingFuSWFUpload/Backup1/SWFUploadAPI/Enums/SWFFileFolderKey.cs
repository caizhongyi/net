using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMMENSITY.SWFUploadAPI.Enums
{
    /// <summary>
    /// 文件存放的文件夹
    /// </summary>
    public enum SWFFileFolderKey
    {
        /// <summary>
        /// 在webConfig中appSettings节点下添加  add key="pic" value="文件夹名称"
        /// </summary>
        pic,
        /// <summary>
        /// 在webConfig中 appSettings 节点下添加 add key="image" value="文件夹名称"
        /// </summary>
        image,
        /// <summary>
        /// 在webConfig中 appSettings 节点下添加 add key="picture" value="文件夹名称"
        /// </summary>
        picture,
        /// <summary>
        /// 在webConfig中 appSettings 节点下添加 add key="file" value="文件夹名称"
        /// </summary>
        file,
        /// <summary>
        /// 旅游图片   在webConfig中 appSettings 节点下添加 add key="file" value="文件夹名称"
        /// </summary>
        travelPic
    }
}
