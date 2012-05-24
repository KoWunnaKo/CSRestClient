using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Net;
using System.IO;

namespace Rest
{
    public class Client
    {
        public string server { get; set; }

        public string MakeRequest(DataTable dt, string resource = "posts", string method = "POST")
        {
            string url = string.Format("{0}/{1}", server, resource);
            string requestBody = ConvertToJson(resource, dt);
            string reponseAsString = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.ContentType = "application/json; charset=utf-8";

                SetBody(request, requestBody);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                reponseAsString = ConvertResponseToString(response);
            }
            catch (Exception ex)
            {
                reponseAsString += "ERROR: " + ex.Message;
            }

            return reponseAsString;
        }

        private void SetBody(HttpWebRequest request, string requestBody)
        {
            if (requestBody.Length > 0)
            {
                using (Stream requestStream = request.GetRequestStream())
                using (StreamWriter writer = new StreamWriter(requestStream))
                {
                    writer.Write(requestBody);
                }
            }
        }

        private string ConvertResponseToString(HttpWebResponse response)
        {
            string result = "Status code: " + (int)response.StatusCode + " " + response.StatusCode + "\r\n";

            foreach (string key in response.Headers.Keys)
            {
                result += string.Format("{0}: {1} \r\n", key, response.Headers[key]);
            }

            result += "\r\n";
            result += new StreamReader(response.GetResponseStream()).ReadToEnd();

            return result;
        }

        private string ConvertToJson(string resource, DataTable dt)
        {
            int resourses_lenght = dt.Rows.Count;
            int proprieties_length = dt.Columns.Count;
            int resourses_i = 0;
            int proprieties_i = 0;

            try
            {
                StringBuilder json = new StringBuilder();
                json.AppendLine("{");

                foreach (DataRow row in dt.Rows)
                {
                    json.AppendLine("\"" + resource + "\": {");
                    foreach (DataColumn column in dt.Columns)
                    {
                        proprieties_i++;
                        json.AppendLine("\"" + column.ColumnName + "\": \"" + row[column.ColumnName].ToString() + "\"");
                        if (proprieties_i < proprieties_length)
                        {
                            json.Append(",");
                        }
                    }

                    json.AppendLine("}");
                    resourses_i++;
                    if (resourses_i < resourses_lenght)
                    {
                        json.Append(",");
                    }

                }
                json.Append("}");

                return json.ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
