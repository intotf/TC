using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TC.Model
{
    /// <summary>
    /// 错误码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 无错误      
        /// </summary>
        NoError = 0,

        /// <summary>
        /// 未登录操作
        /// </summary>
        [Display(Name = "未登录操作")]
        NoLogined = 2,


        /// <summary>
        /// 请求的参数不正确
        /// </summary>
        [Display(Name = "请求的参数不正确")]
        ParameterError = 10,

        /// <summary>
        /// 等待已经超过预设的时间
        /// </summary>
        [Display(Name = "等待已经超过预设的时间")]
        TimoutError = 11,

        /// <summary>
        /// 服务器内部处理异常    
        /// </summary>
        [Display(Name = "服务器内部处理异常")]
        ServiceError = 12,

        /// <summary>
        /// 因某种原因而受到限制请求
        /// </summary>
        [Display(Name = "因某种原因而受到限制请求")]
        LimitedRequest = 13,
    }
}
