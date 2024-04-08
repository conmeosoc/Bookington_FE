namespace Bookington_FE.Models.ResponseModel
{
    public class DistrictResponse
    {
        public List<DistrictModel> result { get; set; } = new List<DistrictModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }
    public class DistrictModel
    {
        public string id { get; set; } = "0";
        public string districtName { get; set; } = string.Empty;
    }
}
