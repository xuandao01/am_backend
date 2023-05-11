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
    public interface IReceiptPaymentBL : IBaseBL<receipt_payment>
    {
        public string GetNewPaymentCode();

        public int DeleteFullPayment(string re_id);

        public IEnumerable<dynamic>? GetAllWithKeyword(string? keyword);
    }
}
