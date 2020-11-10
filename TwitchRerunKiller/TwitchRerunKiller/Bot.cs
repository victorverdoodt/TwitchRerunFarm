using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using static TwitchRerunKiller.Utils.PrettyLog;

namespace TwitchRerunKiller
{
    class Bot
    {
        private static TwitchClient client;
        private string actChannel;
        private static string LastWhisper;
        public Bot(string userName, string token, string channel)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(userName, token);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, channel);

            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnConnected += Client_OnConnected;

            client.Connect();
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            LogSuccess(e.BotUsername + $" connected to {e.AutoJoinChannel}.");
            

        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            actChannel = e.Channel;
        }

        private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username.ToLower() == "StreamElements".ToLower())
            {
                if (LastWhisper != e.WhisperMessage.Message)
                {
                    LogInfo(e.WhisperMessage.Message);
                    LastWhisper = e.WhisperMessage.Message;
                }
            }       
        }


        public void SendMessage(string message)
        {
            client.SendMessage(actChannel, message);
            LogInfo("Channel: " + actChannel + ", Send Message: " + message);
        }

        public void SendMessageTo(string Channel, string message)
        {
            client.SendMessage(Channel, message);
            LogInfo("Channel: " + Channel + ", Send Message: " + message);
        }

        public string GetActChannel()
        {
            return actChannel;
        }
    }
}
