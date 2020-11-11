using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using TwitchRerunKiller.Utils;
using static TwitchRerunKiller.Utils.PrettyLog;
using QuickType;
namespace TwitchRerunKiller
{
    class Program
    {

        public static List<Bot> BotManager = new List<Bot>();
        static void Main(string[] args)
        {
            Console.Title = "TwitchRerun";

            var userData = UserData.FromJson(File.ReadAllText("Users.json"));
            
            var channelsData = ChannelsData.FromJson(File.ReadAllText("Channels.json"));

            foreach (UserData user in userData)
            {
                foreach (string idx in channelsData.Channels)
                {
                    Bot bot = new Bot(user.Username, user.Token, idx);
                    BotManager.Add(bot);
                }
            }


            new Thread(delegate ()
            {
                while (true)
                {
                    var commandArgs = Console.ReadLine()?.Split(' ').ToList();

                    if (commandArgs == null) continue;

                    var commandRoot = commandArgs[0];
                    commandArgs.RemoveAt(0);

                    switch (commandRoot)
                    {
                        case "message":
                        case "msg":
                            if (commandArgs.Count > 1)
                            {
                                string msgConcat = "";
                                for(int idx = 0; idx < commandArgs.Count; idx++)
                                {
                                    msgConcat = msgConcat + commandArgs[idx] + " ";
                                }
                                
                                SendMessageTo(commandArgs[0], msgConcat);
                            }
                            else
                            {
                                SendMessageToAll(commandArgs[0]);
                            }
                            break;
                        case "clear":
                        case "cls":
                            Console.Clear();
                            break;
                        default:
                            LogError($"No command matching '{commandRoot}', please enter a valid command.");
                            break;
                    }
                }
            }).Start();
        }
    
        public static void SendMessageToAll(string message)
        {
            foreach(Bot index in BotManager)
            {
                index.SendMessage(message);
            }
        }

        public static void SendMessageTo(string Channel, string message)
        {
            foreach (Bot index in BotManager)
            {
                string Ch = index.GetActChannel();
                if (Ch.ToLower() == Channel.ToLower())
                {
                    index.SendMessageTo(Ch, message);
                }
            }
        }
    }
}
