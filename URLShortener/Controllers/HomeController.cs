using CaptchaMvc.Attributes;
using Newtonsoft.Json;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    public class HomeController : Controller
    {
        private const string googleUrlShortnerApikey = "AIzaSyCnNgwyhbHFX_Znb3jwr40YT1lr4h8YwCk";

        public ActionResult Index()
        {
            return View();
        }

        // L'action urlShorter est appellé au moment du click sur le button 'Shorten URL'   
        // cette action fait appel à l'API 'GOOGLE URL SHORTENER' pour faire raccourcir les Urls passé en paramèttre . 
        [HttpPost]
        public ActionResult urlShorter(string url)
        {
            string finalURL = "";
            string post = "{\"longUrl\": \"" + url + "\"}";
            string shortUrl = url;
            var recaptchaResponse = Request["g-recaptcha-response"];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url?key=" + googleUrlShortnerApikey);
            try
            {  
                request.Method = "POST";
                request.ContentLength = post.Length;
                request.ContentType = "application/json";
                request.Headers.Add("Cache-Control", "no-cache");
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] postBuffer = Encoding.ASCII.GetBytes(post);
                    requestStream.Write(postBuffer, 0, postBuffer.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            string json = responseReader.ReadToEnd();
                            finalURL = JsonConvert.DeserializeObject<URLShortenerApiResult>(json).id;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // API Google's URL Shortener is down...
                return Json(new { Result = "Service unvailable" });
            }

            return Json(new { Result = finalURL });
        }
    }
}
