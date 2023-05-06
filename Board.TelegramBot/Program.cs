using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using System.Net;
using System.Text.RegularExpressions;
using Board.Application.AppData.Contexts.TelegramClient;
using Doska.AppServices.Services.User;

public class Program
{
    private static readonly ITelegramClientService _service;
    private static readonly IUserService _userService;

    static Program()
    {
        
    }

    static ITelegramBotClient bot = new TelegramBotClient("5864169679:AAFQnVPvqGV4ngDRebIo1jS_3AHGBEZQvv4");
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message.Text.ToLower().Contains("/login"))
            {
                var text = message.Text.Replace("/login","");
                var mail = text?.Split(';')[0];
                var pass = text?.Split(';')[1];

                
            }
            //await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }


    static void Main(string[] args)
    {
        Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}