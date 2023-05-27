using Demo.Webapi.BLayer;
using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Demo.Webapi.DL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Collections;

namespace Demo.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptPaymentController : BaseController<receipt_payment>
    {
        private IReceiptPaymentBL _receiptPaymentBL;
        public ReceiptPaymentController(IBaseBL<receipt_payment> baseBL, IReceiptPaymentBL receiptPaymentBL) : base(baseBL)
        {
            _receiptPaymentBL = receiptPaymentBL;
        }

        /// <summary>
        /// Hàm lấy số chứng từ mới
        /// </summary>
        /// <author>Xuân Đào - 02/05/2023</author>
        /// <returns></returns>
        [HttpGet("GetNewCode")]
        public IActionResult GetNewCode()
        {
            string newCode = _receiptPaymentBL.GetNewPaymentCode();
            if (newCode != null)
            {
                return StatusCode(200, new ServiceResult
                {
                    IsSuccess = true,
                    Data = newCode,
                });
            }
            else
            {
                return StatusCode(400, new ServiceResult { IsSuccess = false });
            }
        }

        /// <summary>
        /// Xóa toàn bộ chứng từ kèm detail
        /// </summary>
        /// <param name="id">id chứng từ</param>
        /// <returns></returns>
        [HttpDelete("FullDelete")]
        public IActionResult FullDelete([FromQuery] string id)
        {
            int result = _receiptPaymentBL.DeleteFullPayment(id);
            if (result == 1) {
                return StatusCode(200, new ServiceResult
                {
                    IsSuccess = true,
                    Message = Resource.deleteSuccess,
                    Data = 1
                });
            }
            else
            {
                return StatusCode (400, new ServiceResult { IsSuccess = false });
            }
        }

        /// <summary>
        /// Hàm xuất dữ liệu ra excel
        /// </summary>
        /// <param name="widthList">Danh sách độ rộng cột</param>
        /// <param name="keyword">Từ khóa nếu có</param>
        /// <returns></returns>
        [HttpGet("ExcelExport")]
        public IActionResult ExcelExport([FromQuery] string widthList, [FromQuery]string? keyword) {
            var stream = new MemoryStream();

            string[] width = widthList.Split(",");
            string excelTitle = Resource.paymentExcelTitle;
            string excelFile = Resource.paymentExcelFile;
            string excelSheet = Resource.paymentExcelSheet;

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(excelSheet);
                workSheet.Row(1).Style.Font.Name = "Arial";
                workSheet.Row(1).Style.Font.Size = 16;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Height = 20;
                workSheet.Cells["A1:J1"].Merge = true;
                workSheet.Cells["A1:J1"].Value = excelTitle;
                workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells["A2:J2"].Merge = true;
                workSheet.Row(2).Height = 20;
                workSheet.Row(3).Style.Font.Name = "Times New Roman";
                workSheet.Row(3).Style.Font.Bold = true;

                List<string> titleList = ExcelConfig.GetPaymentColName();
                List<string> dataList = ExcelConfig.GetPaymentModelName();
                //List<string> propertyName = ExcelConfig.AccountPropertyValue();
                //List<string> statusName = ExcelConfig.AccountStatusValue();
                // Title generator
                int k = 1;
                foreach (var cell in titleList)
                {
                    workSheet.Cells[3, k].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[3, k].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 212, 212, 212));
                    workSheet.Cells[3, k].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Cells[3, k].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    workSheet.Cells[3, k].Value = cell;
                    workSheet.Column(k).Width = (Int32.Parse(width[k - 1])) / 8;
                    workSheet.Cells[3, k].Style.Font.Name = "Times New Roman";
                    k++;
                }

                var serviceData = _receiptPaymentBL.GetAllWithKeyword(keyword);

                //Data generator
                int i = 4;
                decimal total_amount = 0;
                foreach (var cellValue in serviceData)
                {
                    int j = 1;
                    foreach (var cell in dataList)
                    {
                        workSheet.Cells[i, j].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[i, j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        workSheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        if (j == 2 || j == 3 || j == 1)
                            workSheet.Cells[i, j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        if (j == 6)
                            workSheet.Cells[i, j].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        switch(cell)
                        {
                            case "num":
                                {
                                    workSheet.Cells[i, j].Value = i - 3;
                                    break;
                                }
                            case "re_date":
                                {
                                    if (cellValue.re_date != null)
                                    {
                                        string dayVal = cellValue.re_date.Day < 10 ? "0" + cellValue.re_date.Day : "" + cellValue.re_date.Day;
                                        string monthVal = cellValue.re_date.Month < 10 ? "0" + cellValue.re_date.Month : "" + cellValue.re_date.Month;
                                        workSheet.Cells[i, j].Value = $"{dayVal}/{monthVal}/{cellValue.re_date.Year}";
                                    }
                                    break;
                                }
                            case "ca_date":
                                {
                                    if (cellValue.ca_date != null)
                                    {
                                        string dayVal = cellValue.ca_date.Day < 10 ? "0" + cellValue.ca_date.Day : "" + cellValue.ca_date.Day;
                                        string monthVal = cellValue.ca_date.Month < 10 ? "0" + cellValue.ca_date.Month : "" + cellValue.ca_date.Month;
                                        workSheet.Cells[i, j].Value = $"{dayVal}/{monthVal}/{cellValue.ca_date.Year}";
                                    }
                                    break;
                                }
                            case "re_ref_no":
                                {
                                    workSheet.Cells[i, j].Value = cellValue.re_ref_no;
                                    break;
                                }
                            case "re_description":
                                {
                                    workSheet.Cells[i, j].Value = cellValue.re_description;
                                    break;
                                }
                            case "amount":
                                {
                                    if (cellValue.amount != null)
                                    {
                                        total_amount += cellValue.amount;
                                        workSheet.Cells[i, j].Value = cellValue.amount.ToString("#,##0");
                                    }
                                    break;
                                }
                            case "supplier_code":
                                {
                                    workSheet.Cells[i, j].Value = cellValue.supplier_code;
                                    break;
                                }
                            case "supplier_name":
                                {
                                    workSheet.Cells[i, j].Value = cellValue.supplier_name;
                                    break;
                                }
                            case "re_reason":
                                {
                                    workSheet.Cells[i, j].Value = cellValue.re_reason;
                                    break;
                                }
                            case "ca_type":
                                {
                                    if (cellValue.ca_type != null)
                                    {
                                        if (cellValue.ca_type == 0)
                                            workSheet.Cells[i, j].Value = "Phiếu chi";
                                        else
                                            workSheet.Cells[i, j].Value = "Phiếu thu";
                                    } 
                                    else
                                        workSheet.Cells[i, j].Value = "Phiếu chi";
                                    break;
                                }
                        }
                        j++;
                    }
                    i++;
                }
                workSheet.Row(i).Style.Font.Bold = true;
                for(int j=1;j<=dataList.Count;j++)
                {
                    workSheet.Cells[i, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[i,j].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 212, 212, 212));
                    workSheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                workSheet.Cells[i, 2].Value = "Tổng";
                workSheet.Cells[i, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells[i, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                workSheet.Cells[i, 6].Value = total_amount.ToString("#,##0");
                package.Save();
            }

            stream.Position = 0;
            string fileNameExport = $"{excelFile}-{DateTime.Now.ToString()}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileNameExport);
        }

        /// <summary>
        /// Hàm lấy dữ liệu theo từ khóa
        /// </summary>
        /// <param name="keyword">Từ khóa</param>
        /// <author>Xuân Đào 27/04/2023</author>
        /// <returns></returns>
        [HttpGet("GetAllByKeyword")]
        public IActionResult GetAllWithKeyword([FromQuery] string? keyword)
        {
            var result = _receiptPaymentBL.GetAllWithKeyword(keyword);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                return StatusCode(200, result);
            }
        }

        /// <summary>
        /// Hàm xóa hàng loạt chứng từ
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <author>Xuân Đào 27/04/2023</author>
        /// <returns></returns>
        [HttpDelete("DeleteMultiple")]
        public IActionResult DeleteMultiple(string[]? ids)
        {
            int result = _receiptPaymentBL.DeleteFullMultiple(ids);
            if (result > 0) return StatusCode(200, new ServiceResult
            {
                IsSuccess = true,
                Message = Resource.deleteSuccess,
                Data = result,
            });
            else return StatusCode(400, new ServiceResult
            {
                IsSuccess = false,
            });
        }

        /// <summary>
        /// Hàm cập nhật chứng từ kèm detail
        /// </summary>
        /// <param name="record">Chứng từ</param>
        /// <param name="rpds">Danh sách chi tiết</param>
        /// <author>Xuân Đào 27/04/2023</author>
        /// <returns></returns>
        [HttpPut("UpdateFullPayment")]
        public IActionResult UpdateFullPayment([FromQuery]receipt_payment record, [FromBody]receipt_payment_detail[]? rpds)
        {
            var result = _receiptPaymentBL.UpdateFullPayment(record, rpds);
            if (result == null)
            {
                return StatusCode(404, NotFound());
            } else
            {
                return StatusCode(200, result);
            }
        }
    }
}
