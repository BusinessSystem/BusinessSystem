using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Web.Models
{
    public class ResultObject<T>
    {
        public int RecordCount { get; set; }

        public T ReturnData { get; set; }

        public ServerStatus Status { get; set; }


        public ResultObject() { }

        public ResultObject(T _returnData, int _recordCount, ServerStatus _status)
        {
            ReturnData = _returnData;

            RecordCount = _recordCount;

            Status = _status;
        }

        public ResultObject(T _returnData, ServerStatus _status)
        {
            ReturnData = _returnData;

            Status = _status;
        }
    }
}