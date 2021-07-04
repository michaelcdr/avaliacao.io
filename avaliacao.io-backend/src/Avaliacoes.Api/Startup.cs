using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Dominio.Transacoes;
using Avaliacoes.Infra.Data;
using Avaliacoes.Infra.Repositorios.EF;
using Avaliacoes.Infra.Transacoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Avaliacoes.Api", Version = "v1", Description = "Documenta��o da API" });

                var def = new OpenApiSecurityScheme()
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Autentica��o Bearer via JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(def.Reference.Id, def);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        { 
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("api-avaliacoes-security-key")),
                ClockSkew = TimeSpan.FromMinutes(5),
                ValidIssuer = "Avaliacoes.Api",
                ValidAudience = "Postman"
            };

            services.AddAuthentication(options => {
                //options.DefaultScheme = "JwtBearer";
                //options.DefaultChallengeScheme = "JwtBearer";
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("JwtBearer", options => { 
                options.TokenValidationParameters = tokenValidationParameters; 
            });

            //n�o muda o nome do fucking cookie
            //.AddCookie(opt =>
            // {
            //     opt.Cookie.Name = "Teste Identity";
            //     opt.Cookie.Expiration = TimeSpan.FromMinutes(0.4);
            // });

            // defini��es de regras de seguran�a para a password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = false;
                options.Stores.MaxLengthForKeys = 85;
            });  

            services.AddIdentity<Usuario, TipoUsuario>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddRoleManager<RoleManager<TipoUsuario>>()
              .AddDefaultTokenProviders();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDisciplinasRepositorio, DisciplinasRepositorio>();
            services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();
            services.AddTransient<ICompetenciasRepositorio, CompetenciasRepositorio>();
            services.AddTransient<IHabilidadesRepositorio, HabilidadesRepositorio>();
            services.AddTransient<IDimensoesRepositorio, DimensoesRepositorio>();
            services.AddTransient<IUsuarioService, UsuarioService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            UserManager<Usuario> userManager,
            RoleManager<TipoUsuario> roleManager, IUsuarioService usuarioService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true)            
             .AllowCredentials());                          

            app.UseAuthentication();
            app.UseAuthorization();

            var seedService = new SeedService(userManager, roleManager, usuarioService);
            seedService.Seed().GetAwaiter().GetResult();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
