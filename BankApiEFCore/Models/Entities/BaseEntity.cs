using BankApiEFCore.Models.Enums;
using System.Reflection.Metadata.Ecma335;

namespace BankApiEFCore.Models.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus DataStatus { get; set; }
        public BaseEntity()
        {
            DataStatus = DataStatus.Inserted;
            CreatedDate=DateTime.Now;
        }
    }
}
