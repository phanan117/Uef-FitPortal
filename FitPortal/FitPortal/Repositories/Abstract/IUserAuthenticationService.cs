using FitPortal.Areas.Admin.Models.DTO;
using FitPortal.Models.DTO;

namespace FitPortal.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
   
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
        Task<Status> RegisterAdminAsync(AdminAccountRegistraion model);
        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
