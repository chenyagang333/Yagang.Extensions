using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yagang.EntityFrameworkCore.DbContexts;

namespace Yagang.EntityFrameworkCore
{
    public class Class1 : BaseDbContext<int>
    {
        public Class1(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
        }

        public override BaseDbContext<int> UpdateBaseEntity()
        {
            return base.UpdateBaseEntity();
        }
    }
}
