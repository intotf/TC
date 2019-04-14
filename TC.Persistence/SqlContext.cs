using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using TC.Model;
using TC.Model.Entitys;

namespace TC.Persistence
{
    /// <summary>
    /// Sql数据库
    /// </summary>
    public class SqlContext : DbContext
    {
        /// <summary>
        /// 管理员
        /// </summary>
        public DbSet<UserInfo> UserInfo { get; set; }

        /// <summary>
        /// 管理员视图
        /// </summary>
        public DbSet<UserInfoView> UserInfoView { get; set; }

        /// <summary>
        /// 构造SqlContext 上下文
        /// </summary>
        /// <param name="options"></param>
        public SqlContext(DbContextOptions<SqlContext> options)
         : base(options)
        {
        }

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <param name="model">模型</param>
        ///// <param name="id">id</param>
        ///// <param name="newModel">更新后的模型</param>
        ///// <returns></returns>
        //public override T Update<T>(T model) where T : class, IStringId
        //{
        //    try
        //    {
        //        var entry = this.Entry(model);
        //        if (entry.State == EntityState.Detached)
        //        {
        //            model = this.Set<T>().Attach(model);
        //            entry.State = EntityState.Modified;
        //        }
        //        return model;
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        var local = this.Set<T>().Find(model.Id);
        //        var entry = this.Entry(local);
        //        entry.CurrentValues.SetValues(model);
        //        return local;
        //    }
        //}

        ///// <summary>
        ///// 获取服务
        ///// </summary>
        ///// <typeparam name="TServer">服务类型</typeparam>
        ///// <returns></returns>
        //public TServer Server<TServer>() where TServer : ISqlServer, new()
        //{
        //    var server = Activator.CreateInstance<TServer>();
        //    server.SetContext(this);
        //    return server;
        //}

        /// <summary>
        /// 创建模型时
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
