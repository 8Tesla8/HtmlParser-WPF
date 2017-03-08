using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Http
{
    public class HttpResponse
    {
        public string Url { get; set; }
        public TimeSpan ResponseTime { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Context { get; set; }
    }
}
