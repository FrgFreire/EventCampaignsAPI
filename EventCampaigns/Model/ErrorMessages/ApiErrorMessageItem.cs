namespace EventCampaigns.Model.ErrorMessages
{
    [Serializable]
    public class ApiErrorMessageItem
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public ApiErrorMessageType Type { get; set; }

        public ApiErrorMessageItem[] InnerErrors { get; set; }

        public string Timestamp { get; internal set; }

        public ApiErrorMessageItem()
        {
            Timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        }

        public ApiErrorMessageItem(string errorCode, ApiErrorMessageType type, string message = null, string stackTrace = null, Exception innerException = null)
            : this(errorCode, message, type, stackTrace, innerException)
        {
        }

        public ApiErrorMessageItem(string errorCode, string message, ApiErrorMessageType type = ApiErrorMessageType.Business, string stackTrace = null, Exception innerException = null)
        {
            Code = errorCode;
            Message = message;
            StackTrace = stackTrace;
            Type = type;
            Timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            if (innerException != null)
            {
                InnerErrors = new ApiErrorMessageItem[1]
                {
                new ApiErrorMessageItem(innerException)
                };
            }
        }

        public ApiErrorMessageItem(Exception innerException)
            : this(null, innerException.Message, ApiErrorMessageType.Technical, innerException.StackTrace, innerException.InnerException)
        {
        }

        public override string ToString()
        {
            return "Execution returned error " + Code + " with message: " + Message;
        }
    }

    public enum ApiErrorMessageType
    {
        Business,
        Technical
    }
}
