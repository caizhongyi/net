--------------------------------------------------------------------------------
		  飞鸽传书 (IP Messenger) 2007 绿色版	
		Copyright (C) 1996-2006 SHIROUZU Hiroaki
			All Rights Reserved.
--------------------------------------------------------------------------------

目录:

  1. 软件简介
  2. 许可协议
  3. 系统要求
  4. 使用说明
  5. 相关信息
  6. 广域网设置(广播设置)
  7. 补充
  8. 支持
  9. 更新历史
 10. 感谢

================================================================================
  重要说明: comctl32.dll(公共控件) 要求 4.71 或以上版本
  更多信息请参见 "系统要求"
================================================================================

--------------------------------------------------------------------------------
1. 软件简介

 - IPMsg 是一款局域网内即时通信软件, 基于 TCP/IP(UDP).
   可运行于多种操作平台(Win/Mac/UNIX/Java), 并实现跨平台信息交流.

 - 不需要服务器支持.

 - 支持文件/文件夹的传送 (2.00版以上)

 - 通讯数据采用 RSA/Blofish 加密 (2.00版以上)

 - 十分小巧, 简单易用, 而且你可以完全免费使用它

 - 目前已有的版本包括: Win32, Win16, MacOS, MacOSX, X11, GTK, GNOME,
   Java 等, 并且公开源代码.
   请查看以下地址以获得相关信息:
   http://www.ipmsg.org/

--------------------------------------------------------------------------------
2. 许可协议 (BSD License)

  Copyright (c) 1996-2004 SHIROUZU Hiroaki All rights reserved.

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions
  are met:

    Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer. 

    Redistributions in binary form must reproduce the above copyright
    notice, this list of conditions and the following disclaimer in
    the documentation and/or other materials provided with the
    distribution.

    Neither the name of the SHIROUZU Hiroaki nor the names of its
    contributors may be used to endorse or promote products derived
    from this software without specific prior written permission. 

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
  BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
  ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
  POSSIBILITY OF SUCH DAMAGE.

--------------------------------------------------------------------------------
3. 系统要求

   Microsoft Windows 95/98/Me/NT4.0/2000/XP/2003
   95/98/NT4.0 ... 要求 comctl32.dll(公共控件) 的版本为 5.x 或更高.
   (如果你安装的 IE 是 5.x 或更高版本, 那么就不必担心这个问题了)

   注意: Windows 3.1(和 NT3.5x), 请使用 IP Messenger for Win16.

--------------------------------------------------------------------------------
4. 使用说明

 < 安装/卸载 >
   执行 setup.exe 你可以将 IPMsg 安装到指定目录, 或者重新注册到启动组.
   如果需要删除 IPMsg, 请先从控制面板中卸载, 再手动删除 IPMsg 目录.

 < 系统托盘区图标 >
   左键双击图标, 即可打开发送消息窗口.
   右键点击图标, 可进入 [服务设置], [离开] 等.

 < 发送消息窗口 >
 - 发送消息时, 若勾选了 [封装],  则接收者要先打开信封才能看到消息,
   如果你还勾选了 [上锁], 则接收者在打开信封时还需要输入密码.
   (密码是由接收者自己在 [服务设置] 中设置的)
 
 - 在发送信息窗口的右键功能菜单中，可以进行很多用户的自定义操作，将IPMSG个性化，
   如选择用户分组，自定义用户列表显示，搜索用户，
   设置窗口大小，固定窗口位置等。

 - 若要发送文件/文件夹, 可直接将文件/文件夹拖入发送消息窗口
   或在发送消息窗口上点击右键, 再选择发送文件或发送文件夹

   - 传送文件/文件夹时, 当接收者还没有保存(下载)文件/文件夹时,
     若发送者关闭或重启了 IPMsg, 则附带的文件信息将被清除, 
     接收者将不能继续接收(下载)到此文件.

 - 用户列表前缀符号说明
   ":" 表示用户处于离开模式.
   "|" 表示用户使用的不是 2.0 以上版本的 IPMsg,
       将不支持文件/文件夹的传送, 并且不支持通信数据加密.
   "|"(短线) 表示只支持文件/文件夹的传送.

 - 拖动列表表头标题项可改变其顺序, 并可点击右键选择"保存列表顺序"

 - 在消息输入窗口中, 可使用 Ctrl+Tab 输入制表符 Tab.

 - 在发送消息窗口上点击右键, 可以进入设置显示优先级, 选择工作组,
   搜索用户(Ctrl+F), 传送文件, 传送文件夹, 保存列表顺序, 字体设置,
   窗口大小设置, 固定窗口位置, 列表显示设置.

   - 在用户名上点击右键, 可设置其显示优先级
     优先级按由小到大的顺序排序
   - 通过设置显示优先级, 可将经常联系的用户至于列表顶端, 
     或者隐藏不需要联系的用户

 < 接收消息窗口 >
 - 在接收消息窗口上点击右键, 可以进入字体设置,
   窗口大小设置, 固定窗口位置.

 - 接收消息窗口标题栏中的 "+" 或 "-" 表示通信数据使用了加密算法
   "+" 表示 RSA/1024 位, blowfish/128 位加密
   "-" 表示 RSA/512 位, RC2/40 位加密

 - 如果你收到的消息附带了文件, 将会显示出附件按钮.
   点击按钮即可保存文件.

 < 其他 >
 - 如果需要(通过路由器)连接到广域网, 则需要设置广播地址.
   详见广域网设置(广播设置)

 - 其它功能都很容易理解, 你试一试就明白了

