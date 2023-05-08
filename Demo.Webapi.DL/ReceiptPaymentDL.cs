using Dapper;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Demo.Webapi.DL
{
    public class ReceiptPaymentDL : BaseDL<receipt_payment>, IReceiptPaymentDL
    {
        public string GetNewPaymentCode()
        {
            string queryString = "select rp.re_ref_no from receipt_payment rp order by rp.re_ref_no desc, length(rp.re_ref_no) asc";
            var sqlConnection = GetOpenConnection();
            var lastestCode = sqlConnection.QueryFirstOrDefault(queryString);
            string code = lastestCode.re_ref_no;
            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(code);

            string alphaPart = result.Groups[1].Value;
            string numberPart = result.Groups[2].Value;
            int codeNum = Int32.Parse(numberPart);
            string newCode = $"{alphaPart}{++codeNum}";
            return newCode;
        }
    }
}
