using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Application.Interfaces
{
    public interface ITokenService
    {
        public string? GetUserId();
        public string? GetUserRole();
    }
}
