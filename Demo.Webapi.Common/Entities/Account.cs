using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.Common.Entities
{
    public class Account
    {
        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public int Property { get; set; }

        public string? EnglishName { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public Nullable<Guid> Dependency { get; set; }

        public int? FollowObject { get; set; }

        public string? FollowObjectValue { get; set; }

        public int? FollowObjectTHCP { get; set; }

        public string? FollowObjectTHCPValue { get; set; }

        public int? FollowOrder { get; set; }

        public string? FollowOrderValue { get; set; }

        public int? FollowContract { get; set; }

        public string? FollowContractValue { get; set; }

        public int? FollowUnit { get; set; }

        public string? FollowUnitValue { get; set; }

        public int? FollowBankAccount { get; set; }

        public int? FollowConstruction { get; set; }

        public string? FollowConstructionValue { get; set; }

        public int? FollowSellContract { get; set; }

        public string? FollowSellContractValue { get; set; }

        public int? FollowExpenseItem { get; set; }

        public string? FollowExpenseItemValue { get; set; }

        public int? FollowStatisticalCode { get; set; }

        public string? FollowStatisticalCodeValue { get; set; }

        public DateTime? Created_Date { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime? ModifiedDate { get; set;}

        public string? ModifiedBy { get; set;}

        public int DataLevel { get; set; }

        public int? AccountByException { get; set; }

    }
}
