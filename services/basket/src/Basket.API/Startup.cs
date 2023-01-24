using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;

namespace Basket.API
{
    public class Startup
    {
        private const string RedisConnectionStringEnv = "CacheSettings:ConnectionString";
        private const string DiscountGrpcUrlEnv = "GrpcSettings:DiscountUrl";

        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = this.configuration.GetValue<string>(RedisConnectionStringEnv);
            });

            var grpcDiscountUrl = this.configuration["GrpcSettings:DiscountUrl"];

            if (string.IsNullOrEmpty(grpcDiscountUrl))
            {
                // TODO: Validates with a regex http pattern.
                throw new InvalidOperationException($"{nameof(DiscountGrpcUrlEnv)} cannot be null or empty.");
            }

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options => options.Address = new Uri(grpcDiscountUrl));
            services.AddScoped<DiscountGrpcService>();


            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
