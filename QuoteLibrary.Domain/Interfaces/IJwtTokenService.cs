﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteLibrary.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string username, string role, int id);

    }
}
