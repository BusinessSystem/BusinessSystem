namespace Business.Utils.Info
{
    
    public class InfoBase
    {

        private const string defaultCode = "9999";
        private const string defaultLevel = "9999";
        private const string defaultMessage = "This info message not found!";
       
        public string ProjectName
        {
            get;
            set;
        }
        
        public string Code
        {
            get;
            set;
        }
         
        public string Message
        {
            get;
            set;
        }
        
        public string Level
        {
            get;
            set;
        }
       
        public string Description
        {
            get;
            set;
        }

        public static InfoBase CreateDefault()
        {
            return new InfoBase()
            {
                Description = string.Empty,
                Code = defaultCode,
                Level = defaultLevel,
                Message = defaultMessage
            };
        }
      
    }
}
