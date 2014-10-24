using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Web.Models
{
    public class ServerStatus : IComparable<ServerStatus>, IEquatable<ServerStatus>
    {
        public static readonly ServerStatus Unspecified = new ServerStatus() { Code = -1, Description = "Unspecified Status" };

        public static readonly ServerStatus Success = new ServerStatus() { Code = 200, Description = "OK" };

        public static readonly ServerStatus InModelRequired = new ServerStatus() { Code = 300, Description = "Input Empty" };

        public static readonly ServerStatus ItemNotExist = new ServerStatus() { Code = 301, Description = "Item does not exist" };

        /// <summary>
        /// 401
        /// </summary>
        public static readonly ServerStatus Unauthorized = new ServerStatus() { Code = 401, Description = "please login agin" };

        public static readonly ServerStatus InModelValidationError = new ServerStatus() { Code = 302, Description = "Validation Error" };

        public static readonly ServerStatus ExceptionUnhandled = new ServerStatus() { Code = 400, Description = "Unclassified Error" };

        public static readonly ServerStatus UserNotification = new ServerStatus() { Code = 600, Description = "User Notification" };

        public static readonly ServerStatus AccessDenied = new ServerStatus() { Code = 700, Description = "Access Denied" };

        public static readonly ServerStatus SearchSuccess = new ServerStatus() { Code = 200, Description = "查询成功！" };

        public static readonly ServerStatus SearchFailed = new ServerStatus() { Code = 701, Description = "查询失败！" };

        public static readonly ServerStatus SaveSuccess = new ServerStatus() { Code = 200, Description = "保存成功！" };

        public static readonly ServerStatus SaveFailed = new ServerStatus() { Code = 701, Description = "保存失败！" };

        public static readonly ServerStatus DeleteSuccess = new ServerStatus() { Code = 200, Description = "删除成功！" };

        public static readonly ServerStatus DeleteFailed = new ServerStatus() { Code = 701, Description = "删除失败！" };

        public static readonly ServerStatus UpdateSuccess = new ServerStatus() { Code = 200, Description = "更新成功！" };

        public static readonly ServerStatus UpdateFailed = new ServerStatus() { Code = 701, Description = "更新失败！" };

        public static readonly ServerStatus ApproveSuccess = new ServerStatus() { Code = 200, Description = "审核成功！" };

        public static readonly ServerStatus ApproveFailed = new ServerStatus() { Code = 701, Description = "审核失败！" };

        public static readonly ServerStatus ExportSuccess = new ServerStatus() { Code = 200, Description = "上传成功！" };

        public static readonly ServerStatus ExportFailed = new ServerStatus() { Code = 701, Description = "上传失败！" };

        public int Code { get; set; }

        public string Description { get; set; }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        public int CompareTo(ServerStatus other)
        {
            return this.Code - other.Code;
        }

        public bool Equals(ServerStatus other)
        {
            return this.Code == other.Code;
        }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is ServerStatus)
            {
                result = this.Code == ((ServerStatus)obj).Code;
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format("ServerStatus {{ Code: {0}, Description: \"{1}\" }}", this.Code, this.Description);
        }
    }
}