
using Telegram.Bot;

namespace CliperBot
{
    public class TelegramBotWrapper
    {
        private readonly TelegramBotClient _botClient;
        private readonly CancellationTokenSource _cts;

        public TelegramBotWrapper()
        {
            var token = Environment.GetEnvironmentVariable("TOKEN") ?? "";
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException("TOKEN is missing, call the developer!");
            }

            _cts = new CancellationTokenSource();
            _botClient = new TelegramBotClient(token, cancellationToken: _cts.Token);
        }

        public async Task Start()
        {
            Console.WriteLine("Starting bot..");
            var me = await _botClient.GetMe();
            Console.WriteLine($"{me.Username} is running!");
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }
    }
}