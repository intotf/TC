﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TC.Validate
{
    /// <summary>
    /// 表示验证是否和正则表达式匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MatchAttribute : ValidRuleBase
    {
        /// <summary>
        /// 获取或设置正则表达式
        /// </summary>
        public string RegexPattern { get; set; }

        /// <summary>
        /// 验证是否和正则表达式匹配
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        public MatchAttribute(string pattern)
        {
            this.RegexPattern = pattern;
            this.OrderIndex = 1;
            this.ErrorMessage = "请输入正确的值";
        }

        /// <summary>
        /// 转换为对应的ValidBox类型
        /// </summary>
        /// <returns></returns>
        public override ValidBox ToValidBox()
        {
            return ValidBox.New(this.ValidType, this.ErrorMessage, this.RegexPattern);
        }


        /// <summary>
        /// 后台验证
        /// </summary>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        protected override bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }
            return Regex.IsMatch(value, this.RegexPattern);
        }
    }
}
