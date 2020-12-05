
namespace OnlineStoreManager.Infracstructure
{
    public class Result
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public string[] Messages { get; set; }

        public object Data { get; set; }

        public Result(bool success, string message = null, object data = null)
        {
            this.IsSuccess = success;
            this.Message = message;
            this.Data = data;
        }

        public Result(bool success, string[] message)
        {
            this.IsSuccess = success;
            this.Messages = Messages;
        }

        public static Result Fail(string message = null, object data = null)
        {
            return new Result(false, message, data);
        }

        public static Result Fail(string[] messages)
        {
            return new Result(false, messages);
        }

        public static Result Success()
        {
            return new Result(true);
        }
    }
}
