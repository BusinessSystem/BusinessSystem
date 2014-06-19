using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Business.Core
{
    /// <summary>
    /// 意向表
    /// </summary>
    public class Intention
    {
        public virtual long Id { get; set; }

        public virtual string Description { get; set; }

        public virtual long CreatorId { get; set; }

        public virtual DateTime  CreateTime { get; set; }
    }

    public class IntentionFactory
    {
        public static Intention Create(string description, long createorId)
        {
            return new Intention()
            {
                Description = description,
                CreatorId = createorId,
                CreateTime = DateTime.Now
            };
        }

        public static Intention Create(string jsonData)
        {
            return JsonConvert.DeserializeObject<Intention>(jsonData);
        }
    }
}
