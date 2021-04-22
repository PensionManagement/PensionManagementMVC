using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementMVC.Controllers
{
    public class ProcessPensionController : Controller
    {

        public ActionResult GetPensioner()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetPensioner(ProcessPension pension1)
        {
            Pension pension = new();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pension1), Encoding.UTF8, "application/json");


                using (var response = await httpClient.PostAsync("http://localhost:23578/api/ProcessPension/PensionDetail", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    pension = JsonConvert.DeserializeObject<Pension>(apiResponse);
                    TempData["ResponsePension"] = JsonConvert.SerializeObject(pension);
                    TempData["Pensioner"] = JsonConvert.SerializeObject(pension1);
                }

            }
            return RedirectToAction("PensionDetail");
        }
        //[HttpPost]
        //public async Task<ActionResult> GetPension(ProcessPension pension1)
        //{
        //    ProcessPension pension = new ProcessPension();
        //    using (var httpClient = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(pension1), Encoding.UTF8, "application/json");

        //        using (var response = await httpClient.PostAsync("http://localhost:23578/api/ProcessPension/ProcessPension", content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            pension = JsonConvert.DeserializeObject<ProcessPension>(apiResponse);
        //        }

        //    }
        //    return View();
        //}
        public ActionResult PensionDetail(Pension pension)
        {
            string strUser = TempData.Peek("ResponsePension").ToString();
            pension = JsonConvert.DeserializeObject<Pension>(strUser);
            return View(pension);
        }
        public ActionResult PensionNext()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PensionNext(BankDetails bankDetails)
        {
            string strUser = TempData.Peek("ResponsePension").ToString();
            Pension pension = JsonConvert.DeserializeObject<Pension>(strUser);
            string pensioner = TempData.Peek("Pensioner").ToString();
            ProcessPension processpension = JsonConvert.DeserializeObject<ProcessPension>(pensioner);


            ProcessPensionInput processPensionInput = new ProcessPensionInput();
            processPensionInput.AadhaarNo = processpension.AadhaarNo;
            processPensionInput.PensionAmount = (double)pension.PensionAmount;
            if (bankDetails.BankType==1)
            {
                processPensionInput.BankCharge = 500;
            }
            else
            {
                processPensionInput.BankCharge = 550;
            }

            string apiResponse;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(processPensionInput), Encoding.UTF8, "application/json");


                using (var response = await httpClient.PostAsync("http://localhost:23578/api/ProcessPension/ProcessPension", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();

                }
                if (apiResponse != "" || apiResponse != null)
                {
                    if (apiResponse == "10")
                    {
                        return RedirectToAction("ResultPage","ProcessPension", new { msg = "You have sucessfully Get pension" });
                    }
                    if (apiResponse == "20")
                    {
                        TempData["errorResponse"] = "Pensioner Values not match";
                        return RedirectToAction("ResultPage", "ProcessPension", new { msg = "Pensioner Values not match" });


                    }
                    if (apiResponse == "21")
                    {
                        TempData["errorResponse"] = "Some Error Occured";
                        return RedirectToAction("ResultPage","ProcessPension", new { msg="Some Error Occured" });

                    }
                }
            }
            return View();
        }
        public IActionResult ResultPage(string msg)
        {
            string strUser = TempData.Peek("ResponsePension").ToString();
            Pension pension = JsonConvert.DeserializeObject<Pension>(strUser);
            Message message = new Message();
            message.message = msg;
            message.PensionAmount = (double)pension.PensionAmount;
            return View(message);
        }
        public async Task<bool> AadharCheck(string Aadhaarno)
       {
            string apiResponse;
            //string validateaadhar;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:23578/api/ProcessPension/IsValidAadhaar/" + Aadhaarno))
                {
                     apiResponse = await response.Content.ReadAsStringAsync();
                    //validateaadhar = JsonConvert.DeserializeObject<String>(apiResponse);
                }

            }
            if (apiResponse == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
