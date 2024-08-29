using AppelsDOffresApp.Models;
using System.Collections.Generic;

namespace AppelsDOffresApp.ViewModels
{
    public class PrivateChatViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<ChatMessage> Messages { get; set; }
    }
}
