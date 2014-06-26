using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Business.Core
{
    /// <summary>
    /// 用户自定义
    /// </summary>
    public class UserDefined
    {
        public virtual long Id { get; set; }

        public virtual string Description { get; set; }

        public virtual long CreatorId { get; set; }

        public virtual DateTime CreateTime { get; set; }

        public virtual long ManagerId { get; set; }
    }

    public class UserDefinedFactory
    {
        public static UserDefined Create(string jsonData)
        {
            return JsonConvert.DeserializeObject<UserDefined>(jsonData);
        }

        public static UserDefined Create(string description, long createId, long mangerId)
        {
            return new UserDefined()
            {
                Description = description,
                CreatorId = createId,
                CreateTime = DateTime.Now,
                ManagerId = mangerId
            };
        }
    }
}
