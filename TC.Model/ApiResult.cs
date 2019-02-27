using Infrastructure.Page;
using System;
using System.Collections.Generic;
using System.Text;
using TC.Validate;

namespace TC.Model
{
    /// <summary>
    /// Api结果接口
    /// </summary>
    public interface IApiResult
    {
        /// <summary>
        /// 错误码
        /// </summary>      
        ErrorCode Code { get; set; }

        /// <summary>
        /// 相关提示信息
        /// </summary>
        string Msg { get; set; }
    }


    /// <summary>
    /// 表示Api结果
    /// </summary>
    public class ApiResult<T> : IApiResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [Required]
        public ErrorCode Code { get; set; }

        /// <summary>
        /// 相关提示信息
        /// </summary>
        [Required]
        public string Msg { get; set; }

        /// <summary>
        /// 业务数据
        /// </summary>
        [Required]
        public T Data { get; set; }
    }


    /// <summary>
    /// 表示ApiResultOf(T)的构造对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultBuilder<T>
    {
        /// <summary>
        /// 表示成功的结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public ApiResult<T> NoError(T data)
        {
            return new ApiResult<T> { Code = ErrorCode.NoError, Data = data };
        }

        /// <summary>
        /// 表示参数错误的结果
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <returns></returns>
        public ApiResult<T> ParameterError(string msg)
        {
            return this.Error(ErrorCode.ParameterError, msg);
        }

        /// <summary>
        /// 表示服务器内部异常
        /// </summary>
        /// <returns></returns>
        public ApiResult<T> ServiceError()
        {
            return this.Error(ErrorCode.ServiceError, ErrorCode.ServiceError.GetFieldDisplay());
        }

        /// <summary>
        /// 表示超时异常的结果
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <returns></returns>
        public ApiResult<T> TimeoutError(string msg)
        {
            return this.Error(ErrorCode.TimoutError, msg);
        }

        /// <summary>
        /// 因某种原因而受到限制请求
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <returns></returns>
        public ApiResult<T> LimitedRequest(string msg)
        {
            return this.Error(ErrorCode.LimitedRequest, msg);
        }

        /// <summary>
        /// 从错误码包装得到
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">提示消息</param>
        /// <returns></returns>
        private ApiResult<T> Error(ErrorCode code, string msg)
        {
            return new ApiResult<T> { Code = code, Msg = msg };
        }
    }


    /// <summary>
    /// 表示Api结果
    /// </summary>
    public static class ApiResult
    {
        /// <summary>
        /// 生成ApiResultOf(T)的构造对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns></returns>
        public static ApiResultBuilder<T> Build<T>()
        {
            return new ApiResultBuilder<T>();
        }

        /// <summary>
        /// 表示成功的结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static ApiResult<T> NoError<T>(T data)
        {
            return new ApiResult<T> { Code = ErrorCode.NoError, Data = data };
        }

        /// <summary>
        /// 从code得到
        /// </summary>
        /// <param name="apiResultType">返回数据类型</param>
        /// <param name="code">错误码</param>    
        /// <returns></returns>
        public static IApiResult FromCode(Type apiResultType, ErrorCode code)
        {
            var msg = code.GetFieldDisplay();
            return ApiResult.FromCode(apiResultType, code, msg);
        }

        /// <summary>
        /// 从code得到
        /// </summary>
        /// <param name="apiResultType">返回数据类型</param>
        /// <param name="code">错误码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static IApiResult FromCode(Type apiResultType, ErrorCode code, string msg)
        {
            var apiResult = Activator.CreateInstance(apiResultType) as IApiResult;
            if (apiResult != null)
            {
                apiResult.Code = code;
                apiResult.Msg = msg;
            }
            return apiResult;
        }

        /// <summary>
        /// 从page转换得到
        /// </summary>
        /// <typeparam name="TPageEntity"></typeparam>
        /// <param name="page">分页数据</param>
        /// <returns></returns>
        public static ApiResult<PageInfo<TPageEntity>> FromPage<TPageEntity>(IPageInfo<TPageEntity> page) where TPageEntity : class
        {
            var data = new PageInfo<TPageEntity>(page.TotalCount, page);
            return new ApiResult<PageInfo<TPageEntity>> { Code = ErrorCode.NoError, Data = data };
        }
    }
}
