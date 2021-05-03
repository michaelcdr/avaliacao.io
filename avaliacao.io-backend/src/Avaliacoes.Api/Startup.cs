using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Dominio.Transacoes;
using Avaliacoes.Infra.Data;
using Avaliacoes.Infra.Repositorios.EF;
using Avaliacoes.Infra.Transacoes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Avaliacoes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            
            string connStr = Configuration.GetConnectionString("ContextConnection");

            //services.AddDbContext<ApplicationDbContext>(opt =>
            //{
            //    opt.UseSqlServer(connStr);
            //});

            services.AddDbContextPool<ApplicationDbContext>(opt =>
            {
                opt.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Avaliacoes.Api", Version = "v1" });
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDisciplinasRepositorio, DisciplinasRepositorio>();
            services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avaliacoes.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true) // allow any origin
             .AllowCredentials()); // allow credentials

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
