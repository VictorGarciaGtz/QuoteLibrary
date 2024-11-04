using QuoteLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Interfaces
{
    public interface IAuthUserRepository
    {
        Task<Users?> ValidateUserAsync(string username, string password);

    }
}
