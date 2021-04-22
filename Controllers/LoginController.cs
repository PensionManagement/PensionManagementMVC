using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementMVC.Controllers
{
    public class LoginController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));
        //Get Login
        
        public IActionResult Login()
        {
            
            _log4net.Info("Http Login Request Initiated");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVerification user)
        {
            TempData["aadhar"] = user.AadhaarNo;
           
            string token;
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44316/");
                var postData = httpclient.PostAsJsonAsync<LoginVerification>("/api/Authentication/VerifyUser", user);
                var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    token = await res.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    if (token =="Yes")
                    {
                        return RedirectToAction("CheckPassword");
                    }
                    if (token=="No")
                    {
                        return RedirectToAction("NewPassword");
                    }
                    
                }
            }
            return View();

        }
        public IActionResult CheckPassword()
        {

            _log4net.Info("Http Login Request Initiated");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CheckPassword(CheckPassword user)
        {
            user.AadhaarNo = Convert.ToString(TempData.Peek("aadhar"));
            string token;
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("https://localhost:44316/");
                //CheckPassword Obj;
                StringContent content = new (JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpclient.PostAsync("api/Authentication/AuthenicateUser", content))
                {
                     token = await response.Content.ReadAsStringAsync();
                    //token = JsonConvert.DeserializeObject<String>(apiResponse);

                }
                
                if (token == "")
                {
                    return RedirectToAction("CheckPassword");
                }
                
            }
            return RedirectToAction("GetPensioner", "ProcessPension");

        }
        public IActionResult NewPassword()
        {

            _log4net.Info("Http Login Request Initiated");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> NewPassword(AddPassword add)
        {
            CheckPassword obj = new();
            obj.AadhaarNo = TempData.Peek("aadhar").ToString();
            obj.Password = add.Password;
            string Aadhar = TempData.Peek("aadhar").ToString();
            PensionDetail usr;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44316/api/Authentication/" + Aadhar,content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usr = JsonConvert.DeserializeObject<PensionDetail>(apiResponse);
                }
                if (usr!=null)
                {
                    return RedirectToAction("CheckPassword");
                }
                //var postData = httpclient.PutAsJsonAsync<CheckPassword>("/api​/Authentication​/"+Aadhar, content);
                /*var res = postData.Result;
                if (res.IsSuccessStatusCode)
                {
                    token = await res.Content.ReadAsStringAsync();
                    TempData["token"] = token;
                    if (token == null)
                    {
                        return RedirectToAction("CheckPassword");
                    }
                }*/
            }
            return RedirectToAction("NewPassword");

        }
    }
}
