using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Web.Http;
using System.Collections;
using Windows.Storage.Streams;
using System.Threading;
using System.Net;
using Windows.Storage;

namespace HttpClientTest
{
    public class HttpManager
    {

        public async void SendGetMethod(string url, Action<HttpResponseMessage> resopnse)
        {
            HttpClient client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            string header = "";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                // throw new Exception("Invalid header value: " + header);
            }
            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri(url);
            try
            {

                httpResponse = await client.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                resopnse(httpResponse);
            }
            catch (Exception ex)
            {
                Debug.Write("Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message);
            }

        }
        public async void SendPostMethod(string url, StorageFile file, Dictionary<string, string> param, Action<HttpMultipartFormDataContent> FileContent, Action<string> response)
        {
            HttpClient client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            string header = "";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri(url);
            var content = new HttpMultipartFormDataContent();

            foreach (var item in param)
            {
                content.Add(new HttpStringContent(item.Value),item.Key);

            }
            //var fileContent = new HttpStreamContent(await file.OpenAsync(FileAccessMode.Read));
            //fileContent.Headers.Add("Content-Type", "multipart/form-data");
            //传递出去
            FileContent(content);
            try
            {
                httpResponse = await client.PostAsync(requestUri, content);
                response(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
        public async void SendPostMethod(string url, StorageFile file, Dictionary<string, string> param, Func<HttpMultipartFormDataContent, Task> FileContent, Action<string> response)
        {
            HttpClient client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            string header = "";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                // throw new Exception("Invalid header value: " + header);
            }
            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                //  throw new Exception("Invalid header value: " + header);
            }
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri(url);
            var content = new HttpMultipartFormDataContent();

            foreach (var item in param)
            {
                content.Add(new HttpStringContent(item.Value), item.Key);

            }
            //var fileContent = new HttpStreamContent(await file.OpenAsync(FileAccessMode.Read));
            //fileContent.Headers.Add("Content-Type", "multipart/form-data");
            //传递出去

            // FileContent.Invoke(content);
            await FileContent(content);

            try
            {
                httpResponse = await client.PostAsync(requestUri, content);
                response(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
   
        public async void SendPostMethod(string url, Dictionary<string, string> param, Action<string> response)
        {
            HttpClient client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            string header = "";
            headers.Add("", "");
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri(url);

            var content = new HttpFormUrlEncodedContent(param);
            try
            {
                httpResponse = await client.PostAsync(requestUri, content);

                response(await httpResponse.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {

            }

        }
    }
}
