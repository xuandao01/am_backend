using Dapper;
using Demo.Webapi.BLayer;
using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entites.DTO;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Demo.Webapi.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Demo.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        private IEmployeeBL _employeeBL;
        public EmployeesController(IBaseBL<Employee> baseBL, IEmployeeBL employeeBL) : base(baseBL)
        {
            _employeeBL = employeeBL;
        }
    }
}
