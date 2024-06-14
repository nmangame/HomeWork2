using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoCodeFactory.WebHost.Models;
using System.Runtime.CompilerServices;

namespace PromoCodeFactory.WebHost.Services
{

    public class EmployeeService : IEmployeeService
    {
        protected readonly IRepository<Employee> _employeeRepository;
        protected readonly IRoleService _roleService;

        public EmployeeService(IRepository<Employee> employeeRepository, IRoleService roleService) 
        {
            _employeeRepository = employeeRepository;
            _roleService = roleService;
        }

        public async Task Create(Employee viewModel)
        {
            await _roleService.IsExits(viewModel.Roles);

            viewModel.Id = Guid.NewGuid();
            await _employeeRepository.Add(viewModel);
        }

        public async Task Delete(Guid id)
        {
             await _employeeRepository.Delete(id);
        }

        public async Task<EmployeeResponse> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                
                FullName = $"{employee.FirstName} {employee.LastName}",
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        public async Task<IEnumerable<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(employee =>
                new EmployeeShortResponse()
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    FullName = $"{employee.FirstName} {employee.LastName}",
                }).ToList();

            return employeesModelList;
        }

        public async Task Update(Employee viewModel)
        {
            await _roleService.IsExits(viewModel.Roles);

            await _employeeRepository.Update(viewModel);
        }

        public async Task<IEnumerable<Employee>> GetEntity()
        {
            var emploee = await _employeeRepository.GetAllAsync();

            return emploee;
        }
    }
}
