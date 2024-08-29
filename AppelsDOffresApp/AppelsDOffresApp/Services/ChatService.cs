/*using AppelsDOffresApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChatService : IChatService
{
    private readonly AppDbContext _context;

    public ChatService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChatMessage>> GetMessagesAsync()
    {
        return await _context.ChatMessages.OrderBy(m => m.When).Take(50).ToListAsync();
    }

    public async Task SaveMessageAsync(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();
    }
}*/
