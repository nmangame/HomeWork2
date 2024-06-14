using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using PromoCodeFactory.WebHost.Services;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IRepository<Employee> employeeRepository, IEmployeeService employeeService)
        {
            //_employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<EmployeeShortResponse>> GetEmployeesAsync()
        {
            return await _employeeService.GetEmployeesAsync();            
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);

                return Ok(employee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Создан для удобства
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetEntity")]
        public async Task<ActionResult<EmployeeResponse>> GetEntity()
        {
            var employees = await _employeeService.GetEntity();

            return Ok(employees);
        }

        /// <summary>
        /// Удаляем сотрудника по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _employeeService.Delete(id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Обновляем данные сотрудника
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Employee viewModel)
        {
            try
            {
                await _employeeService.Update(viewModel);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Добавляем сотрудника
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Employee viewModel)
        {
            try
            {
                await _employeeService.Create(viewModel);

                return Ok();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}