using System;
using System.IO;
using System.Net;

namespace RestClientName
{

    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    ///start authentication type and technique



    public enum authenticationType
    {
        Basic,
        NTLM
    }

    public enum autheticationTechnique
    {
        RollYourOwn,
        NetworkCredential
    }

    ///End Auntentication Type and Technique











    class RESTClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        ///start authentication. Authentication type and technique. Username and password

        public authenticationType authType { get; set; }
        public autheticationTechnique authTech { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }

        ///end authentication

        public RESTClient()
        {
            endPoint = "";
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {

            string strResponseValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();


            ///start Auntentication . This part get the username and password

            String authHeader = System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(userName + ":" + userPassword));
            request.Headers.Add("Authorization", authType.ToString() + " " + authHeader);



            ///End Authenication



            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();




                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }
    }
}
