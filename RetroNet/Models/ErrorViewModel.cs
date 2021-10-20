using System;

namespace _90sTest.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorDisplayMessage = "We are looking into the issue.";

    }
}


