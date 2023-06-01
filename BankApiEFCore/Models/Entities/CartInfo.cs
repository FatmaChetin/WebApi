using System.ComponentModel.DataAnnotations.Schema;

namespace BankApiEFCore.Models.Entities
{
    public class CartInfo:BaseEntity
    {
        public string CardUserName { get; set; }
        public string SecurityNumber { get; set; }
        public string CardNumber { get; set; }
        
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        [Column("money")]
        public decimal Limit { get; set; }
        [Column("money")]//decimal türünü sql için burada moneye çevirdim.
        public decimal Balance { get; set; }
    }
}
