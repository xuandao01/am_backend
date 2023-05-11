using Dapper;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Demo.Webapi.Common.Entities.DTO;

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

        public int DeleteFullPayment(string re_id)
        {
            try
            {
                string deleteMaster = $"delete from receipt_payment where re_id = '{re_id}';";
                string deleteDetail = $"delete from receipt_payment_detail where rp_id = '{re_id}';";
                var sqlConnection = GetOpenConnection();
                var result = QueryMultiple(sqlConnection, deleteMaster + deleteDetail, commandType: System.Data.CommandType.Text);
                if (result == null)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
                //using (var transaction = sqlConnection.BeginTransaction())
                //{
                //    try
                //    {
                //        var result = QueryMultiple(sqlConnection, deleteMaster + deleteDetail, commandType: System.Data.CommandType.Text);
                //        if (result == null)
                //        {
                //            transaction.Rollback();
                //            return -1;
                //        }
                //        else
                //        {
                //            transaction.Commit();
                //            return 1;
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        transaction.Rollback();
                //        Console.WriteLine(e.Message);
                //        return -1;
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public IEnumerable<dynamic>? GetAllWithKeyword(string? keyword)
        {
            string queryString = "Select *, (select sum(rpd.amount) as \"amount\" from receipt_payment_detail rpd where rpd.rp_id = re_id) from receipt_payment left join supplier on receipt_payment.account_id = supplier.supplier_id";

            if (keyword != null && keyword.Length > 0)
            {
                var properties = typeof(receipt_payment).GetProperties();
                int i = 0;
                foreach (var property in properties)
                {
                    if (i == 0)
                        queryString += $" where cast(receipt_payment.{property.Name} as text) ilike '%{keyword}%'";
                    else
                        queryString += $" or cast(receipt_payment.{property.Name} as text) ilike '%{keyword}%'";
                    i++;
                }
            }

            try
            {
                var sqlConnection = GetOpenConnection();
                var result = sqlConnection.Query(queryString, commandType: System.Data.CommandType.Text);
                return result;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
