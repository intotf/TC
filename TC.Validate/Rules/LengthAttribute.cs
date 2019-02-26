﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MVC = System.ComponentModel.DataAnnotations;

namespace TC.Validate
{
    /// <summary>
    /// 表示验证输入的长度范围
    /// maxLength参数会影响EF-CodeFirst生成的数据库字段最大长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LengthAttribute : MVC.StringLengthAttribute, IValidRule
    {
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 验证输入的长度范围
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public LengthAttribute(int maxLength)
            : base(maxLength)
        {
            this.OrderIndex = 1;
            this.ErrorMessage = "长度必须介于1到{1}个字";
        }

        /// <summary>
        /// 验证输入的长度范围
        /// </summary>       
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        public LengthAttribute(int minLength, int maxLength)
            : this(maxLength)
        {
            this.MinimumLength = minLength;
            this.ErrorMessage = "长度必须介于{0}到{1}个字";
        }

        /// <summary>
        /// 生成验证框对象
        /// </summary>
        /// <returns></returns>
        public ValidBox ToValidBox()
        {
            return ValidBox.New("length", this.ErrorMessage, this.MinimumLength, this.MaximumLength);
        }

        /// <summary>
        /// 获取错误提示信息
        /// </summary>       
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(this.ErrorMessage, this.MinimumLength, this.MaximumLength);
        }
    }
}
