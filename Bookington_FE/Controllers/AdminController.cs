using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Json;
using System.Text;

namespace Bookington_FE.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }
        public IActionResult Dashboard()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "admin")
				
			{
				return RedirectToAction("Login", "Home");
			}
			//getCourt by query
			DashboardAdmin res = null;
			string resJsonStr = string.Empty;
			try
			{

                string link = ConfigAppSetting.Api_Link + "dashboard/admin";
				resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
				res = JsonConvert.DeserializeObject<DashboardAdmin>(resJsonStr);
				//
				//luu lai session
				new SessionController(HttpContext).SetSession(KeySession._COURT, res);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			//
			return View(res);
		}
        public IActionResult UserManager(string searchText = "", int currentPage = 1, int pageSize = 10)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //getUser by query
            AccountResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "accounts/query";
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
                res = JsonConvert.DeserializeObject<AccountResponse>(resJsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res);
        }

        public IActionResult ManageCourtReport()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            CourtReportResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "bookington/reports/courtreports";
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken); ;
                //
                res = JsonConvert.DeserializeObject<CourtReportResponse>(resJsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res);
        }
        public IActionResult ManageUserReport()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            UserReportResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "bookington/reports/userreports";
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<UserReportResponse>(resJsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res);
        }
        public IActionResult Profile()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role != "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }


        public IActionResult Logout()
        {
            //check session account

            //AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            //sessAcount.message = string.Empty;
            new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, "");
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public bool DeleteAccount(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "accounts/" + id;
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

        public bool UpdateAccount(string id, string role)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "accounts/assignRole?userId=" + id + "&roleId=" + role;
                //loại bỏ ký tự không gian có độ dài = 0
                link = link.Replace("\u200B", "");
                UpdateRoleRequest request = new UpdateRoleRequest() { RoleId = role };
				string jscontent = JsonConvert.SerializeObject(request);
				StringContent content = new StringContent(jscontent, Encoding.UTF8, "application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAcount.result.sysToken);
                new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, sessAcount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
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
                sessAcount.profileRead.DateOfBirth = DateTime.ParseExact(dob, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
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
        public bool CreateCourtBan(string banCId, string content, int duration)
        {
            string resJsonStr;
            try
            {
                AuthLoginResponse sessAccount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "bookington/reports/courtreports/handle";
                CourtReportResponseWriteDTO banC = new CourtReportResponseWriteDTO() { CourtReportId = banCId, Content = content, IsBanned = true, Duration = duration };
                string jscontent = JsonConvert.SerializeObject(banC);
                StringContent content1 = new StringContent(jscontent, Encoding.UTF8,"application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content1, MethodHttp.POST, sessAccount.result.sysToken);
                new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT,sessAccount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public bool CreateAccountBan(string banAId, string content, int duration)
        {
            string resJsonStr;
            try
            {
                AuthLoginResponse sessAccount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "bookington/reports/userreports/handle/" + banAId;
                UserReportResponseWriteDTO banA = new UserReportResponseWriteDTO() { UserReportId = banAId, Content = content, IsBanned = true, Duration = duration };
                string jscontent = JsonConvert.SerializeObject(banA);
                StringContent content1 = new StringContent(jscontent, Encoding.UTF8,"application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content1, MethodHttp.POST, sessAccount.result.sysToken);
                new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, sessAccount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

    }
}
