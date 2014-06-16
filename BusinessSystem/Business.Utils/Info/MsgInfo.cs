using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Business.Utils.Info
{
    public class MsgInfo : InfoBase
    {
        public object Data { get; set; }

        public MsgInfo()
            : this(InfoBase.CreateDefault(), null)
        {

        }

        public MsgInfo(object data)
            : this(InfoBase.CreateDefault(), data)
        {

        }


        public MsgInfo(InfoBase infoObj, object data)
        {
            Data = data;
            this.Code = infoObj.Code;
            this.Description = infoObj.Description;
            this.Level = infoObj.Level;
            this.Message = infoObj.Message;
        }

        public string ToJson()
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式
            timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(this, Formatting.Indented, timeConverter); ;
        }
    }

    public class MsgInfoFactory
    {
        public static MsgInfo Create(object data, string code = "", string description = "", string level = "", string message = "")
        {
            MsgInfo msgInfo = new MsgInfo(data);
            if (!string.IsNullOrEmpty(code))
                msgInfo.Code = code;
            if (!string.IsNullOrEmpty(description))
                msgInfo.Description = description;
            if (!string.IsNullOrEmpty(level))
                msgInfo.Level = level;
            if (!string.IsNullOrEmpty(message))
                msgInfo.Message = message;
            return msgInfo;
        }
    }
}
