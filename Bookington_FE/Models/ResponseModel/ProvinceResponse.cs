namespace Bookington_FE.Models.ResponseModel
{

    public class ProvinceResponse
    {
        public List<ProvinceInfo> result { get; set; } = new List<ProvinceInfo>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }
    public class ProvinceInfo
    {
        public string id { get; set; } = "0";
        public string provinceName { get; set; } = string.Empty;
    }
}
