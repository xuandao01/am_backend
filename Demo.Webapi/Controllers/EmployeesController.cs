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

        [HttpGet("NewEmployeeCode")]
        public string GetNewEmployeeCode()
        {
            return _employeeBL.GetNewEmployeeCode();
        }

        [HttpDelete("DeleteMultiple")]
        public IActionResult DeleteMultiple([FromBody] string idList)
        {
            try
            {
                int result = _employeeBL.DeleteMultipleRecords(idList);
                return StatusCode(200, new ServiceResult()
                {
                    IsSuccess = true,
                    Message = "Xóa thành công",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResult()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = new ErrorResult()
                    {
                        ErrorCode = ErrorCode.Exception,
                        DevMsg = Resource.devMsg_Exception,
                        UserMsg = Resource.userMsg_Exception,
                        TraceId = HttpContext.TraceIdentifier
                    }
                });
            }
        }

    }
}
