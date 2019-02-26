
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TC.Validate
{
    /// <summary>
    /// 表示验证模型枚举值是否定义特性
    /// </summary>
    public class EnumDefinedAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            return Enum.IsDefined(value.GetType(), value);
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return "值不在定义范围内";
        }
    }
}
