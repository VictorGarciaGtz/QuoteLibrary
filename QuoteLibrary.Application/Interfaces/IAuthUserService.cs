using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface IAuthUserService
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
