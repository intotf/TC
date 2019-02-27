using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TC.Web
{
    /// <summary>
    /// 系统基础配置信息
    /// </summary>
    public class WebConfig
    {
        /// <summary>
        /// 系统名称 
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 系统描述 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 系统关键字 
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 版权
        /// </summary>
        public string CopyRight { get; set; }

        /// <summary>
        /// 备案号
        /// </summary
        public string KeeponRecord { get; set; }
    }
}
