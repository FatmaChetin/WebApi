using BankApiEFCore.Models.Context;
using BankApiEFCore.Models.Entities;
using BankApiEFCore.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace BankApiEFCore.Controllers
{
    [ApiController]// bir api controller olduğunu belirtmelisin o yüzden bu attiributeu kullanıyoruz.
    [Route("api/[controller]")] // buda apinin routenu belirten attribute dir. bunun dışında [Route("[controller]")] vardır.istediğin bir tanesini kullanabilirsin.
    public class PaymentController : ControllerBase
    {
        MyContext _db;
        public PaymentController(MyContext db)
        {
            _db = db;
        }
        // ef core da setintiliazer yok bundan dolayı bit httpget işlemi oluşturdum ve içerisinde cartinfosun instancenı alarak bir veri yarattım ki test edilebilirliğini kontrol edebileyim. Bogus kütüphanesi kurulamadığı için mantıklı yöntem bu gelmiştir.
      
        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            List<CartInfo> cartInfos = new List<CartInfo> { new CartInfo { ID=1, CardUserName="Ebru Şahin", CardExpiryMonth=3,CardExpiryYear=2026,SecurityNumber="123",Balance=50000, CardNumber="1111 1111 1111 1111"} };
            return Ok(cartInfos);
        }

        [HttpPost] //[HttpPost (Name="action adı")] buda bizin url kısmını yazarken çok daha rahat bir şekilde yapmamızı sağlar. ve aynı zamanda aynı action ismine sahip işlemleri birbirinden ayırıp route doğru gösterilmesine yardımcı olur
        public async Task<ActionResult<CartInfo>> ReceivePayment(PaymentRequestModel item)
        {
            CartInfo ci = _db.CartInfos.FirstOrDefault(x=> x.CardNumber==item.CardNumber && x.SecurityNumber==item.SecurityNumber && x.CardUserName==item.CardUserName && x.CardExpiryYear==item.CardExpiryYear && x.CardExpiryMonth==item.CardExpiryMonth);
            if (ci!=null) 
            {
                if (ci.CardExpiryYear<DateTime.Now.Year)
                {
                    return BadRequest("Expired Card");// burada bad request dönüyoruz amacımız yıl olarak süresi dolmuş.
                }
                else if (ci.CardExpiryMonth<DateTime.Now.Month)
                {
                    return BadRequest("Expired Card(month)");
                }

                // yukarıda ccardın sürelerine göre inceledik ki bir problem varsa direkman badrequest dönsün diye.
               
                if (ci.Balance>= item.ShoppingPrice)
                {
                    SetBalance(item,ci);
                    return Ok();// 200 karşılaşak en iyi sonuç budur işlemin başarılı bir şekilde gerçekleştiğini gösterir.
                }

                return BadRequest("Balance exceeded");// yetersiz bakiye

            }
            return BadRequest("Card info Wrong");// cart bilgileri hatalıdır
        }
        private void SetBalance(PaymentRequestModel item, CartInfo ci)
        {
            ci.Balance-= item.ShoppingPrice; // cartın limitinden alışveriş ücretini düşürdük
            _db.SaveChangesAsync();
        }
    }
}
