using Demo.Webapi.BLayer;
using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptPaymentDetailController : BaseController<receipt_payment_detail>
    {
        private IReceiptPaymentDetailBL _receiptPaymentDetailBL;

        public ReceiptPaymentDetailController(IBaseBL<receipt_payment_detail> baseBL,IReceiptPaymentDetailBL receiptPaymentDetailBL) : base(baseBL)
        {
            _receiptPaymentDetailBL = receiptPaymentDetailBL;
        }

        /// <summary>
        /// Hàm lấy danh sách detail theo id chứng từ
        /// </summary>
        /// <param name="re_id">id chứng từ</param>
        /// <author>Xuân Đào 25/04/2023</author>
        /// <returns></returns>
        [HttpGet("GetAllByReId")]
        public IActionResult GetAllByReId([FromQuery] Guid re_id)
        {
            var result = _receiptPaymentDetailBL.GetAllRPDByReId(re_id);
            if (result != null)
            {
                return StatusCode(200, result);
            } else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Hàm cập nhật chi tiết chứng từ theo id
        /// </summary>
        /// <param name="re_id">id chứng từ</param>
        /// <param name="records">Danh sách detail</param>
        /// <author>Xuân Đào 25/04/2023</author>
        /// <returns></returns>
        [HttpPut("UpdateMultiple")]
        public IActionResult UpdateMultiple([FromQuery] Guid re_id, [FromBody]IEnumerable<receipt_payment_detail> records)
        {
            var result = _receiptPaymentDetailBL.UpdateMultiple(re_id, records);
            if (result != null) { 
                return StatusCode(200, new ServiceResult
                {
                    IsSuccess= true,
                    Message=Resource.updateSuccess,
                    Data=result,
                });
            } else
            {
                return NotFound();
            }
        }

        //[HttpPost("BulkCreate")]
        //public IActionResult BulkCreate([FromBody] IEnumerable<ReceiptPaymentDetail> data)
        //{
        //    int result = _receiptPaymentDetailBL.BulkInsert(data);
        //    if (result != 0)
        //    {
        //        return StatusCode(201, new ServiceResult
        //        {
        //            IsSuccess = true,
        //            Message = Resource.createSuccess,
        //            Data = result,
        //        });
        //    }
        //    else
        //    {
        //        _exceptionResult.TraceId = HttpContext.TraceIdentifier;
        //        return StatusCode(400, _exceptionResult);
        //    }
        //}
    }
}
