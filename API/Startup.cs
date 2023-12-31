using API.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using Repository;
using API.Services;

namespace API
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
			addScope(services);

			services.AddCors(options => options.AddPolicy("MyPolicy",
				builder => {
					builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
		}));

			services.AddControllers();

			services.AddSwaggerGen(x =>
			{
				//Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				x.IncludeXmlComments(xmlPath);
			});
		}

		private void addScope(IServiceCollection services)
		{
			services.AddSingleton<IConfiguration>(Configuration);


			services.AddDbContext<EFContext>(options =>
				options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"],
				b => b.MigrationsAssembly("API")));

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPersonRepository, PersonRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPersonService, PersonService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
