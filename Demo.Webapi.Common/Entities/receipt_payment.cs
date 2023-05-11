using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.Common.Entities
{
    public class receipt_payment
    {
        public Guid re_id { get; set; }
        public string re_ref_no { get; set; }

        public DateTime re_date { get; set; }

        public string? re_description { get; set; }

        public Guid account_id { get; set; }

        public string? re_reason { get; set; }

        public int? ca_type { get; set; }

        public DateTime ca_date { get; set; }

        public string? employee_id { get; set; }

        public DateTime? created_date { get; set; }

        public string? created_by { get; set; }

        public DateTime? modified_date { get; set; }

        public string? modified_by { get; set; }
    }
}
