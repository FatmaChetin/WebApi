using BankApiEFCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankApiEFCore.Models.Context
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        public DbSet<CartInfo> CartInfos { get; set; }
    }
}
