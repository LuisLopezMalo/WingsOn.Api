namespace WingsOn.Api.Models.Responses
{
    /// <summary>
    /// The base class that returns information about a request execution.
    /// </summary>
    public class ResponseObject
    {
        /// <summary>
        /// The code that represent the status of the execution for a request.
        /// This will be returned to the front-end.
        /// </summary>
        public MessageCode Code { get; set; } = MessageCode.SUCCESS;

        /// <summary>
        /// The information message that will be return to the front-end.
        /// These messages should be retrieved from a resources file, using Cultures
        /// so different languages can be managed.
        /// </summary>
        public string Message { get; set; } = "Ok";

        /// <summary>
        /// The data that was retrieved from a request.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Receive the status code.
        /// </summary>
        /// <param name="code">Status Code from <see cref="MessageCode"/> enum.</param>
        public ResponseObject(MessageCode code)
            : this(code, string.Empty)
        {
        }

        /// <summary>
        /// Receive the status code and the message.
        /// </summary>
        /// <param name="code">Status Code from <see cref="MessageCode"/> enum.</param>
        /// <param name="message">The message to return.</param>
        public ResponseObject(MessageCode code, string message)
            : this(code, string.Empty, null)
        {
        }

        /// <summary>
        /// Receive the status code, the message and the data.
        /// </summary>
        /// <param name="code">Status Code from <see cref="MessageCode"/> enum.</param>
        /// <param name="message">The message to return.</param>
        /// <param name="data">The data to be returned.</param>
        public ResponseObject(MessageCode code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Receive the data.
        /// </summary>
        /// <param name="data">The data to be returned.</param>
        public ResponseObject(object data)
        {
            Data = data;
        }

        /// <summary>
        /// Send an ok response with the data.
        /// </summary>
        /// <param name="data">The data to be returned.</param>
        public static ResponseObject Ok(object data = null)
        {
            return new ResponseObject(data);
        }

        /// <summary>
        /// Send a response with the custom status, message and data.
        /// </summary>
        /// <param name="code">Status Code from <see cref="MessageCode"/> enum.</param>
        /// <param name="message">The message to return.</param>
        /// <param name="data">The data to be returned.</param>
        public static ResponseObject StatusCode(MessageCode code, string message, object data = null)
        {
            return new ResponseObject(code, message, data);
        }
    }
}
