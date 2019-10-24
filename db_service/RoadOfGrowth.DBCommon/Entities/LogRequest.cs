using RoadOfGrowth.DBUtility.Providers.EntityExtension;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadOfGrowth.DBCommon.Entities
{
    public class LogRequest : BaseEntity
    {
        [Column(Name = "url")]
        public string Url { get; set; }

        [Column(Name = "methods")]
        public string Method { get; set; }

        [Column(Name = "request_body")]
        public string RequestBody { get; set; }

        [Column(Name = "request_time")]
        public DateTime RequestTime { get; set; }

        [Column(Name = "response_body")]
        public string ResponseBody { get; set; }

        [Column(Name = "response_time")]
        public DateTime? ResponseTime { get; set; }

        [Column(Name = "processing_time")]
        public int ProcessingTime { get; set; }
    }
}
