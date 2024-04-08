using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;


namespace Bookington_FE.Controllers
{
    public class OwnerController : Controller
    {
        public MainLayoutViewModel MainLayoutViewModel { get; set; }
        public OwnerController() 
        {
            this.MainLayoutViewModel = new MainLayoutViewModel();
            this.ViewData["MainLayoutViewModel"] = this.MainLayoutViewModel;
        }

        public IActionResult Index()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            GetNotify();
            //
            return View(sessAcount);
        }
        public void GetNotify()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "owner")
            {
                return;
            }
            //call query notify
            NotificationResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "notifications?UserId=" + sessAcount.result.userId;

                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                res = JsonConvert.DeserializeObject<NotificationResponse>(resJsonStr);
                if(res != null && res.result !=null && res.result.Count > 0)
                {
                    this.MainLayoutViewModel = new MainLayoutViewModel() { ArrayNotification = res.result.OrderByDescending(ite => ite.CreateAt).ToList() };
                    this.ViewData["MainLayoutViewModel"] = this.MainLayoutViewModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public IActionResult Dashboard()
        {
			//check session account
			AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
			if (sessAcount == null || sessAcount.result.role != "owner")
			{
				return RedirectToAction("Login", "Home");
			}
			//getCourt by query
			DashboardOwnerResponse res = null;
			string resJsonStr = string.Empty;
			try
			{
               
				string link = ConfigAppSetting.Api_Link + "dashboard/owner?ownerId=" + sessAcount.result.userId;
				resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
				res = JsonConvert.DeserializeObject<DashboardOwnerResponse>(resJsonStr);
				//
				//luu lai session
				new SessionController(HttpContext).SetSession(KeySession._COURT, res);
			}
			catch (Exception ex)
			{
				throw ex;
			}
            GetNotify();
            //
            return View(res);
		}
        public IActionResult History(string searchText = "", int currentPage = 1, int pageSize = 10)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //getCourt by query
            transactionResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "transaction-history/owner";
                string param = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    param = "SearchText=" + searchText;
                }
                //
                if (currentPage > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&PageNumber=" + currentPage;
                    else
                        param += "PageNumber=" + currentPage;
                }
                //
                if (pageSize > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&MaxPageSize=" + pageSize;
                    else
                        param += "MaxPageSize=" + pageSize;
                }
                //
                if (!string.IsNullOrEmpty(param))
                    link += "?" + param;
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<transactionResponse>(resJsonStr);
                //
                //luu lai session
                new SessionController(HttpContext).SetSession(KeySession._COURT, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            GetNotify();
            //
            return View(res);
        }
        public IActionResult ManageYard(string searchText = "", int currentPage = 1, int pageSize = 10)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //getCourt by query
            CourtResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "courts";
                string param = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    param = "SearchText=" + searchText;
                }
                //
                if (currentPage > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&PageNumber=" + currentPage;
                    else
                        param += "PageNumber=" + currentPage;
                }
                //
                if (pageSize > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&MaxPageSize=" + pageSize;
                    else
                        param += "MaxPageSize=" + pageSize;
                }
                //
                if (!string.IsNullOrEmpty(param))
                    link += "?" + param;
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<CourtResponse>(resJsonStr);
                //
                //luu lai session
                new SessionController(HttpContext).SetSession(KeySession._COURT, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            ViewData["province"] = new SessionController(HttpContext).GetSessionT<List<ProvinceInfo>>(KeySession._PROVINCE);
            //
            GetNotify();
            //
            return View(res);
        }

        public IActionResult Profile()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            GetNotify();
            //
            return View(sessAcount);
        }

        public IActionResult Chat()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View();
        }
        public IActionResult Competition()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View();
        }
        public IActionResult SubCourt(string courtID)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            CourtResponse arrCourt = new SessionController(HttpContext).GetSessionT<CourtResponse>(KeySession._COURT);
            CourtModel parentCourt = (from x in arrCourt.result
                                      where x.Id.ToLower() == courtID.ToLower()
                                      select x).FirstOrDefault();
            //

            //getCourt by query
            SubcourtResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "subcourts/" + courtID;

                //
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<SubcourtResponse>(resJsonStr);
                //
                SubCourtAllModel resAll = new SubCourtAllModel();
                List<SubCourtDetails> subcourtDetails = new List<SubCourtDetails>();
                //
                foreach (SubcourtModel sc in res.result)
                {
                    string subcourtId = sc.Id;
                    //get schedule by query
                    SlotResponse resslot = null;
                    
                    string resslotJsonStr = string.Empty;
                    string linkslot = ConfigAppSetting.Api_Link + "slots/schedule/" + subcourtId;

                    resslotJsonStr = GlobalFunc.CallAPI(linkslot, null, MethodHttp.GET, sessAcount.result.sysToken);
                    //
                    resslot = JsonConvert.DeserializeObject<SlotResponse>(resslotJsonStr);
                    //
                    Dictionary<string, List<SlotModel>> mappingSlotToTime = new Dictionary<string, List<SlotModel>>();

                    BookingResponse resbook = null;
                    string resbookJsonStr = string.Empty;
                    string linkbook = ConfigAppSetting.Api_Link + "booking-history/sub-courts?SubCourtId=" + subcourtId;
                    List<BookingModel> bookings = new List<BookingModel>();

                    resbookJsonStr = GlobalFunc.CallAPI(linkbook, null, MethodHttp.GET, sessAcount.result.sysToken);
                    //
                    resbook = JsonConvert.DeserializeObject<BookingResponse>(resbookJsonStr);     //

                    foreach (SlotModel slotModel in resslot?.result)
                    {
                        string key = slotModel.startTime.ToString(@"hh\:mm") + "-" + slotModel.endTime.ToString(@"hh\:mm");

                        if (!mappingSlotToTime.Keys.Contains(key))
                        {
                            mappingSlotToTime.Add(key, new List<SlotModel> { slotModel });
                        }
                        else
                        {
                            mappingSlotToTime[key].Add(slotModel);
                        }
                    }
                    //
                   
                    SubCourtDetails dt = new SubCourtDetails();
                    dt.subcourtModel = sc;
                    dt.GroupSlotByTime = mappingSlotToTime;
                    dt.bookingModel = bookings;
                    subcourtDetails.Add(dt);
                    
                    


                    
                    //
                }
                //
                resAll.SubCourtDetails = subcourtDetails;
                //
                resAll.courtParent = parentCourt;
                //
                
                //
                GetNotify();
                //
                return View(resAll);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Logout()
        {
            //check session account

            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, "");
            return RedirectToAction("Login", "Home");
        }

        public bool DeleteCourt(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "courts/" + id;
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.DELETE, sessAcount.result.sysToken);
                //
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public bool UpdateCourt(string cid, string coid, string cdid, string cname, string caddress, string des, string copen, string cclose, string image)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "courts/" + cid;
#if DEBUG
                
                image = "D:\\NewWeb\\Bookington_FE\\Bookington_FE\\wwwroot\\images\\" + Path.GetFileName(image);
