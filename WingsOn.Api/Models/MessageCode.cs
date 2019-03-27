namespace WingsOn.Api.Models
{
    /// <summary>
    /// Arbitrary code messages specific for the api implementation, these should be defined by the IT team.
    /// </summary>
    public enum MessageCode
    {
        /// <summary>
        /// Successful processing.
        /// </summary>
        SUCCESS = 1,
        /// <summary>
        /// Generic error.
        /// </summary>
        ERROR_UNKNOWN = 500,
        /// <summary>
        /// When an entry is not found in the given repository.
        /// </summary>
        ERROR_VALUE_NOT_FOUND = 510,
        /// <summary>
        /// When the request is malformed.
        /// </summary>
        ERROR_BAD_REQUEST = 520,
        /// <summary>
        /// Sent when a entry is already found in the repository.
        /// </summary>
        ERROR_DUPLICATE_ENTRY = 530,
        /// <summary>
        /// Page not found error.
        /// </summary>
        ERROR_404 = 600
    }
}