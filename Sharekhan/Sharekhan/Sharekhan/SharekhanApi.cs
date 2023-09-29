using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace Sharekhan.Sharekhan.Sharekhan
{
    class SharekhanApi
    {
        protected string APIURL = "https://api.sharekhan.com/skapi/services"; //prod endpoint

        SharekhanToken Token { get; set; }

        public SharekhanApi(string _accessToken = "", string _apiKey = "", string _vendorKey = "")
        {
            this.Token = new SharekhanToken();
            this.Token.accessToken = _accessToken;
            this.Token.apiKey = _apiKey;
            this.Token.vendorKey = _vendorKey;
        }

        /* Validate Token data internally */
        private bool ValidateToken(SharekhanToken token)
        {
            bool result = false;
            if (token != null)
            {
                if (token.accessToken != "" && token.apiKey != "")
                {
                    result = true;
                }
            }
            else
                result = false;

            return result;
        }

        public string GetLoginUrl(string apiKey, string vendorKey, long state, long? versionId)
        {

            string URL = "https://api.sharekhan.com/skapi/auth/login.html?" + "api_key=" + apiKey;

            if (vendorKey != null)
            {
                URL += "&vendor_key=" + vendorKey;
            }
            else
            {
                Console.WriteLine("no vendor key");
            }
            if (state != null)
            {
                URL += "&state=" + state.ToString();
            }
            else
            {
                Console.WriteLine("no state");
            }
            if (versionId != null)
            {
                URL += "&version_id=" + versionId.ToString();
            }
            else
            {
                Console.WriteLine("no versionId");
            }

            return URL;

        }

        public string DecryptStringFromAES(string encryptedText, string key)
        {
            string iv = "AAAAAAAAAAAAAAAAAAAAAA==";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            string base64EncryptedText = encryptedText.Replace('-', '+').Replace('_', '/').PadRight(encryptedText.Length + (4 - encryptedText.Length % 4) % 4, '=');

            byte[] encryptedBytes = Convert.FromBase64String(base64EncryptedText);

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(keyBytes), 128, ivBytes, null);
            cipher.Init(false, parameters);

            byte[] decryptedBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
            int len = cipher.ProcessBytes(encryptedBytes, 0, encryptedBytes.Length, decryptedBytes, 0);
            cipher.DoFinal(decryptedBytes, len);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public string EncryptStringToAES(string plaintext, string key)
        {
            string iv = "AAAAAAAAAAAAAAAAAAAAAA==";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Convert.FromBase64String(iv);
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(keyBytes), 128, ivBytes, null);
            cipher.Init(true, parameters);

            byte[] encryptedBytes = new byte[cipher.GetOutputSize(plaintextBytes.Length)];
            int len = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, encryptedBytes, 0);
            cipher.DoFinal(encryptedBytes, len);

            return Convert.ToBase64String(encryptedBytes);
        }

        public string DecryptStringFromAES1(string encryptedText, string key)
        {
            //Console.WriteLine(encryptedText);
            string iv = "AAAAAAAAAAAAAAAAAAAAAA==";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            string base64EncryptedText = Uri.UnescapeDataString(encryptedText.Replace('-', '+').Replace('_', '/'));

            byte[] encryptedBytes = Convert.FromBase64String(base64EncryptedText);

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(keyBytes), 128, ivBytes, null);
            cipher.Init(false, parameters);

            byte[] decryptedBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
            int len = cipher.ProcessBytes(encryptedBytes, 0, encryptedBytes.Length, decryptedBytes, 0);
            cipher.DoFinal(decryptedBytes, len);

            return Encoding.UTF8.GetString(decryptedBytes);
        }




        public string EncryptStringToAES1(string plaintext, string key)
        {
            string iv = "AAAAAAAAAAAAAAAAAAAAAA==";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Convert.FromBase64String(iv);
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(keyBytes), 128, ivBytes, null);
            cipher.Init(true, parameters);

            byte[] encryptedBytes = new byte[cipher.GetOutputSize(plaintextBytes.Length)];
            int len = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, encryptedBytes, 0);
            cipher.DoFinal(encryptedBytes, len);

            return Convert.ToBase64String(encryptedBytes);
        }

        public string GenerateAccessToken(string api_Key, string request_Token, int state01, string secretKey, string vendorkey, int versionId)
        {
            try
            {
                string URL = APIURL + "/access/token";
                Console.WriteLine(URL);
                string token = request_Token;
                string key = secretKey;
                string decData = DecryptStringFromAES1(token, key);
                //Console.Write(decData);
                string[] tokenParts = decData.Split('|');

                string newToken = tokenParts[1] + "|" + tokenParts[0];
                Console.Write(newToken);
                string encData = EncryptStringToAES1(newToken, key);
                string formattedEncData = encData.Replace('+', '-').Replace('/', '_');

                //Console.WriteLine("Encrypted token: " + formattedEncData);

                var requestData = new
                {
                    apiKey = api_Key,
                    requestToken = formattedEncData,
                    vendorKey = vendorkey,
                    state = state01,
                    versionId = versionId


                };
                var json = JsonConvert.SerializeObject(requestData);

                string Json = POSTWebRequest0(URL, json);

                return Json;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GenerateAccessTokenWithoutVersionId(string api_Key, string request_Token, int state01, string secretKey, string vendorkey)
        {
            try
            {
                string URL = APIURL + "/access/token";
                Console.WriteLine(URL);
                string token = request_Token;
                string key = secretKey;
                string decData = DecryptStringFromAES(token, key);
                //Console.Write(decData);
                string[] tokenParts = decData.Split('|');

                string newToken = tokenParts[1] + "|" + tokenParts[0];
                //Console.Write(newToken);
                string encData = EncryptStringToAES(newToken, key);
                //string formattedEncData = encData.Replace('+', '-').Replace('/', '_');
                Console.Write(encData);
                //Console.WriteLine("Encrypted token: " + formattedEncData);

                var requestData = new
                {
                    apiKey = api_Key,
                    requestToken = encData,
                    vendorKey = vendorkey,
                    state = state01
                };
                var json = JsonConvert.SerializeObject(requestData);

                string Json = POSTWebRequest0(URL, json);

                return Json;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string POSTWebRequest0(string URL, string Data)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";

                byte[] byteArray = Encoding.UTF8.GetBytes(Data);
                httpWebRequest.ContentLength = byteArray.Length;
                string Json = "";

                using (Stream dataStream = httpWebRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        Json = reader.ReadToEnd();
                    }
                }

                return Json;
            }
            catch (Exception ex)
            {
                return "PostError:" + ex.Message;
            }
        }




        private string POSTWebRequest(SharekhanToken agr, string URL, string Data)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

                if (agr != null)
                {
                    // Add the access token and api key to the headers
                    httpWebRequest.Headers.Add("access-token", agr.accessToken);
                    httpWebRequest.Headers.Add("api-key", agr.apiKey);

                    if (!string.IsNullOrEmpty(agr.vendorKey))
                    {
                        httpWebRequest.Headers.Add("vendor-key", agr.vendorKey);
                    }
                }

                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";

                string nonNullData = RemoveNullProperties(Data);
                byte[] byteArray = Encoding.UTF8.GetBytes(nonNullData);

                httpWebRequest.ContentLength = byteArray.Length;
                string Json = "";



                using (Stream dataStream = httpWebRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        Json = reader.ReadToEnd();
                    }
                }

                return Json;
            }
            catch (Exception ex)
            {
                return "PostError:" + ex.Message;
            }
        }

        private string RemoveNullProperties(string json)
        {
            var jsonObject = JObject.Parse(json);
            var properties = jsonObject.Properties().ToList();

            foreach (var property in properties)
            {
                if (property.Value.Type == JTokenType.Null)
                {
                    property.Remove();
                }
            }

            return jsonObject.ToString();
        }
        private string GETWebRequest(SharekhanToken agr, string URL)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

                if (agr != null)
                {
                    // Add the access token and API key to the headers
                    httpWebRequest.Headers.Add("access-token", agr.accessToken);
                    httpWebRequest.Headers.Add("api-key", agr.apiKey);

                    if (!string.IsNullOrEmpty(agr.vendorKey))
                    {
                        // Add the vendor-key only if it is not null or empty
                        httpWebRequest.Headers.Add("vendor-key", agr.vendorKey);
                    }
                }

                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";

                string Json = "";
                WebResponse response = httpWebRequest.GetResponse();

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    Json = reader.ReadToEnd();
                }

                return Json;
            }
            catch (Exception ex)
            {
                return "GetError: " + ex.Message;
            }
        }



        public string placeOrder(OrderInfo order)
        {
            try
            {
                SharekhanToken Token = this.Token;

                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/orders";

                        string PostData = JsonConvert.SerializeObject(order);

                        string Json = POSTWebRequest(Token, URL, PostData);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string modifyOrder(OrderInfo order)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/orders";

                        Console.WriteLine(URL);

                        string PostData = JsonConvert.SerializeObject(order);

                        string Json = POSTWebRequest(Token, URL, PostData);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string cancelOrder(OrderInfo order)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/orders";

                        string PostData = JsonConvert.SerializeObject(order);

                        string Json = POSTWebRequest(Token, URL, PostData);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string funds(string exchange, int customerId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/limitstmt/" + exchange + "/" + customerId;

                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string cancelByOrderId(int orderId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/cancelOrder/" + orderId;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string orders(int customerId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/reports/" + customerId;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string positions(int customerId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/trades/" + customerId;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string history(string exchange, int customerId, int orderId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/reports/" + exchange + "/" + customerId + "/" + orderId;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string trade(string exchange, int customerId, int orderId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/reports/" + exchange + "/" + customerId + "/" + orderId + "/trades";



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string holdings(int customerId)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/holdings/" + customerId;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string activeScrips(string exchange)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/master/" + exchange;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string historicalData(string exchange, int scripCode, string interval)
        {
            try
            {
                SharekhanToken Token = this.Token;
                if (Token != null)
                {
                    if (ValidateToken(Token))
                    {
                        string URL = APIURL + "/historical/" + exchange + "/" + scripCode + "/" + interval;



                        string Json = GETWebRequest(Token, URL);

                        return Json;
                    }
                    else
                    {
                        return "The token is invalid";
                    }
                }
                else
                {
                    return "The token is invalid";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
