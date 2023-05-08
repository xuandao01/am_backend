using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.Common.Entities
{
    public class Supplier
    {
        [Required]
        public Guid supplier_id { get; set; }

        [Required]
        [StringLength(20)]
        public string supplier_code { get; set; }

        public string? supplier_name { get; set; }

        public string? tax_code { get; set; }

        public string? address { get; set; }

        public string? phone_number { get; set; }

        public DateTime? created_date { get; set; }

        public string? created_by { get; set; }

        public DateTime? modified_date { get; set; }

        public string? modified_by { get; set; }
    }
}
