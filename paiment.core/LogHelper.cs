using System.Text;
using System.IO;
using System.Web;

namespace paiment.core
{
    public class LogHelper
    {
        public static void SaveString(string data, HttpRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();
            if (!string.IsNullOrEmpty(data))
            {
                stringBuilder.Append(data);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("QueryString:");
            foreach (string key in request.QueryString.AllKeys)
            {
                stringBuilder.AppendFormat("{0}={1};", key, request.QueryString[key]);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Form:");
            foreach (string key in request.Form.AllKeys)
            {
                stringBuilder.AppendFormat("{0}={1};", key, request.Form[key]);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Cookies:");
            foreach (string key in request.Cookies.AllKeys)
            {
                stringBuilder.AppendFormat("{0}={1};", key, request.Cookies[key]);
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine("ServerVariables:");
            foreach (string key in request.ServerVariables.AllKeys)
            {
                stringBuilder.AppendFormat("{0}={1};", key, request.ServerVariables[key]);
            }

            stringBuilder.AppendLine();
            SaveString(stringBuilder.ToString());
        }

        public static void SaveString(System.Web.HttpRequest request)
        {
            SaveString(string.Empty, request);
        }

        public static void SaveString(string dataString)
        {
            using (StreamWriter sw = File.AppendText(GetFilePath())) sw.WriteLine(dataString);
        }

        private static string GetFilePath()
        {
            string folderPath = @"c:\Logs\";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, "Ispn.txt");
        }
    }
}