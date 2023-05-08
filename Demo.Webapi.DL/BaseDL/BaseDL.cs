using Dapper;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entites.DTO;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Demo.Webapi.Common.Enums;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Npgsql;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Demo.Webapi.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Tạo mới bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns>Service result</returns>
        /// Xuân Đào (28/03/2023)
        //public ServiceResult CreateRecord(T record)
        //{
        //    var properties = typeof(T).GetProperties();
        //    string funcName = $"func_create_{typeof(T).Name}";
        //    string funcParam = "";
        //    foreach (var property in properties)
        //    {
        //        var value = property.GetValue(record);
        //        var propName = property.Name;
        //        if (property.Name == $"{typeof(T).Name}Id" || property.Name == "re_id")
        //        {
        //            Guid id = Guid.NewGuid();
        //            funcParam += $"'{id}'";
        //        }
        //        else if (property.Name == "CreatedDate" || property.Name == "ModifiedDate")
        //        {
        //            funcParam += $",'{DateTime.Now}'";
        //        }
        //        else
        //        {
        //            if (property.PropertyType.Name.CompareTo("Int32") != 0)
        //            {
        //                funcParam += $",'{value}'";
        //            } else
        //            {
        //                funcParam += $",{value}";
        //            }
        //        }
        //    }
        //    string queryString = $"select {funcName}({funcParam})";
        //    var mysqlConnection = GetOpenConnection();
        //    var result = QueryFirstOrDefault(mysqlConnection, queryString, commandType: CommandType.Text);
        //    mysqlConnection.Close();
        //    if (result == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return new ServiceResult()
        //        {
        //            IsSuccess = true,
        //            Data = result,
        //        };
        //    }
        //}

        public ServiceResult CreateRecord(T record)
        {
            try
            {

                string queryString = $"insert into {typeof(T).Name}";
                string colName = "(";
                string colValue = "(";
                var properties = typeof(T).GetProperties();
                int i = 0;
                foreach (var property in properties)
                {
                    if (i != properties.Length - 1)
                    {
                        colName += property.Name + ',';
                        if (i == 0)
                        {
                            string newId = Guid.NewGuid().ToString();
                            colValue += $"'{newId}',";
                        }
                        else
                        {
                            if (property.GetValue(record) == null)
                            {
                                colValue += "null" + ',';
                            } 
                            else
                            {
                                if (property.PropertyType.Name.CompareTo("Int32") != 0)
                                    colValue += $"'{property.GetValue(record).ToString()}'" + ',';
                                else 
                                    colValue += property.GetValue(record).ToString() + ',';
                            }
                        }
                    } else
                    {
                        colName += property.Name + ')';
                        if (property.GetValue(record) == null)
                        {
                            colValue += "null" + ')';
                        }
                        else
                        {
                            if (property.PropertyType.Name.CompareTo("Int32") != 0)
                                colValue += $"'{property.GetValue(record).ToString()}'" + ')';
                            else
                                colValue += property.GetValue(record).ToString() + ')';
                        }
                    }
                    i++;
                }

                queryString += colName + " values " + colValue + ';';
                var sqlConnection = GetOpenConnection();
                int result = sqlConnection.Execute(queryString, commandType: CommandType.Text);
                sqlConnection.Close();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Resource.createSuccess,
                    Data = result,
                };
            
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = Resource.createFail,
                    Data = ex.ToString(),
                };
            }
        }

        public ServiceResult BulkCreate(IEnumerable<T> RecordList)
        {
            try
            {
                string queryString = $"insert into {typeof(T).Name}";
                string colName = "(";
                string colValue = "";
                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    if (i != properties.Length - 1)
                    {
                        colName += properties[i].Name + ',';
                    }
                    else
                    {
                        colName += properties[i].Name + ')';
                    }
                }
                for (int i = 0; i < RecordList.Count(); i++)
                {
                    string colVal = "";
                    for (int j = 0; j < properties.Length; j++)
                    {
                        if (j == 0)
                        {
                            string id = Guid.NewGuid().ToString();
                            colVal += "('" + id + "',";
                        }
                        else if (j != properties.Length - 1)
                        {
                            if (properties[j].GetValue(RecordList.ElementAt(i)) == null)
                                colVal += "null,";
                            else
                            {
                                if (properties[j].PropertyType.Name.CompareTo("Int32") != 0)
                                    colVal += $"'{properties[j].GetValue(RecordList.ElementAt(i)).ToString()}'" + ',';
                                else
                                    colVal += properties[j].GetValue(RecordList.ElementAt(i)).ToString() + ',';
                            }
                        }
                        else
                        {
                            if (properties[j].GetValue(RecordList.ElementAt(i)) == null)
                                colVal += "null" + ')';
                            else
                            {
                                if (properties[j].PropertyType.Name.CompareTo("Int32") != 0) 
                                    colVal += $"'{properties[j].GetValue(RecordList.ElementAt(i)).ToString()}'" + ')';
                                else
                                    colVal += properties[j].GetValue(RecordList.ElementAt(i)).ToString() + ')';
                            }
                        }
                    }
                    if (i != 0)
                    {
                        colValue += ',' + colVal;
                    }
                    else
                    {
                        colValue += colVal;
                    }
                }
                queryString += colName + " values " + colValue + ';';
                var sqlConnection = GetOpenConnection();
                int result = sqlConnection.Execute(queryString, commandType: CommandType.Text);
                sqlConnection.Close();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Resource.createSuccess,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = Resource.createFail,
                    Data = ex.ToString(),
                };
            }
        }

        /// <summary>
        /// Xóa một bản ghi theo id
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Xuân Đào (28/03/2023)
        public int DeleteRecord(Guid recordId)
        {
            try
            {
                // Chuẩn bị query string
                string queryString = $"Delete from {typeof(T).Name} where {typeof(T).Name}.{typeof(T).Name}id = '{recordId}'";
                // Kết nối tới db
                var mySqlConection = GetOpenConnection();
                // Thực hiện query
                int rowEffected = Execute(mySqlConection, queryString, commandType: System.Data.CommandType.Text);

                mySqlConection.Close();
                return rowEffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public int MultipleDeleteRecord(string recordIds)
        {
            using (var mysqlConnection = GetOpenConnection())
            {
                using (var transaction = mysqlConnection.BeginTransaction())
                {
                    try
                    {
                        string[] list = recordIds.Split(',');
                        string funcName = $"func_delete_{typeof(T).Name}";
                        var mySqlConnection = GetOpenConnection();
                        string param = "";
                        for (int i = 0; i < list.Length; i++)
                        {
                            if (i == 0) param += $"'{list[i]}'";
                            else param += $", '{list[i]}'";
                        }
                        string queryString = $"Select {funcName}(array[{param}])";
                        int result = Execute(mySqlConnection, queryString, commandType: CommandType.Text);
                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return -2;
                    }
                }
            }
        }

        /// <summary>
        /// Truy vấn trả về số bản ghi bị ảnh hưởng
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns> Số bản ghi bị ảnh hưởng </returns>
        /// Xuân Đào (28/03/2023)
        public int Execute(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Filter Record
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// Xuân Đào (28/03/2023)
        public ServiceResult FilterRecord(string? keyWord, int? pageSize, int? pageNumber)
        {
            try
            {
                // Chuẩn bị procedure name
                //string func_name = $"func_filter_{typeof(T).Name}";
                int? offset = (pageSize * pageNumber) - pageSize;
                keyWord ??= "";
                string selectOption = "*";
                string joinOption = "";
                string? optionalQuery = null;
                if (typeof(T).Name == "receipt_payment") { 
                    selectOption = "*, (select sum(rpd.amount) as \"total_amount\" from receipt_payment_detail rpd where rpd.rp_id = re_id)";
                    joinOption = "left join supplier on account_id = supplier_id";
                    optionalQuery = "select sum(rpd.amount) from receipt_payment_detail rpd;";
                }
                string queryString = $"select {selectOption} from {typeof(T).Name} {joinOption} order by {typeof(T).Name}.created_date desc limit {pageSize} offset {offset};";
                string getTotalRecord = $"select count(*) as \"Total record\" from {typeof(T).Name};";
                var sqlConection = GetOpenConnection();
                // Thực hiện truy vấn
                string excuteQuery = queryString + getTotalRecord;
                if (optionalQuery != null) excuteQuery += optionalQuery;
                var resultSets = QueryMultiple(sqlConection, excuteQuery, commandType:CommandType.Text);
                // Kiểm tra kết quả trả về
                var data = resultSets.Read();
                var totalRecord = resultSets.Read();
                var optionResult = optionalQuery != null ? resultSets.Read() : null;
                sqlConection.Close();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = "",
                    Data = new
                    {
                        data,
                        totalRecord,
                        optionResult,
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ServiceResult
                {
                    IsSuccess = false,
                    Data = new ErrorResult
                    {
                        ErrorCode = ErrorCode.Exception,
                        DevMsg = Resource.devMsg_Exception,
                        UserMsg = Resource.userMsg_Exception,
                        MoreInfo = "More info: "
                    },
                };
            }
        }

        /// <summary>
        /// Khởi tạo kết nối tới db
        /// </summary>
        /// <returns></returns>
        /// Xuân Đào (28/03/2023)
        public IDbConnection GetOpenConnection()
        {
            var mySqlConnection = new NpgsqlConnection(DatabaseContext.connectionString);
            mySqlConnection.Open();
            return mySqlConnection;
        }

        /// <summary>
        /// Tìm kiếm bản ghi theo id
        /// </summary>
        /// <param name="id">id bản ghi cần tìm</param>
        /// <returns>Generic</returns>
        /// Xuân Đào (28/03/2023)
        public T GetRecordById(Guid id)
        {

            // Chuẩn bị tên stored
            string storedName = $"Proc_Get{typeof(T).Name}ById";
            // Chuẩn bị tham số đầu vào
            var param = new DynamicParameters();
            param.Add($"{typeof(T).Name}Id", id);
            // Kết nối tới db
            var mySqlConnection = GetOpenConnection();
            // Thực hiện gọi vào db chạy proc
            var record = QueryFirstOrDefault(mySqlConnection, storedName, param, commandType: System.Data.CommandType.StoredProcedure);
            // Xử lý kết quả trả về
            mySqlConnection.Close();
            return record;
        }

        /// <summary>
        /// Lấy toàn bộ record
        /// </summary>
        /// <returns>IEnumerable</returns>
        /// Xuân Đào (28/03/2023)
        public IEnumerable<dynamic> GetRecords()
        {
            try
            {
                // Chuẩn bị tên stored
                //string storedName = $"Proc_GetAll{typeof(T).Name}";
                string orderBy = "created_date";
                string orderSort = "desc";
                if (typeof(T).Name == "Account") { 
                    orderBy = "datalevel asc, accountnumber ";
                    orderSort = "asc";
                } 
                string queryString = $"select * from {typeof(T).Name} order by {orderBy} {orderSort}";
                // Kết nối tới db
                var mySqlConnection = GetOpenConnection();
                // Thực hiện gọi vào db chạy proc
                var resultSets = Query(mySqlConnection, queryString, commandType: System.Data.CommandType.Text);
                // Xử lý kết quả trả về
                mySqlConnection.Close();
                if (resultSets == null)
                {
                    return null;
                }
                else
                {
                    return resultSets.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Thực hiện truy vấn
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns> IEnumerable </returns>
        /// Xuân Đào (28/03/2023)
        public IEnumerable<dynamic> Query(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Truy vấn bản ghi đầu tiên
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>Generic</returns>
        /// Xuân Đào (28/03/2023)
        public T QueryFirstOrDefault(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public GridReader QueryMultiple(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }
        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns>Service result</returns>
        /// Xuân Đào (28/03/2023)
        public ServiceResult UpdateRecord(Guid id, T record)
        {
            var properties = typeof(T).GetProperties();
            string excuteString = $"update {typeof(T).Name} set ";
            int i = 0;
            foreach (var property in properties)
            {
                var value = property.GetValue(record);
                var propName = property.Name;
                if (i == 1)
                {
                    if (value == null)
                    {
                        excuteString += $"{propName}" + "=" + "null";
                    } else
                    {
                        if (property.PropertyType.Name.CompareTo("Int32") != 0)
                            excuteString += $"{propName}" + "=" + $"'{value}'";
                        else
                            excuteString += propName + "=" + value;
                    }
                }
                else if (i > 1)
                {
                    if (value == null || value.ToString().Length == 0)
                    {
                        excuteString += $",{propName}" + "=" + "null";
                    }
                    else
                    {
                        if (property.PropertyType.Name.CompareTo("Int32") != 0)
                        {
                            if (propName == "ModifiedDate")
                            {
                                excuteString += "," + $"{propName}" + "=" + $"'{new DateTime()}'";
                            }
                            else if (propName != "Created_Date" && propName != "Created_By" && propName != "CreatedBy")
                            {
                                excuteString += "," + $"{propName}" + "=" + $"'{value}'";
                            }
                        }
                        else
                            excuteString += "," + $"{propName}" + "=" + value;
                    }
                }
                i++;
            }
            excuteString += $" where {properties[0].Name} = {id};";
            var mysqlConnection = GetOpenConnection();
            var result = QueryFirstOrDefault(mysqlConnection, excuteString, commandType: CommandType.Text);
            mysqlConnection.Close();
            if (result == null)
            {
                return null;
            }
            else
            {
                return new ServiceResult()
                {
                    IsSuccess = true,
                    Message = Resource.updateSuccess,
                    Data = result,
                };
            }
        }

        public IEnumerable<dynamic> getAllByKeyword(string keyword)
        {
            // Chuẩn bị tên stored
            string storedName = $"Proc_Filter{typeof(T).Name}";

            int? offset = 0;
            var param = new DynamicParameters();
            param.Add("keyWord", keyword);
            param.Add("pageSize", 999);
            param.Add("offset", offset);
            // Kết nối tới db
            var mySqlConnection = GetOpenConnection();
            // Thực hiện query
            var result = Query(mySqlConnection, storedName, commandType: System.Data.CommandType.StoredProcedure);
            mySqlConnection.Close();
            return result;

        }

        public IEnumerable<dynamic> GetRecordByKeyword(string keyword)
        {
            string procName = $"Proc_GetEmployeeByKeyword";
            var param = new DynamicParameters();
            param.Add("keyword", keyword);
            var mySqlConnection = GetOpenConnection();
            var result = mySqlConnection.Query(procName, param, commandType: CommandType.StoredProcedure);
            mySqlConnection.Close();
            return result;
        }

    }
}