#endif
                byte[] imgData = System.IO.File.ReadAllBytes(image);
                //
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(link);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + sessAcount.result.sysToken);
                    client.Timeout = new TimeSpan(0, 0, 30);
#if DEBUG
                    client.Timeout = new TimeSpan(0, 10, 00);
#endif
                    //
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent(cid), "id");
                    form.Add(new StringContent(sessAcount.result.userId), "OwnerId");
                    form.Add(new StringContent(cdid), "DistrictId");
                    form.Add(new StringContent(cname), "Name");
                    form.Add(new StringContent(caddress), "Address");
                    form.Add(new StringContent(des), "Description");
                    form.Add(new StringContent(copen), "OpenAt");
                    form.Add(new StringContent(cclose), "CloseAt");
                    form.Add(new ByteArrayContent(imgData, 0, imgData.Length), "courtImages");
                    HttpResponseMessage response;
                    response = client.PutAsync("", form).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        resJsonStr = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        resJsonStr = response.Content.ReadAsStringAsync().Result;
                    }
                }


                //string link = ConfigAppSetting.Api_Link + "accounts/" + id;
                //resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.POST, sessAcount.result.sysToken);
                //

            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            return true;

            //string link = ConfigAppSetting.Api_Link + "accounts/" + id;
            //resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.POST, sessAcount.result.sysToken);
            //

        }


        public IActionResult Search()
        {
            return View();
        }
        public bool DeleteSubCourt(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "subcourts/" + id;
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.DELETE, sessAcount.result.sysToken);
                //
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public bool UpdateProfile(string name, string dob)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                string id = sessAcount.result.userId;
                // 
                string link = ConfigAppSetting.Api_Link + "accounts/" + id;
                UpdateAccountRequest request = new UpdateAccountRequest() { fullName = name, dateOfBirth = dob };
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAcount.result.sysToken);
                //
                //if success
                sessAcount.profileRead.DateOfBirth = DateTime.ParseExact(dob, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                sessAcount.profileRead.FullName = name;
                sessAcount.result.fullName = name;
                //
                new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, sessAcount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public IActionResult Schedule(string subcourtId, SubcourtResponse ressub)

        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //get schedule by query
            SlotResponse res = null;
            string resJsonStr = string.Empty;
            string link = ConfigAppSetting.Api_Link + "slots/schedule/" + subcourtId;

            resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
            //
            res = JsonConvert.DeserializeObject<SlotResponse>(resJsonStr);
            //
            Dictionary<string, List<SlotModel>> mappingSlotToTime = new Dictionary<string, List<SlotModel>>();
            foreach (SlotModel slotModel in res?.result)
            {
                string key = slotModel.startTime.ToString(@"hh\:mm") + "-" + slotModel.endTime.ToString(@"hh\:mm");

                if (!mappingSlotToTime.Keys.Contains(key))
                {
                    mappingSlotToTime.Add(key, new List<SlotModel> { slotModel });
                }
                else
                {
                    mappingSlotToTime[key].Add(slotModel);
                }
            }
            SubCourtAllModel model = new SubCourtAllModel();
            return RedirectToAction("Subcourt", "Owner", new { courtID = "", resall = model });
            //
            //return View(res);
            //
        }
        public bool UpdateSlot(string id, double price, bool status, string idsubcourt)
        {
            //
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            //PUT/ slots​ / updateSlot​ / ref -subCourt
            string link = ConfigAppSetting.Api_Link + "slots​/updateSlot​/ref-subCourt?subCourtId=" + idsubcourt;
            //loại bỏ ký tự không gian có độ dài = 0
            link = link.Replace("\u200B", "");
            UpdateSlotRequest request = new UpdateSlotRequest() { Id = id, Price = price, IsActive = status };
            string jscontent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jscontent, Encoding.UTF8, "application/json");
            string resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAcount.result.sysToken);
            return true;
        }
        
        public List<DistrictModel> GetDistrictByProvince(string id = "0")
        {
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            //
            List<DistrictModel> ressult = new List<DistrictModel>();
            string resDistrict = GlobalFunc.CallAPI(ConfigAppSetting.Api_Link + "districts/province?id=" + id, null, MethodHttp.GET, sessAcount.result.sysToken);
            DistrictResponse dis = JsonConvert.DeserializeObject<DistrictResponse>(resDistrict);
            //
            return dis.result;
        }
        public bool CreateCourt(string name, string district, string address, string description, string open, string close, string image)
        {

            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "courts/";
#if DEBUG
                image = "D:\\NewWeb\\Bookington_FE\\Bookington_FE\\wwwroot\\images\\" + Path.GetFileName(image);
#endif
                byte[] imgData = System.IO.File.ReadAllBytes(image);
                //
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(link);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + sessAcount.result.sysToken);
                    client.Timeout = new TimeSpan(0, 0, 30);
