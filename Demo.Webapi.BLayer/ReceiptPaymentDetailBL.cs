using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Demo.Webapi.DL;
using Demo.Webapi.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.BLayer
{
    public class ReceiptPaymentDetailBL : BaseBL<receipt_payment_detail>, IReceiptPaymentDetailBL
    {
        private IReceiptPaymentDetailDL _receiptPaymentDetailDL;

        public ReceiptPaymentDetailBL(IReceiptPaymentDetailDL receiptPaymentDetailDL) : base(receiptPaymentDetailDL)
        {
            this._receiptPaymentDetailDL = receiptPaymentDetailDL;
        }

        public ServiceResult GetAllRPDByReId(Guid re_id)
        {
            return _receiptPaymentDetailDL.GetAllRPDByReId(re_id);
        }

        public int BulkInsert(IEnumerable<receipt_payment_detail> ReceiptPaymentDetails)
        {
            return _receiptPaymentDetailDL.BulkInsert(ReceiptPaymentDetails);
        }
    }
}
