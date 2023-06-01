namespace BankApiEFCore.ResponseModel
{
    public class PaymentResponseModel
    {
        public string CardUserName { get; set; }
        public string SecurityNumber { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CartExpiryYear { get; set; }
    }
}
