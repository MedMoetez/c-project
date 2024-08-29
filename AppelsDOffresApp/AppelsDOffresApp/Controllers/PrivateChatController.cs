using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using AppelsDOffresApp.Models;
using AppelsDOffresApp.Hubs;
using AppelsDOffresApp.ViewModels;
using Microsoft.AspNetCore.Http;

namespace AppelsDOffresApp.Controllers
{
    public class PrivateChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<Utilisateur> _userManager;

        public PrivateChatController(AppDbContext context, IHubContext<ChatHub> hubContext, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _context.Utilisateurs.ToList(); 
            return View(users);
        }

        public IActionResult PrivateChat(string toUserId)
        {
            var user = _context.Users.Find(toUserId);
            if (user == null)
            {
                return NotFound();
            }

            var currentUserId = User.GetUserId(); // Now should work fine

            var viewModel = new PrivateChatViewModel
            {
                UserId = toUserId,
                UserName = user.UserName,
                Messages = _context.ChatMessages
                                     .Where(m => (m.FromUserId == currentUserId && m.ToUserId == toUserId) ||
                                                 (m.FromUserId == toUserId && m.ToUserId == currentUserId))
                                     .OrderBy(m => m.When)
                                     .ToList()
            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult SendPrivateMessage(string toUserId, string messageText)
        {
            var fromUserId = _userManager.GetUserId(User); // Get the current user's ID
            var fromUserName = User.Identity.Name; // Get the current user's name

            var message = new ChatMessage
            {
                FromUserId = fromUserId,
                ToUserId = toUserId, // Set the recipient's ID
                UserName = fromUserName,
                Text = messageText,
                When = DateTime.Now,
                IsPrivate = true
            };

            _context.ChatMessages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("PrivateChat", new { toUserId = toUserId });
        }

    }
}
