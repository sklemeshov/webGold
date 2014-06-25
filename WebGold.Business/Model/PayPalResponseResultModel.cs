namespace webGold.Business.Model
{
    public class PayPalResponseResultModel
    {
        public bool IsSucces { get; set; }
        public string Url { get; set; }
        public string Errors { get; set; }
    }
}