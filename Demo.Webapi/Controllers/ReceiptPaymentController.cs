using Demo.Webapi.BLayer;
using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptPaymentController : BaseController<receipt_payment>
    {
        private IReceiptPaymentBL _receiptPaymentBL;
        public ReceiptPaymentController(IBaseBL<receipt_payment> baseBL, IReceiptPaymentBL receiptPaymentBL) : base(baseBL)
        {
            _receiptPaymentBL = receiptPaymentBL;
        }

        [HttpGet("GetNewCode")]
        public IActionResult GetNewCode()
        {
            string newCode = _receiptPaymentBL.GetNewPaymentCode();
            if (newCode != null)
            {
                return StatusCode(200, new ServiceResult
                {
                    IsSuccess = true,
                    Data = newCode,
                });
            }
            else
            {
                return StatusCode(400, new ServiceResult { IsSuccess = false });
            }
        }
    }
}
