using System;
using System.Net.Http;
using System.Threading.Tasks;
using AminoApi;

namespace AminoBot
{
    public class Bot
    {
        private IApi _api;
        
        public Bot()
        {
            _api = new Api(new HttpClient());
        }
        
        public async Task Run()
        {
            await Login();
            
            
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
                var loginResult = await _api.LoginAsync(email, password);
                if (loginResult.DidSucceed())
                {
                    _api.Sid = loginResult.Data.Sid;
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