using System;

namespace AspNetCore.Models
{
    public class ErrorViewModel: BaseModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorViewModel(string title) : base(title)
        {
        }
    }
}