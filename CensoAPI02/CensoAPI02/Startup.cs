using CENSO.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CensoAPI02
{
    public class Startup
    {

        /*Hcer que el proyecto reciba solicitudes y no tengamos el problema de Cors
         el Cors es la seguridad que tienene los navegadores para que no podamos hacer solicitudes*/
        readonly string CorsConfiguration = "_corsConfiguration";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Leemos la llave secreta del appsetting.json
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            // Agregamos el servicio de autenticación a utilizar
            services.AddAuthentication(x =>
            {
                // Definimos el metdodo de autenticacion por JWT
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrador", pol => pol.RequireClaim("Role", new string[] {"1"}));
                options.AddPolicy("SURH", pol => pol.RequireClaim("Role", new string[] { "1", "2" }));
                options.AddPolicy("StaffRH", pol => pol.RequireClaim("Role", new string[] { "1", "2", "3" }));
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CensoAPI02", Version = "v1" });
            });

            services.AddDbContext<CDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CensoLocal")));

            //Configuración del Cors
            services.AddCors(options => {
                options.AddPolicy(name: CorsConfiguration,
                    //agregamos un politica
                    builder =>
                    {
                        //establecemos las opciones de que es lo que vamos a permitir
                        //builder.WithOrigins("http://localhost:4200");
                        /*establecemos que solo permitira peticiones provenientes de este origen (en caso de querer aceptar las peticiones de todos lados
                         solo ponemos un asterisco "*")*/

                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CensoAPI02 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Agregamos de configuracion de Cors
            app.UseCors(CorsConfiguration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
