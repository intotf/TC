using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TC.Validate
{
    /// <summary>
    /// 表示验证是网络地址
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UrlAttribute : ValidRuleBase
    {
        /// <summary>
        /// 验证是网络地址
        /// </summary>
        public UrlAttribute()
        {
            this.ErrorMessage = "请输入正确的网络地址";
        }

        /// <summary>
        /// 生成验证框
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this.ValidType, this.ErrorMessage);
        }

        /// <summary>
        /// 后台验证
        /// </summary>       
        /// <param name="value">属性的值</param>
        /// <returns></returns>
        protected override bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            Uri uri;
            return Uri.TryCreate(value, UriKind.Absolute, out uri);
        }
    }
}
