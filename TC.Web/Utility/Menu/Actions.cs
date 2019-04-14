using System;
using System.ComponentModel.DataAnnotations;

namespace TC.Web.Utility.Menu
{
    [Flags]
    public enum Actions
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Display(Name = "新增")]
        Create = 0x1,

        /// <summary>
        /// 编辑
        /// </summary>
        [Display(Name = "编辑")]
        Edit = 0x2,

        /// <summary>
        /// 删除
        /// </summary>
        [Display(Name = "删除")]
        Delete = 0x4,

        /// <summary>
        /// 详情
        /// </summary>
        [Display(Name = "详情")]
        Details = 0x8,

        /// <summary>
        /// 组织菜单权限
        /// </summary>
        [Display(Name = "菜单权限")]
        OrgPermission = 0x10,

        /// <summary>
        /// 禁用
        /// </summary>
        [Display(Name = "禁用")]
        Disable = 0x20,

        /// <summary>
        /// 批量创建
        /// </summary>
        [Display(Name = "批量创建")]
        CreateBat = 0x40,

        /// <summary>
        /// 公告发布
        /// </summary>
        [Display(Name = "公告发布")]
        Publish = 0x80,

        /// <summary>
        /// 绑定门口机
        /// </summary>
        [Display(Name = "门口机户主")]
        BindMachine = 0x100,

        /// <summary>
        /// 绑定门卡
        /// </summary>
        [Display(Name = "绑定门卡")]
        BindCard = 0x200,

        /// <summary>
        /// 绑定指纹
        /// </summary>
        [Display(Name = "绑定指纹")]
        BindFingerprint = 0x400,

        /// <summary>
        /// 绑定房间
        /// </summary>
        [Display(Name = "绑定房间")]
        BindRoom = 0x800,

        /// <summary>
        /// 设为主机
        /// </summary>
        [Display(Name = "设为主机")]
        ToMaster = 0x1000,

        /// <summary>
        /// 重启
        /// </summary>
        [Display(Name = "重启")]
        Reboot = 0x2000,

        /// <summary>
        /// 批量初始化住户和卡
        /// </summary>
        [Display(Name = "批量初始化")]
        InitHouse = 0x4000,

        /// <summary>
        /// 绑定指人脸
        /// </summary>
        [Display(Name = "绑定人脸")]
        BindFace = 0x8000,

        /// <summary>
        /// 开通云对讲
        /// </summary>
        [Display(Name = "开通云对讲")]
        CloudEnable = 0x10000,
    }
}