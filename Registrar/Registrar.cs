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
using Board.Application.AppData.Contexts.Translator;
using Board.Application.AppData.Contexts.Photo.AdPhoto;
using Board.Infrastucture.DataAccess.Contexts.Photo.AdPhoto;
using Board.Infrastucture.DataAccess.Contexts.Photo.UserPhoto;
using Board.Application.AppData.Contexts.Photo.UserPhoto;
using Board.Application.AppData.Contexts.Photo;
using Board.Application.AppData.Contexts.Role;
using Board.Infrastucture.DataAccess.Contexts.Role;
using Board.Application.AppData.Contexts.TelegramClient;
using Board.Infrastucture.DataAccess.Contexts.TelegramClient;

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

            services.AddLogging();

            services.AddMemoryCache();

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

            services.AddTransient<IAdPhotoRepository, AdPhotoRepository>();
            services.AddTransient<IUserPhotoRepository, UserPhotoRepository>();

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IPhotoService, PhotoService>();

            services.AddTransient<IMailService, MailService>();

            services.AddTransient<ITranslatorService, TranslatorService>();

            services.AddTransient<ITelegramClientService, TelegramClientService>();
            services.AddTransient<ITelegramClientRepository, TelegramClientRepository>();

            services.AddScoped<IClaimAcessor, HttpContextClaimAcessor>();

            return services;
        }
    }
}
