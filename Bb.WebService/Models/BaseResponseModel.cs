using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bb.WebService.Models
{
    public class BaseResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("response_code")]
        public ResponseCode ResponseCode { get; set; }
    }

    public enum ResponseCode
    {
        SUCCESS = 1,
        GENERAL_ERROR = 0,
        DUPLICATED_ID = 1001
    }
}