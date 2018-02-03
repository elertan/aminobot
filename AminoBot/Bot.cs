using System;
using System.Net.Http;
using System.Threading.Tasks;
using AminoApi;
using AminoApi.Models.Chat;

namespace AminoBot
{
    public class Bot
    {
        private TimeSpan ChatCheckDelay { get; set; } = TimeSpan.FromSeconds(10);
        private readonly AminoApi.IApi _aminoApi;
        private readonly CleverBotApi.IApi _cleverBotApi;
        
        public Bot()
        {
            _aminoApi = new Api(new HttpClient());
            _cleverBotApi = new CleverBotApi.Api();
        }
        
        public async Task Run()
        {
            await Login();

            var joinedCommunitiesResult = await _aminoApi.GetJoinedCommunitiesAsync();
            
            while (true)
            {
                foreach (var community in joinedCommunitiesResult.Data.Communities)
                {
                    Console.WriteLine($"Doing check for {community.Name}");
                    await Task.Delay(500);
                    
                    var threadCheckResult = await _aminoApi.ThreadCheckAsync(community.Id);
                    foreach (var threadCheck in threadCheckResult.Data.ThreadChecks)
                    {
                        if (!threadCheck.HasReceivedNewMessage) continue;

                        Console.WriteLine($"Replying to thread {threadCheck.ThreadId}");
                        // Chat has new message
                        await _aminoApi.SendMessageToChatAsync(community.Id, threadCheck.ThreadId, "Pong!");
                    }
                }

                Console.WriteLine($"Waiting for delay\n\n");
                await Task.Delay(ChatCheckDelay);
            }
        }

        private async Task Login()
        {
            while (true)
            {
                Console.Write("Enter username: ");
                var email = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Enter password: ");
                var password = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Logging in...");
                var loginResult = await _aminoApi.LoginAsync(email, password);
                if (loginResult.DidSucceed())
                {
                    _aminoApi.Sid = loginResult.Data.Sid;

                    Console.WriteLine($"Welcome back, {loginResult.Data.Nickname}!");
                    await Task.Delay(2500);
                    Console.Clear();
                    break;
                }

                Console.Clear();
                Console.WriteLine("Error:\n" + loginResult.Info.Message);
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}