using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class Web_StoreContext(DbContextOptions<Web_StoreContext> options) : IdentityDbContext<Web_Store.Data.ApplicationUser>(options)
{
}
