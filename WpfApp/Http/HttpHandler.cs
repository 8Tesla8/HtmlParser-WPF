using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Http
{
    public class HttpHandler
    {
        public HttpResponse GetResponse(string url)
        {
            HttpResponse response = new HttpResponse();
            response.Url = url;

            Stopwatch timer = new Stopwatch();

            using (var client = new HttpClient())
            {
                try
                {
                    timer.Start();

                    var resp = client.GetAsync(url).Result;

                    timer.Stop();

                    response.Context = resp.Content.ReadAsStringAsync().Result;
                    response.StatusCode = resp.StatusCode;
                    response.ResponseTime = timer.Elapsed;

                }
                catch (Exception ex)
                {
                    timer.Stop();
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            return response;
        }
    }
}

