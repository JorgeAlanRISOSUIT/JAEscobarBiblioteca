using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }
        public List<List<object>> Paginations { get; set; }
    }
}
