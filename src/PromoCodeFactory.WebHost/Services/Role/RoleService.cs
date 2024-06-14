using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Services
{
    public class RoleService : IRoleService
    {
        protected readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> repository)
        {
            _roleRepository = repository;
        }

        /// <summary>
        /// Проверяем список ролей
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task IsExits(List<Role> roles)
        {
            if (roles == null || roles.Count == 0) return;

            foreach (var role in roles)
            {
                await _roleRepository.IsExits(role.Id);
            }
        }
    }
}
