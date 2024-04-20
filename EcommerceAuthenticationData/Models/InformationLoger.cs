using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAuthenticationData.Models
{
    public class InformationLoger
    {
        public InformationLoger(string FunctionName, Exception ex, object Data, string TypeLog = "")
        {
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            this.FunctionName = FunctionName;
            this.TypeLog = !string.IsNullOrEmpty(TypeLog) ? TypeLog : this.TypeLog;
            Message = $"Line error: {line}. Message: {ex.Message}";
            this.Data = JsonConvert.SerializeObject(Data);
        }

        public string FunctionName { get; set; }
        public string TypeLog { get; set; } = "Data storage error";
        public DateTimeOffset DateTimeOffsetLog { get; set; } = DateTimeOffset.UtcNow;
        public string Message { get; set; }
        public string Data { get; set; }

        public string GetMessage()
        {
            return $"Time of error occurrence {DateTimeOffsetLog.ToString("dd/MM/yyyy HH:mm:ss")}. {FunctionName} {TypeLog}. Message: {Message}, Data error: {Data}";
        }
    }
}
