using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DSLR_Camera_Webhook
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer webServ;
            CameraControl camControl;

            camControl = new CameraControl();

            webServ = new WebServer(8081);
            Console.WriteLine(webServ.Start());

            Console.WriteLine("Selected Camera: " + camControl.CurrentCamera());

            WebServer.captureRequest += () =>
            {
                try
                {
                    camControl.CapturePhoto();
                    Console.WriteLine(camControl.CurrentCamera() + " captured a photograph.");

                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(camControl.CurrentCamera() + ": " + e.GetType().Name);
                }
            };

            System.Console.ReadLine();
        }
    }
}
