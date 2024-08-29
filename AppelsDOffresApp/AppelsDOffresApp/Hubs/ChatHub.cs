using Microsoft.AspNetCore.SignalR;
using AppelsDOffresApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AppelsDOffresApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<Utilisateur> _userManager;

        public ChatHub(UserManager<Utilisateur> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendPublicMessage(ChatMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendPrivateMessage(string toUserId, ChatMessage message)
        {
            var user = await _userManager.FindByIdAsync(toUserId);
            var connectionId = user?.ConnectionId;

            if (connectionId != null)
            {
                message.IsPrivate = true; // Mark the message as private
                await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", message);
            }
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
                await _userManager.UpdateAsync(user);
            }
            await base.OnConnectedAsync();
        }


    }
}
