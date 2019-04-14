namespace TC.Web.Utility.Menu
{
    public class SystemMenu
    {
        /// <summary>
        /// 获取菜单容器实例
        /// </summary>
        private readonly MenuBuilder<Actions> builder = new MenuBuilder<Actions>().System("5K 门禁管理平台")
            .Module("系统管理", "fa-gear")
                .Page("系统配置", "sysconfig", "index", "fa-th")

                .Page("升级管理", "upgrade", "index", "fa-th")
                    .Action("upgrade", "create", Actions.Create)
                    .Action("upgrade", "edit", Actions.Edit)
                    .Action("upgrade", "remove", Actions.Delete)

                .Page("数据备份", "dbbackup", "index", "fa-th")
                    .Action("dbbackup", "create", Actions.Create)
                    .Action("dbbackup", "remove", Actions.Delete)

            .Module("基础数据", "fa-home")
                .Page("房间结构", "roomstruct", "index", "fa-th")
                    .Action("roomstruct", "create", Actions.Create)
                    .Action("roomstruct", "createbat", Actions.CreateBat)
                    .Action("roomstruct", "edit", Actions.Edit)
                    .Action("roomstruct", "remove", Actions.Delete)

                .Page("组织机构", "organize", "index", "fa-cogs")
                    .Action("organize", "create", Actions.Create)
                    .Action("organize", "edit", Actions.Edit)
                    .Action("organize", "remove", Actions.Delete)
                    .Action("organize", "permission", Actions.OrgPermission)

                .Page("系统账号", "userinfo", "index", "fa-th")
                    .Action("userinfo", "create", Actions.Create)
                    .Action("userinfo", "edit", Actions.Edit)
                    .Action("userinfo", "remove", Actions.Delete)

            .Module("设备管理", "fa-tablet")
                .Page("门口机/门禁", "EntranceMachine", "index", "fa-th")
                    .Action("EntranceMachine", "create", Actions.Create)
                    .Action("EntranceMachine", "edit", Actions.Edit)
                    .Action("EntranceMachine", "delete", Actions.Delete)
                    .Action("EntranceMachine", "reboot", Actions.Reboot)
                    .Action("HouseEntranceMachine", "index")

                .Page("户门口机", "roomentrancemachine", "index", "fa-th")
                    .Action("roomentrancemachine", "create", Actions.Create)
                    .Action("roomentrancemachine", "remove", Actions.Delete)

                .Page("保安机", "callingmachine", "index", "fa-th")
                    .Action("callingmachine", "create", Actions.Create)
                    .Action("callingmachine", "edit", Actions.Edit)
                    .Action("callingmachine", "remove", Actions.Delete)

                .Page("室内机", "indoormachine", "index", "fa-th")
                    .Action("indoormachine", "create", Actions.Create)
                    .Action("indoormachine", "remove", Actions.Delete)
                    .Action("indoormachine", "tomaster", Actions.ToMaster)


            .Module("用户管理", "fa-user")
                .Page("用户列表", "house", "index", "fa-th")
                    .Action("house", "create", Actions.Create)
                    .Action("house", "edit", Actions.Edit)
                    .Action("house", "remove", Actions.Delete)
                    .Action("house", "init", Actions.InitHouse)
                    .Action("house", "cloudenable", Actions.CloudEnable)

                    .Action("houseroom", "bindhouse", Actions.BindRoom)
                    .Action("card", "bindhouse", Actions.BindCard)
                    .Action("houseentrancemachine", "bindhouse", Actions.BindMachine)
                    .Action("fingerprint", "bindhouse", Actions.BindFingerprint)
                    .Action("face", "bindhouse", Actions.BindFace)

                .Page("门卡列表", "card", "index", "fa-th")
                    .Action("card", "remove", Actions.Delete)

            .Module("公告管理", "fa-bullhorn")
                .Page("公告管理", "announcement", "index", "fa-th")
                    .Action("announcement", "create", Actions.Create)
                    .Action("announcement", "edit", Actions.Edit)
                    .Action("announcement", "remove", Actions.Delete)
                    .Action("announcement", "release", Actions.Publish)
                    .Action("announcement", "releaseindoor")

                .Page("发布记录", "announcementpublish", "index", "fa-th")
                    .Action("announcementpublish", "remove", Actions.Delete)
                    .Action("announcementindoorpublish", "index")
                    .Action("announcementindoorpublish", "remove")

             .Module("日志管理", "fa-reorder")
                .Page("报警日志", "alarmlog", "index", "fa-th")
                .Page("开锁日志", "unlocklog", "index", "fa-th")
                .Page("对讲日志", "intercomlog", "index", "fa-th")
                .Page("离线日志", "linelog", "index", "fa-th")
                .Page("安防报警", "imalarmlog", "index", "fa-th")
            ;


        /// <summary>
        /// 获取或设置当前菜单顶节点
        /// </summary>
        public static MenuNode<Actions> TopNode
        {
            get
            {
                return HttpContext.Current.Items["TopNode"] as MenuNode<Actions>;
            }
            set
            {
                HttpContext.Current.Items["TopNode"] = value;
            }
        }

        /// <summary>
        /// 获取或设置当前菜单活动节点
        /// </summary>
        public static MenuNode<Actions> ActiveNode
        {
            get
            {
                return HttpContext.Current.Items["ActiveNode"] as MenuNode<Actions>;
            }
            set
            {
                HttpContext.Current.Items["ActiveNode"] = value;
            }
        }

        /// <summary>
        /// 生成系统菜单，返回顶节点
        /// </summary>
        /// <returns></returns>
        public static MenuNode<Actions> Create()
        {
            return new SystemMenu().builder.GetTopNode();
        }
    }
}