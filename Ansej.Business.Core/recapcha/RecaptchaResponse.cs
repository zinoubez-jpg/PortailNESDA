using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Ansej.Business.Core
{
    public class RecaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("score")]
        public float Score { get; set; }

        [JsonPropertyName("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
