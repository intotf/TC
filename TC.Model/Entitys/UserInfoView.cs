using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TC.Model.Entitys
{
    /// <summary>
    /// 系统后台用户信息
    /// </summary>
    [Table("V_UserInfo")]
    public class UserInfoView : IEntityView
    {
        /// <summary>
        /// 视图id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        [Column("U_ID")]
        public string U_ID { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>      
        [Column("U_Account")]
        public string Account { get; set; }

        /// <summary>
        /// 登录秘密
        /// </summary>
        [Column("U_Password")]
        public string Password { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        [Column("U_Enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("U_Name")]
        public string Name { get; set; }

        /// <summary>
        /// 组织架构ID
        /// </summary>
        [Column("U_OR_ID")]
        public string OR_ID { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("U_EMail")]
        public string EMail { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Column("U_Mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// Creater
        /// </summary>
        [Column("U_Creater")]
        public string Creater { get; set; }


        /// <summary>
        /// 组织结构名称
        /// </summary>
        [Column("OR_Name")]
        public string OR_Name { get; set; }

    }
}
