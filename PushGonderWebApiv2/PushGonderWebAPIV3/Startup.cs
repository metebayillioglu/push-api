using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Push.Business.Abstract;
using Push.Business.Concreate;
using Push.Core.Crypto;
using Push.DataAccess.Abstract;
using Push.DataAccess.Concreate;

namespace PushGonderWebAPIV3
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IDeviceRecordingTempService, DeviceRecordingsTempmanager>();
            services.AddSingleton<IDeviceRecordingsService, DeviceRecordingsManager>();
            services.AddSingleton<ICryptyoService, CryptoManager>();
            services.AddSingleton<IAnnouncementService, AnnouncementsManager>();
            services.AddSingleton<IUsersService, UsersManager>();
            services.AddSingleton<ILoginTempService, LoginTempManager>();
            services.AddSingleton<ISendPushService, SendPushManager>();
            services.AddSingleton<IUserKeysService, UserKeysManager>();

            services.AddSingleton<IDeviceRecordingsDal, MongoDeviceRecordingsDal>();
            services.AddSingleton<IDeviceRecordingsTempDal, MongoDeviceRecordingsTempDal>();
            services.AddSingleton<IAnnouncementsDal, MongoAnnouncementsDal>();
            services.AddSingleton<IUsersDal, MongoUsersDal>();
            services.AddSingleton<ILoginTempDal, MongoLogingsTemp>();
            services.AddSingleton<IUserKeysDal, MongoUserKeysDal>();

            services.AddSingleton<IMailService, MailManager>();
            services.AddSingleton<IRabbitMqService, RabbitMqManager>();
            services.AddSingleton<INotesService, NotesManager>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //  app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
