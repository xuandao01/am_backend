using Dapper;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Demo.Webapi.DL.BaseDL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.DL
{
    public class ReceiptPaymentDetailDL : BaseDL<receipt_payment_detail>, IReceiptPaymentDetailDL
    {
        public ServiceResult GetAllRPDByReId(Guid re_id)
        {
            string queryString = $"Select rpd.rpd_description , rpd.credit_account , rpd.debit_account , rpd.amount , s.supplier_code " +
                $",s.supplier_name from receipt_payment_detail rpd left join receipt_payment rp on rpd.rp_id = rp.re_id left join " +
                $"supplier s on rp.account_id = s.supplier_id where rpd.rp_id = '{re_id}';";
            string getSummary = $"select sum(rpd.amount) as \"Total amount\" from receipt_payment_detail rpd where rpd.rp_id = '{re_id}';";
            string getTotalRecord = $"select count(*) as \"Total record\" from receipt_payment_detail rpd where rpd.rp_id = '{re_id}';";
            var sqlConnection = GetOpenConnection();
            var result = sqlConnection.QueryMultiple(queryString + getSummary + getTotalRecord, commandType: System.Data.CommandType.Text);
            var data = result.Read();
            var summary = result.Read();
            var totalRecord = result.Read();
            return new ServiceResult
            {
                IsSuccess = true,
                Message = "",
                Data = new
                {
                    data,
                    summary,
                    totalRecord
                }
            };
        }

        public int BulkInsert(IEnumerable<receipt_payment_detail> ReceiptPaymentDetails)
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=password;Database=postgres"))
            {
                connection.Open();

                var param = ReceiptPaymentDetails.Select(p => new
                {
                    rpd_id = p.rpd_id,
                    rp_id = p.rp_id,
                    rpd_description = p.rpd_description,
                    debit_account = p.debit_account,
                    credit_account = p.credit_account,
                    amount = p.amount,
                    object_id = p.object_id,
                    created_date = p.created_date,
                    created_by = p.created_by,
                    modified_date = p.modified_date,
                    modified_by = p.modified_by
                });
                connection.Execute("INSERT INTO receipt_payment_detail (rpd_id, rp_id, rpd_description, debit_account, credit_account, amount, object_id, created_date, created_by," +
                    "modified_date, modified_by) VALUES (@rpd_id, @rp_id, @rpd_description, @debit_account, @credit_account, @amount, @object_id, @created_date, @created_by," +
                    "@modified_date, @modified_by)", param);
                return ReceiptPaymentDetails.Count();
            }
        }
    }
}
