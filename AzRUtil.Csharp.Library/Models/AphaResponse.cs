using System.Collections.Generic;

namespace AzRUtil.Csharp.Library.Models
{

    public class AlphaResponseBody
    {
        public string status { get; set; }
        public string error { get; set; }
        public string smslog_id { get; set; }
        public string queue { get; set; }
        public string to { get; set; }
    }

    public class AlphaResponse
    {
        public List<AlphaResponseBody> data { get; set; }
        public string error_string { get; set; }
        public string otp { get; set; }

    }

}
