using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<bool> RegisterUserAsync(string username, string password);
        Task<string> AuthenticateAsync(string username, string password);
    }
}
