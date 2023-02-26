using Orders.Application.Models;

namespace Orders.Application.Abstractions
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Email email);
    }
}
