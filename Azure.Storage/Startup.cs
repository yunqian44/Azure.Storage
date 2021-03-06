using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Storage.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Azure.Storage.File;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using AzureStorage = Microsoft.Azure.Storage;

namespace Azure.Storage
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
            services.AddControllersWithViews();

            services.AddSingleton(x => new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));

            services.AddSingleton(x => new CloudStorageAccount(new StorageCredentials("cnbateblogaccount", "FU01h022mn1JjONp+ta0DAXOO7ThK3diYhd4xrm0Hpg891n9nycsTLGZF83nJpGvTIZvO5VCVFhGOfV0wndOOQ=="), true));

            //
            services.AddSingleton(x => new Microsoft.Azure.Cosmos.Table.CloudStorageAccount(new Microsoft.Azure.Cosmos.Table.StorageCredentials("cnbateblogaccount", "FU01h022mn1JjONp+ta0DAXOO7ThK3diYhd4xrm0Hpg891n9nycsTLGZF83nJpGvTIZvO5VCVFhGOfV0wndOOQ=="), true));


            services.AddSingleton(x => new QueueClient("DefaultEndpointsProtocol=https;AccountName=cnbateblogaccount;AccountKey=e2T2gYREFdxkYIJocvC4Wut7khxMWJCbQBp8tPM2EJt37QaUUlflTPAlkoJzIlY29aGYt8WW0xx1bckO4hLKJA==;EndpointSuffix=core.windows.net", "blogmessage"));


            services.AddSingleton(x => new AzureStorage.CloudStorageAccount(new AzureStorage.Auth.StorageCredentials("cnbateblogaccount", "e2T2gYREFdxkYIJocvC4Wut7khxMWJCbQBp8tPM2EJt37QaUUlflTPAlkoJzIlY29aGYt8WW0xx1bckO4hLKJA=="),true));


            //services.AddSingleton(x => new CloudFileClient(new AzureStorage.StorageUri(new Uri("DefaultEndpointsProtocol=https;AccountName=cnbateblogaccount;AccountKey=e2T2gYREFdxkYIJocvC4Wut7khxMWJCbQBp8tPM2EJt37QaUUlflTPAlkoJzIlY29aGYt8WW0xx1bckO4hLKJA==;EndpointSuffix=core.windows.net")),new AzureStorage.Auth.StorageCredentials()));

            services.AddSingleton<IBlobSergvice, BlobService>();
            services.AddSingleton<ITableService, TableService>();
            services.AddSingleton<ITableServiceV2, TableServiceV2>();
            services.AddSingleton<IQueueService, QueueService>();
            services.AddSingleton<IFileService, FileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