--------------------------------------------------------------------------------
5. 相关信息

 - 所有的设置信息都保存在注册表的以下位置:
   \\HKEY_CURRENT_USER\Software\HSTools\

   用户密码以不可逆加密方式存储.
   *******************************************************************
   * 如果你忘记了密码, 可以在注册表中将此键值删除                    *
   * \\HKEY_CURRENT_USER\Software\HSTools\IPMsgEng\PasswordStr       * 
   *******************************************************************

 - 本软件使用的默认端口为 2425
   若仅使用 UDP 协议的端口, 将不能传送文件/文件夹
   (如果安装了防火墙, 则必须打开相应的 TCP 和 UDP 端口)

 - 本软件为自由软件, 你可以随意传播, 但源码使用请参见许可协议.

 - 只有在启动或退出程序, 使用离开模式, 刷新在线用户时 IPMsg 才会进行消息广播.

 - 本软件由 Microsoft Visual C++ 6.0 编译

--------------------------------------------------------------------------------
6. 广域网设置(广播设置)

 - 主机号全部为 1 的 IP 地址, 即广播地址
   例如, 连接到一个 C 类子网(即 24 位网络号, 8 位主机号), IP 地址为
   aaa.bbb.ccc.ddd, 其广播地址即为: aaa.bbb.ccc.255
   若对方处于另一个私有子网中, 广播可能无效.

 - 更多问题, 请参阅相关资料或咨询你的网络管理员.

 - 若两台主机的连接经过了多个路由器, 请直接指定对方IP地址

 - 拨号上网用户请勾选 [拨号连接]
   当刷新在线用户列表时, 列表不会被清空

--------------------------------------------------------------------------------
7. 补充

 - 启动飞鸽传书前, 你可以指定其运行时使用的端口, 
   且可以使用不同的端口打开多个窗口. 用法如下：
   ipmsg.exe 2426 (你可在快捷方式上设置)
   但是你只能与同时也使用该端口的用户通信.

 - 所以你尽可选用你喜欢的端口运行本软件.
   介于 10000 至 60000 可能更安全些.
   你也可以咨询你的网络管理员.

 - 如果有多个网卡(IP), 你可以将飞鸽传书与指定的网卡(IP)进行绑定.
   命令格式如下(你可以在快捷方式上设置):
   ipmsg.exe [端口] /NIC IP地址
   例如:
   C:\>ipmsg.exe /NIC 192.168.10.100

 - 支持命令方式发送消息
   命令格式如下：
   ipmsg.exe [端口] /MSG [/LOG][/SEAL] <主机名或IP地址> <消息>
   例如：
   C:\>ipmsg.exe /MSG /SEAL localhost Hello.

 - 操作技巧.

   1. 隐藏/显示 窗口  Ctrl + D

   2. 按住 Ctrl 键再点 [刷新] 可保持现有用户, 搜索新上线的用户

   3. 打开发送/接收消息窗口  Ctrl + Alt + S / R (需要进行详细设置)

   4. 打开搜索窗口  Ctrl + F

   5. 接收到多个文件, 保存时可勾选 [全部]

--------------------------------------------------------------------------------
8. 支持

 - IPMsg 的技术讨论区是开放的.
   如果你想订阅相关邮件, 请联系 ipmsg-subscribe@ring.gr.jp

 - 欢迎报告 bug, 以及提出建议

 - 如果你有任何疑问, 请 E-mail 联系.
   shirouzu@h.email.ne.jp
   飞鸽传书 (IP Messenger) 2007 绿色版 可联系飞鸽: 
   http://www.fige.com.cn/

 - 发送错误报告, 请勿必记录以下信息:
   软件版本, 操作系统, 故障描述, 以及故障重现方法等.

--------------------------------------------------------------------------------
9. 更新历史

  ver 1.00 ... 日文版 (1996/08/19)

  ver 1.31 ... 英文版/日文版 (1997/09/01)

  ver 2.00 ... 英文版/日文版 (2002/11/19)
               支持文件/文件夹传送
	       支持通信数据加密

  ver 2.03 ... Bug 修正 (文件传送引起缓冲溢出)
               广播设置支持主机地址(FQDN)解析

  ver 2.04 ... 增加绑定网卡(IP)功能

  ver 2.05 ... Bug 修正 (2.04版当激活发送/接收消息窗口时,无法注销/关闭系统)

  ver 2.06 ... 很小的调整
  
  ver 2007 ... 全面调整软件的界面可用性、性能稳定性

--------------------------------------------------------------------------------
10. 感谢

 - IPMsg 技术讨论区的所有成员

 - Mr.Kanazawa (英文信息修正)

 - 所有报告软件 bug 以及提出建议的朋友.

--------------------------------------------------------------------------------
 - 官方站点:   http://www.ipmsg.org/

 - 中文版站点: http://www.fige.com.cn/

 - 飞鸽传书(IP Messenger) 2007 绿色版 由 phay 制作  2006-11-18  欢迎批评指正
--------------------------------------------------------------------------------