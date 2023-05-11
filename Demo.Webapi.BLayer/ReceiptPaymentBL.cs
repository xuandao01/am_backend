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
    public class ReceiptPaymentBL : BaseBL<receipt_payment>, IReceiptPaymentBL
    {
        private IReceiptPaymentDL _receiptPaymentDL;

        public ReceiptPaymentBL(IReceiptPaymentDL receiptPaymentDL) : base(receiptPaymentDL)
        {
            this._receiptPaymentDL = receiptPaymentDL;
        }

        public string GetNewPaymentCode()
        {
            return _receiptPaymentDL.GetNewPaymentCode();
        }

        public int DeleteFullPayment(string re_id)
        {
            return _receiptPaymentDL.DeleteFullPayment(re_id);
        }

        public IEnumerable<dynamic>? GetAllWithKeyword(string? keyword)
        {
            return _receiptPaymentDL.GetAllWithKeyword(keyword);
        }
    }
}
