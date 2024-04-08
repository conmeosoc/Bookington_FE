using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bookington_FE.Controllers
{
    public class SessionController : Controller
    {
        HttpContext _context;
        public SessionController(HttpContext context)
        {
            _context = context;
        }
        public bool SetSession<T>(string key, T value)
        {
            try
            {
                string valueStr = JsonConvert.SerializeObject(value);
                _context.Session.SetString(key, valueStr);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SetSession<T>(string key, List<T> value)
        {
            try
            {
                string valueStr = JsonConvert.SerializeObject(value);
                _context.Session.SetString(key, valueStr);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<T> GetSessionListT<T>(string key)
        {
            try
            {
                string jsonStr = _context.Session.GetString(key);
                if (string.IsNullOrEmpty(jsonStr))
                    return new List<T>();
                return JsonConvert.DeserializeObject<List<T>>(jsonStr);
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
        public T GetSessionT<T>(string key)
        {
            try
            {
                string jsonStr = _context.Session.GetString(key);
                if (string.IsNullOrEmpty(jsonStr))
                    return default(T);
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