#if DEBUG
                    client.Timeout = new TimeSpan(0, 10, 00);
#endif
                    //
                    MultipartFormDataContent form = new MultipartFormDataContent();

                    form.Add(new StringContent(sessAcount.result.userId), "OwnerId");
                    form.Add(new StringContent(district), "DistrictId");
                    form.Add(new StringContent(name), "Name");
                    form.Add(new StringContent(address), "Address");
                    form.Add(new StringContent(description), "Description");
                    form.Add(new StringContent(open), "OpenAt");
                    form.Add(new StringContent(close), "CloseAt");
                    form.Add(new ByteArrayContent(imgData, 0, imgData.Length), "courtImages");
                    HttpResponseMessage response;
                    response = client.PostAsync("", form).Result;


                    if (response.IsSuccessStatusCode)
                    {
                        resJsonStr = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        resJsonStr = response.Content.ReadAsStringAsync().Result;
                    }
                }


                //string link = ConfigAppSetting.Api_Link + "accounts/" + id;
                //resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.POST, sessAcount.result.sysToken);
                //

            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            return true;
        }
        public bool CreateSCourt(string id, string nameSC, string typeid, string scstatus, string scdelete)
        {

            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "subcourts";
                SubcourtResquest request = new SubcourtResquest() { parentCourtId = id, name = nameSC, courtTypeId = Convert.ToInt32(typeid), isActive = Convert.ToBoolean(scstatus), isDeleted = Convert.ToBoolean(scdelete) };
                List<SubcourtResquest> arrRe = new List<SubcourtResquest>() { request };
                string jsrequest = JsonConvert.SerializeObject(arrRe);
                StringContent content = new StringContent(jsrequest, Encoding.UTF8, "application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.POST, sessAcount.result.sysToken);
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            return true;
        }

        public bool MarkAsRead(string id, bool isRead)
        {
            string resJsonStr;
            try
            {
                if(string.IsNullOrEmpty(id)) { return true; }
                //
                List<string> arrid = id.Split(',').ToList();
                AuthLoginResponse sessAccount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                string link = ConfigAppSetting.Api_Link + "markAllAsRead";
                //UpdateNotificationRequest request = new UpdateNotificationRequest() { NotiId = id, IsRead =  isRead};
                List<string> arrRe = arrid;
                string jsrequest = JsonConvert.SerializeObject(arrRe);
                StringContent content = new StringContent(jsrequest, Encoding.UTF8,"application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAccount.result.sysToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        
    }
}
