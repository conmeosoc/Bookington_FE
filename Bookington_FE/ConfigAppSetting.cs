namespace Bookington_FE
{
    public class ConfigAppSetting
    {
        public static string Api_Link { get; set; } = string.Empty;
        public static int SessionTimeout { get; set; } = 60;
        public static void LoadConfig(IConfiguration config)
        {
            try
            {
                Api_Link = config["WebConfig:Api_Link"];
                string sTimeout = config["WebConfig:SessionTimeout"];
                //
                if (string.IsNullOrEmpty(sTimeout))
                    SessionTimeout = 120;//sec
                if (int.TryParse(sTimeout, out int value))
                    SessionTimeout = value;
                else
                    SessionTimeout = 120;//sec
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
