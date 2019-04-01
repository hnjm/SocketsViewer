﻿/****************************************************************************
*项目名称：SocketsViewer.Model
*CLR 版本：4.0.30319.42000
*机器名称：WENLI-PC
*命名空间：SocketsViewer.Model
*类 名 称：TransferInfo
*版 本 号：V1.0.0.0
*创建人： yswenli
*电子邮箱：wenguoli_520@qq.com
*创建时间：2019/4/1 13:41:31
*描述：
*=====================================================================
*修改时间：2019/4/1 13:41:31
*修 改 人： yswenli
*版 本 号： V1.0.0.0
*描    述：
*****************************************************************************/

namespace SocketsViewer.Model
{
    public class TransferInfo
    {
        public string Protocol
        {
            get; set;
        }

        public string SourceIP
        {
            get; set;
        }

        public int SourcePort
        {
            get; set;
        }

        public string TargetIP
        {
            get; set;
        }

        public int TargetPort
        {
            get; set;
        }

        public long Length
        {
            get; set;
        }

        public byte[] Data
        {
            get; set;
        }
    }
}
