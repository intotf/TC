﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Model
{
    /// <summary>
    /// 定义实体视图的接口
    /// </summary>
    public interface IEntityView
    {
        /// <summary>
        /// 获取或设置唯一标识
        /// </summary>
        Guid Id { get; set; }
    }
}
