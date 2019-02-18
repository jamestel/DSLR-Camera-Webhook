using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;

namespace DSLR_Camera_Webhook
{
    public delegate void EventHandler();

    class WebServer
    {
        private HttpServer httpsv;
        private int port;

        public static event EventHandler captureRequest;

        public WebServer(int port)
        {
            this.port = port;

            httpsv = new HttpServer(port);

            httpsv.OnGet += pageRequest;
        }

        public string Start()
        {
            httpsv.Start();

            return "http://" + System.Net.Dns.GetHostName() + ":" + port;
        }

        public void Stop()
        {
            httpsv.Stop();
        }

        private static void pageRequest(object sender, HttpRequestEventArgs e)
        {
            HttpListenerResponse response = e.Response;
            HttpListenerRequest request = e.Request;

            if (request.RawUrl != "/")
            {
                response.StatusCode = (int)HttpStatusCode.NotImplemented;
                return;
            }

            captureRequest.Invoke();

            response.ContentType = "text/html";
            response.ContentEncoding = Encoding.UTF8;

            response.WriteContent(Encoding.UTF8.GetBytes("Capture attempted..."));
        }
    }
}
