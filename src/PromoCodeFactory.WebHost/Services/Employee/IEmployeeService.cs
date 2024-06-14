using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeShortResponse>> GetEmployeesAsync();

        Task<EmployeeResponse> GetEmployeeByIdAsync(Guid id);

        Task Delete(Guid id);

        Task Update(Employee viewModel);

        Task Create(Employee viewModel);

        Task<IEnumerable<Employee>> GetEntity();
    }
}
