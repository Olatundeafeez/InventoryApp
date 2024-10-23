using InventoryAPI.Helper;
using InventoryAPI.Model.DTOs.Mail;

namespace InventoryAPI.Service
{
    public interface IEmailService
    {
        Task<ResponseModel<string>> SendEmail(MailRequest mail);
    }
}
