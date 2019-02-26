using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TC.Validate;

namespace TC.Model.Entitys
{
    /// <summary>
    /// 系统后台用户信息
    /// </summary>
    [Table("T_SYS_UserInfo")]
    [Serializable]
    public class UserInfo : IEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Length(50)]
        [Column("U_ID")]
        public string Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Length(15)]
        [Required(ErrorMessage = "登录账号不能为空")]
        [Column("U_Account")]
        //[Remote("/UserInfo/CheckAccount", "Id", "Account")]
        public string Account { get; set; }

        /// <summary>
        /// 登录秘密
        /// </summary>
        [Length(50)]
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
        [Length(20)]
        [Column("U_Name")]
        public string Name { get; set; }

        /// <summary>
        /// 组织架构ID
        /// </summary>
        [Length(50)]
        [Column("U_OR_ID")]
        public string OR_ID { get; set; }

        /// <summary>
        /// 表示组织架构的parentID
        /// NotMapped
        /// </summary>
        [NotMapped]
        public string OR_ParentID { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Length(50)]
        [Email]
        [Column("U_EMail")]
        public string EMail { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Length(15)]
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
        [Length(50)]
        [Column("U_Creater")]
        public string Creater { get; set; }
    }
}