using Bookington_FE.Controllers;
using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Bookington_FE
{
	public static class GlobalFunc
	{
		public static string CallAPI(string link, StringContent content = null, MethodHttp method = MethodHttp.GET, string token = "")
		{
			string resJsonStr;
			try
			{
				HttpClientHandler clientHandler = new HttpClientHandler();
				clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
				using (var client = new HttpClient(clientHandler))
				{
					client.BaseAddress = new Uri(link);
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					if (token != "")
						client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.Timeout = new TimeSpan(0, 0, 30);
#if DEBUG
                    client.Timeout = new TimeSpan(0, 10, 00);
#endif
					//
					//AuthLoginRequest request = new AuthLoginRequest() { Phone = phone, Password = password };
					//
					//var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
					HttpResponseMessage response;
					if (method == MethodHttp.POST)
						response = client.PostAsync("", content).Result;
					else if (method == MethodHttp.GET)
						response = client.GetAsync("").Result;
					else if (method == MethodHttp.DELETE)
						response = client.DeleteAsync(link).Result;
					else
						response = client.PutAsync(link, content).Result;

					if (response.IsSuccessStatusCode)
					{
						resJsonStr = response.Content.ReadAsStringAsync().Result;
					}
					else
					{
						resJsonStr = response.Content.ReadAsStringAsync().Result;
					}
				}
				return resJsonStr;
			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
