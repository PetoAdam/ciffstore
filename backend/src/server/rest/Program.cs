﻿using System.Collections.Generic;
using System.Security.Claims;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PhotoSauce.MagicScaler;
using PhotoSauce.NativeCodecs.Giflib;

namespace CaffStore.REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
                        

            CodecManager.Configure(codecs => {
                codecs.UseGiflib();
            });

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("/home/ubuntu/caffstore-secret/caff-store-firebase-adminsdk-lu9y2-53f4bdc1f6.json")
            });

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
