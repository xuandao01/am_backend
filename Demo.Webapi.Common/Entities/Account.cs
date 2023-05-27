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
        // ID Tài khoản
        [Required]
        public Guid AccountId { get; set; }

        /// Số tài khoản
        [Required]
        public int AccountNumber { get; set; }

        // Tên tài khoản
        [Required]
        public string AccountName { get; set; }

        // Tính chất
        [Required]
        public int Property { get; set; }

        // Tên tiếng anh
        public string? EnglishName { get; set; }

        // Diễn giải
        public string? Description { get; set; }

        // Trạng thái
        public int Status { get; set; }

        // Tài khoản tổng hợp
        public Nullable<Guid> Dependency { get; set; }

        // Theo dõi đối tượng
        public int? FollowObject { get; set; }

        // Giá trị theo dõi đối tượng
        public string? FollowObjectValue { get; set; }

        // Theo dõi đối tượng THCP
        public int? FollowObjectTHCP { get; set; }

        // Giá trị theo dõi đối tượng THCP
        public string? FollowObjectTHCPValue { get; set; }

        // Theo dõi đơn mua
        public int? FollowOrder { get; set; }
        // Giá trị theo dõi đơn mua
        public string? FollowOrderValue { get; set; }

        // Theo dõi hợp đồng mua
        public int? FollowContract { get; set; }

        // Giá trị theo dõi hợp đồng mua
        public string? FollowContractValue { get; set; }

        // Theo dõi đơn vị
        public int? FollowUnit { get; set; }

        // Giá trị theo dõi đơn vị
        public string? FollowUnitValue { get; set; }
        
        // Theo dõi theo tài khoản ngân hàng
        public int? FollowBankAccount { get; set; }

        // Theo dõi theo công trình
        public int? FollowConstruction { get; set; }

        // Giá trị theo dõi theo công trình
        public string? FollowConstructionValue { get; set; }

        // Theo dõi theo hợp đồng bán
        public int? FollowSellContract { get; set; }

        // Giá trị theo dõi theo hợp đồng bán
        public string? FollowSellContractValue { get; set; }

        // Theo dõi theo khoản mục CP
        public int? FollowExpenseItem { get; set; }

        // Giá trị theo dõi theo khoản mục CP
        public string? FollowExpenseItemValue { get; set; }

        // Theo dõi theo mã thống kê
        public int? FollowStatisticalCode { get; set; }

        // Giá trị theo dõi theo mã thống kê
        public string? FollowStatisticalCodeValue { get; set; }

        // Ngày tạo
        public DateTime? Created_Date { get; set; }

        // Người tạo
        public string? CreatedBy { get; set; } 

        // Ngày sửa
        public DateTime? ModifiedDate { get; set;}

        // Người sửa
        public string? ModifiedBy { get; set;}

        public int DataLevel { get; set; }

        public int? AccountByException { get; set; }

    }
}
