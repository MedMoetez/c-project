using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using AppelsDOffresApp.Models;
using AppelsDOffresApp.Hubs;
using System.Security.Claims;


namespace AppelsDOffresApp.Controllers
{
    public class PublicChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<Utilisateur> _userManager;

        public PublicChatController(AppDbContext context, IHubContext<ChatHub> hubContext, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var messages = _context.ChatMessages.Where(m => !m.IsPrivate).ToList();
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string messageText)
        {
            if (string.IsNullOrEmpty(messageText))
            {
                return BadRequest("Message cannot be empty.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var userName = User.Identity.Name; // Get the current user's name

            var message = new ChatMessage
            {
                FromUserId = userId,
                UserName = userName,
                Text = messageText,
                When = DateTime.Now,
                IsPrivate = false
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            // Broadcast message to connected clients (SignalR)
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);

            // Redirect back to the chat page or return a view if not using AJAX
            return RedirectToAction("Index");
        }


        public IActionResult SendPublicMessage(string messageText)
        {
            var userId = _userManager.GetUserId(User); // Get the current user's ID
            var userName = User.Identity.Name; // Get the current user's name

            var message = new ChatMessage
            {
                FromUserId = userId,
                Text = messageText,
                When = DateTime.Now,
                IsPrivate = false,
                ToUserId = null
            };

            _context.ChatMessages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
