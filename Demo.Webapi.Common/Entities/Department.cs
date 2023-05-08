﻿namespace Demo.Webapi.Common.Entities;
public class Department
{
    /// <summary>
    /// Id phòng ban
    /// </summary>
    public Guid DepartmentId { get; set; }

    /// <summary>
    /// Tên phòng ban
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    /// Mã phòng ban
    /// </summary>
    public string DepartmentCode { get; set; }

    /// <summary>
    /// Mô tả
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Ngày tạo
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Người tạo
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Ngày sửa 
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Người sửa
    /// </summary>
    public string ModifiedBy { get; set; }
}

