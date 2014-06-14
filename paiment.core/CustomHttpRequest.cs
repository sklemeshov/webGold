using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace paiment.core
{
    public static class CustomHttpRequest
    {
        static public T SendHttpRequest<T>(string url, string formParameters, string methodType)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = methodType;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization: Basic ");

            byte[] encodedBytes = Encoding.UTF8.GetBytes(formParameters);
            httpWebRequest.AllowWriteStreamBuffering = true;

            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(encodedBytes, 0, encodedBytes.Length);
            requestStream.Close();

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream receiveStream = httpWebResponse.GetResponseStream();

            StreamReader sr = new StreamReader(receiveStream);
            string resultString = sr.ReadToEnd();

            sr.Close();
            return JsonConvert.DeserializeObject<T>(resultString);
        }
    }
}
