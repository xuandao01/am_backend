using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.BLayer
{
    public interface IReceiptPaymentDetailBL : IBaseBL<receipt_payment_detail>
    {
        public ServiceResult GetAllRPDByReId(Guid re_id);

        public int BulkInsert(IEnumerable<receipt_payment_detail> ReceiptPaymentDetails);
    }
}
