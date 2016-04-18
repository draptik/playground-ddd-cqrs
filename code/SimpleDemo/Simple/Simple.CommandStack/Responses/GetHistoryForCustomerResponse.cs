using System.Collections.Generic;
using Simple.Common;

namespace Simple.CommandStack.Responses
{
    public class GetHistoryForCustomerResponse
    {
        public string Message { get; set; }
        public IEnumerable<HistoryItem> HistoryItems { get; set; }
    }
}