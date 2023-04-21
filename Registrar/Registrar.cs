using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Doska.AppServices.MapProfile;
using Doska.AppServices.IRepository;
using Doska.AppServices.Services.Ad;
using Doska.DataAccess.Repositories;
using Doska.AppServices.Services.Categories;
using Doska.AppServices.Services.User;
using Doska.AppServices.Services.FavoriteAd;
using Doska.AppServices.Services.Message;
using Doska.AppServices.Services.Comment;
using Doska.Infrastructure.Identity;
using Board.Infrastucture.DataAccess.Interfaces;
using Board.Infrastucture.DataAccess;
using Board.Infrastucture.Repository;
using Board.Application.AppData.Contexts.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Abstractions;
using Board.Application.AppData.Contexts.Categories;
using Board.Application.AppData.Contexts.Comment;
using Board.Application.AppData.Contexts.FavoriteAd;
using Board.Application.AppData.Contexts.Message;
using Board.Application.AppData.Contexts.User;
using Board.Application.AppData.Contexts.File;
using Board.Infrastucture.DataAccess.Contexts.File;

namespace Doska.Registrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextOptionsConfigurator<BoardDbContext>, BoardDbContextConfiguration>();

            services.AddDbContext<BoardDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
                ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BoardDbContext>>()
                .Configure((DbContextOptionsBuilder<BoardDbContext>)dbOptions)));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BoardDbContext>()));

            //services.AddAutoMapper(typeof(UserMapProfile), typeof(AdMapProfile),
            //    typeof(CategoryMapProfile), typeof(SubCategoryMapProfile),
            //    typeof(ChatMapProfile), typeof(MessageMapProfile),
            //    typeof(FavoriteAdMapProfile), typeof(CommentMapProfile));

            // Регистрация объявления

            services.AddLogging();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAdService, AdService>();
            services.AddTransient<IAdRepository, AdRepository>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IFavoriteAdService, FavoriteAdService>();
            services.AddTransient<IFavoriteAdRepository, FavoriteAdRepository>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IFileRepository, FileRepository>();

            services.AddTransient<IMailService, MailService>();

            services.AddScoped<IClaimAcessor, HttpContextClaimAcessor>();

            return services;
        }
    }
}
