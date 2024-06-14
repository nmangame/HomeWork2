using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Services
{
    public interface IRoleService
    {
        Task IsExits(List<Role> roles);
    }
}
