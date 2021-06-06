using System.Text.Json.Serialization;

namespace Api.Models.Responses
{
    public class HttpError
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
        
        public HttpError(string error, string errorDescription)
        {
            Error = error;
            ErrorDescription = errorDescription;
        }
    }
}
