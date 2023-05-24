//using Sharekhan.Sharekhan.Sharekhan;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sharekhan.Example.SharekhanConsoleApp.SharekhanConsoleApp
//{
//    internal class SocketTest
//    {
//        static void Main(string[] args)
//        {

//            string accesstoken = "enter-your-accesstoken";
//            string apiKey = "enter-your-apikey";



//            WEBSocket _WS = new WEBSocket();
//            var exitEvent = new ManualResetEvent(false);

//            _WS.ConnectforOrderQuote(accesstoken,apiKey);
//            if (_WS.IsConnected())
//            {
//                string script = "{\"action\":\"subscribe\",\"key\":[\"feed\"],\"value\":[\"\"]}";
//                _WS.RunScript(script);

//                string feedData = "{\"action\":\"feed\",\"key\":[\"ltp\"],\"value\":[\"MX255320\"]}";
//                _WS.RunScript(feedData);

//                string unsubscribe = "{\"action\":\"unsubscribe\",\"key\":[\"ltp\"],\"value\":[\"RN1064\"]}";
//                _WS.RunScript(unsubscribe);

//                _WS.MessageReceived += WriteResult;


//                Console.WriteLine("Press any key to stop and close the socket connection.");
//                Console.ReadKey();

//                _WS.Close(true);// to stop and close socket connection
//            }
//            //exitEvent.WaitOne();
//        }
//        static void WriteResult(object sender, MessageEventArgs e)
//        {
//            Console.WriteLine("Tick Received : " + e.Message);

//        }


//    }
//}
