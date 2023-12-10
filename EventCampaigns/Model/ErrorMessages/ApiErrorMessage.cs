namespace EventCampaigns.Model.ErrorMessages
{

    [Serializable]
    public class ApiErrorMessage
    {
        public IEnumerable<ApiErrorMessageItem> Errors { get; set; } = new ApiErrorMessageItem[0];


        public ApiErrorMessage(IEnumerable<ApiErrorMessageItem> errors)
        {
            Errors = errors;
        }

        public ApiErrorMessage(ApiErrorMessageItem error)
            : this(new ApiErrorMessageItem[1] { error })
        {
        }

        public ApiErrorMessage()
        {
        }
    }
}
