using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TakChat.Data;

namespace TakChat.Service
{
    public class TakHub : Hub
    {
        public const string HubUrl = "/chat";

        public async Task SendMessageRoom(MessageDetail message)
        {
            await Clients.Group(message.Recipient.ToLower()).SendAsync("SendMessageRoom", message.UserName, message.Message, message.Recipient, "");
        }

        public async Task SendUserRegisterAsync(MessageDetail message)
        {
            await Clients.OthersInGroup(message.Recipient.ToLower()).SendAsync("SendUserRegisterAsync", message.UserName, message.Message, message.Recipient, "");
        }

        public async Task SendDirectMessage(MessageDetail message)
        {
            await Clients.User(message.Recipient.ToLower()).SendAsync("SendDirectMessage", message.UserName, message.Message, message.Recipient, "");
        }

        public async Task SendChangeRoom(string roomDisconnect, string roomConnect)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomDisconnect);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomConnect);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            Groups.AddToGroupAsync(Context.ConnectionId, "principal");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}

