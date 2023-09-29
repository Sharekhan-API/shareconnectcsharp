//using Newtonsoft.Json;
//using Sharekhan.Sharekhan.Sharekhan;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Sharekhan.Example.SharekhanConsoleApp
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {

//            string api_key = "enter-your-apiKey";
//            string access_token = "enter-your-accessToken";
//            string vendor_key = null;
//            SharekhanApi connect = new SharekhanApi(access_token, api_key, vendor_key);

                //string apiKey = "enter-your-apiKey";
                //string vendorKey = null;
                //long state = 12345;
                //long? versionId = null;
                //var response = connect.GetLoginUrl(apiKey, vendorKey, state, versionId);
                //Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));


                //string apiKey = "enter-your-apikey";
                //string requestToken = "enter-your-requesttoken";
                //int state = 12345;
                //string secretKey = "enter-your-secretkey";
                //string vendorKey = null;
                //int versionId = 1006;
                //var response = connect.GenerateAccessToken(apiKey, requestToken, state, secretKey, vendorKey, versionId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);


                //string apiKey = "enter-your-apiKey";
                //string requestToken = "enter-your-requestToken";
                //int state = 12345;
                //string secretKey = "enter-your-secretKey";
                //string vendorKey = null;
                //var response = connect.GenerateAccessTokenWithoutVersionId(apiKey, requestToken, state, secretKey, vendorKey);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);



                //var order = new OrderInfo
                //{
                //    customerId = 1111111,
                //    scripCode = 2475,
                //    tradingSymbol = "ONGC",
                //    exchange = "NC",
                //    transactionType = "B",
                //    quantity = 1,
                //    disclosedQty = 0,
                //    price = "150",
                //    triggerPrice = "0",
                //    rmsCode = "ANY",
                //    afterHour = "N",
                //    orderType = "NORMAL",
                //    channelUser = "1111111",
                //    validity = "GFD",
                //    requestType = "NEW",
                //    productType = "INVESTMENT",
                //    instrumentType = null,
                //    strikePrice = null,
                //    optionType = null,
                //    expiry = null
                //};

                //var response = connect.placeOrder(order);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);



                //var order = new OrderInfo
                //{
                //    orderId = "1111111",
                //    customerId = 1111111,
                //    scripCode = 2475,
                //    tradingSymbol = "ONGC",
                //    exchange = "NC",
                //    transactionType = "B",
                //    quantity = 1,
                //    disclosedQty = 0,
                //    executedQty = 0,
                //    price = "159.7",
                //    triggerPrice = "0",
                //    rmsCode = "SKNSE2",
                //    afterHour = "N",
                //    orderType = "NORMAL",
                //    channelUser = "1111111",
                //    validity = "GFD",
                //    requestType = "MODIFY",
                //    productType = "INVESTMENT",
                //    instrumentType = null,
                //    strikePrice = null,
                //    optionType = null,
                //    expiry = null
                //};

                //var response = connect.modifyOrder(order);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);


                //var order = new OrderInfo
                //{
                //    orderId = "1111111",
                //    customerId = 1111111,
                //    scripCode = 252905,
                //    tradingSymbol = "GOLDPETAL",
                //    exchange = "MX",
                //    transactionType = "B",
                //    quantity = 2,
                //    disclosedQty = 0,
                //    price = "5992",
                //    triggerPrice = "0",
                //    rmsCode = "SKMCX12",
                //    afterHour = "N",
                //    orderType = "NORMAL",
                //    channelUser = "1111111",
                //    validity = "GFD",
                //    requestType = "CANCEL",
                //    productType = "INVESTMENT",
                //    instrumentType = "FS",
                //    strikePrice = "-1",
                //    optionType = "XX",
                //    expiry = "31/05/2023"
                //};

                //var response = connect.cancelOrder(order);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //string exchange = "RN";
                //int customerId = 111111;
                //var response = connect.funds(exchange, customerId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //int customerId = 1111111;
                //var response = connect.orders(customerId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //int customerId = 1111111;
                //var response = connect.positions(customerId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //string exchange = "MX";
                //int customerId = 1111111;
                //int orderId = 1111111;
                //var response = connect.history(exchange, customerId, orderId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //string exchange = "RN";
                //int customerId = 1111111;
                //int orderId = 1111111;
                //var response = connect.trade(exchange, customerId, orderId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //int customerId = 1111111;
                //var response = connect.holdings(customerId);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //string exchange = "RN";
                //var response = connect.activeScrips(exchange);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

                //string exchange = "RN";
                //int scripCode = 123123;
                //string interval = "daily";
                //var response = connect.historicalData(exchange, scripCode, interval);
                //var responseObject = JsonConvert.DeserializeObject(response);
                //var responseString = JsonConvert.SerializeObject(responseObject, Formatting.Indented);
                //Console.WriteLine(responseString);

//            Console.ReadKey();

//        }
//    }
//}