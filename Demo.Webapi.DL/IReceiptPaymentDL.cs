using Demo.Webapi.Common.Entities;
using Demo.Webapi.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.DL
{
    public interface IReceiptPaymentDL : IBaseDL<receipt_payment>
    {
        public string GetNewPaymentCode();
    }
}
