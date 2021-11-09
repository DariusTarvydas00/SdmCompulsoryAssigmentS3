using System;
using System.IO;
using Core.IServices;
using Domain.IRepositories;
using Domain.Services;
using Infrastructure.Static.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SdmCompulsoryAssigmentS3
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IBEReviewRepository,BEReviewRepository>();
            serviceCollection.AddScoped<IBEReviewService, BEReviewService>();
            serviceCollection.AddScoped<TextReader>(_ => new StreamReader(@"../../../../Infrastructure.Static.Data/ratings.json"));
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var reviewService = serviceProvider.GetRequiredService<IBEReviewService>();

            foreach (var movie in reviewService.GetMostProductiveReviewers())
            {
                Console.WriteLine(movie);
            }
        }
    }
}