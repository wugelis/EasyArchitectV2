using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2VSIXProject.ClassesDef
{
    /// <summary>
    /// 取得 DbContext 類別敘述定義
    /// </summary>
    public class DbContextDef
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GetEasyArchitectV2InfrastructureManager
        {
            get => @"using $(NAMESPACE_DEF)$.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $(NAMESPACE_DEF)$.RentalCars
{
    /// <summary>
    /// EasyArchiect V2 的 Infrastructure 中間層管理者物件
    /// </summary>
    public class EasyArchitectV2InfrastructureManager
    {
        private readonly RequestDelegate _next;
        private readonly IServiceCollection _services;

        public EasyArchitectV2InfrastructureManager(RequestDelegate next, IServiceCollection services)
        {
            _next = next;
            _services = services;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    context.RequestServices.GetRequiredService<IConfiguration>().GetConnectionString(""OutsideDbContext""),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // 處理 Request 之前的邏輯
            await _next(context);
            // 處理 Request 之後的邏輯
        }
    }
}
";
        }
        /// <summary>
        /// 
        /// </summary>
        public static string GetClassTemplate
        {
            get => @"using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;$(OTHER_NAMESPACE)$
using Microsoft.EntityFrameworkCore.Metadata;

namespace $(NAMESPACE_DEF)$.Persistance
{
    public partial class $(ENTITIES_DEF)$ : DbContext $(MARK_CODE)$ //, IApplicationDbContext
    {
        public $(ENTITIES_DEF)$()
        {
        }

        public $(ENTITIES_DEF)$(DbContextOptions<$(ENTITIES_DEF)$> options)
            : base(options)
        {
        }
		
		$(DB_SET_DEF)$
		
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(""Server=(local)\\MSSQLSERVER2017;Database=[yourDBName];Trusted_Connection=True;"");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public Task<int> SaveChangesAsync(bool cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

";
        }
    }
}
