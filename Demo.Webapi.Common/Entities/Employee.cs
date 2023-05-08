using Demo.Webapi.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Webapi.Common.Entities
{
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required]
        [StringLength(100)]
        public string? FullName { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [StringLength(20)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        
        [StringLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [StringLength(255)]
        public string? Address { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        [StringLength(25)]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        [StringLength(255)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// id chức danh
        /// </summary>
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Số tk ngân hàng
        /// </summary>
        [StringLength(25)]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh tk ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankBranch { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? Created_Date { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set;}

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string? ModifiedBy { get; set;}
    }
}
