using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CleverBotApi
{
    public interface IApi
    {
        Task<string> AskQuestionAsync(string sessionId, string question);
    }
    
    public class Api : IApi
    {
        private readonly HttpClient _httpClient;
        private readonly CookieContainer _cookieContainer;
        
        public Api()
        {
            _cookieContainer = new CookieContainer();
            _httpClient = new HttpClient(new HttpClientHandler() { CookieContainer = _cookieContainer });
        }
        
        public async Task<string> AskQuestionAsync(string sessionId, string question)
        {
            var cookieCollection = _cookieContainer.GetCookies(new Uri("http://www.cleverbot.com"));
            cookieCollection.Add(new Cookie("_cbsid", "-1"));
            cookieCollection.Add(new Cookie("XVIS", "TEI939AFFIAGAYQZ"));

            _httpClient.DefaultRequestHeaders.Host = "www.cleverbot.com";
            _httpClient.DefaultRequestHeaders.Referrer = new Uri("http://www.cleverbot.com/");
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
//            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip, deflate"));
            
//            _httpClient.DefaultRequestHeaders.Add("Content-Type", "text/plain;charset=UTF-8");
            _httpClient.DefaultRequestHeaders.Add("DNT", "1");
            _httpClient.DefaultRequestHeaders.Add("Origin", "http://www.cleverbot.com");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36");
            
//            var values = new Dictionary<string, string>
//            {
//                { "stimulus", question },
//                { "cb_settings_language", "es" },
//                { "cb_settings_scripting", "no" },
//                { "islearning", "1" },
//                { "icognoid", "wsf" },
//                { "icognocheck", "c1303fa2ba311642055d5b946df68c8b" }
//            };

            var textContent = $"stimulus={question}&cb_settings_language=es&cb_settings_scripting=no&islearning=1&icognoid=wsf&icognocheck=c1303fa2ba311642055d5b946df68c8b";

//            var formUrlEncodedContent = new FormUrlEncodedContent(values);
            var httpResponseMessage = await _httpClient.PostAsync("http://www.cleverbot.com/webservicemin?uc=UseOfficialCleverbotAPI&", new StringContent(textContent, Encoding.UTF8, "text/plain"));
            Console.WriteLine(await httpResponseMessage.RequestMessage.Content.ReadAsStringAsync());
            var responseString = httpResponseMessage.Headers.First(h => h.Key == "CBOUTPUT").Value.First();

            return responseString;
        }
    }
}