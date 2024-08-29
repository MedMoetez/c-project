using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class DeadlineNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEmailSender _emailSender;

    public DeadlineNotificationService(IServiceScopeFactory scopeFactory, IEmailSender emailSender)
    {
        _scopeFactory = scopeFactory;
        _emailSender = emailSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckDeadlines();
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Run daily
        }
    }

    private async Task CheckDeadlines()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var threeDaysFromNow = DateTime.Now.AddDays(3);

            var offers = context.AppelsDOffres
                .Where(ao => ao.Date <= threeDaysFromNow && ao.Date > DateTime.Now)
                .ToList();

            foreach (var offer in offers)
            {
                var users = context.Users.ToList();

                foreach (var user in users)
                {
                    var message = $"L'Appel d'Offre {offer.Nom} est proche de sa date limite ({offer.Date:dd/MM/yyyy}).";
                    await _emailSender.SendEmailAsync(user.Email, "Appel d'Offre Deadline Approaching", message);
                }
            }
        }
    }
}
