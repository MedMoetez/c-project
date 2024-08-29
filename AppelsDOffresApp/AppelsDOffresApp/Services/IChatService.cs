using AppelsDOffresApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IChatService
{
    Task<List<ChatMessage>> GetMessagesAsync();
    Task SaveMessageAsync(ChatMessage message);
}
