﻿using Demo.Webapi.Common.Entites;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entites.DTO;
using System.Net.Http;
using Demo.Webapi.Common.Enums;
using Microsoft.AspNetCore.Http;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.DL.BaseDL;

namespace Demo.Webapi.DL
{
    public class AccountDL : BaseDL<Account>, IAccountDL
    {
        /// <summary>
        /// Hàm cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="ids">Danh sách id tài khoản</param>
        /// <param name="newStatus">Trạng thái mới</param>
        /// <author>Xuân Đào 10/05/2023</author>
        /// <returns></returns>
        public int UpdateMultipleStatus(string ids, int newStatus)
        {
            string[] idList = ids.Split(',');
            string idl = "";
            for (int i = 0; i < idList.Length; i++)
            {
                if (i==0) idl = $"'{idList[i]}'";
                else 
                    idl += $",'{idList[i]}'";
            }
            string queryString = $"SELECT public.func_update_multiple_account(array[{idl}], {newStatus});";
            var sqlConnection = GetOpenConnection();
            int result = sqlConnection.QueryFirstOrDefault<int>(queryString, commandType: CommandType.Text);
            sqlConnection.Close();
            return result;
        }

        /// <summary>
        /// Hàm cập nhật cấp độ tài khoản
        /// </summary>
        /// <param name="accounts">Danh sách tài khoản</param>
        /// <author>Xuân Đào 10/05/2023</author>
        /// <returns></returns>
        public int UpdateAccountLevel(List<Account> accounts)
        {
            string queryString = "update account set datalevel = case";
            string account_ids = "";
            for (int i = 0;i < accounts.Count;i++)
            {
                queryString += " when accountid = " + accounts[i].AccountId + " then " + accounts[i].DataLevel;
                if (i == 0)
                {
                    account_ids += $"'{accounts[i].AccountId}'";
                }
                else
                {
                    account_ids += $",'{accounts[i].AccountId}'";
                }
            }
            queryString += $" else datalevel end where accountid in ({account_ids});";
            var sqlConnection = GetOpenConnection();
            int result = sqlConnection.Execute(queryString, commandType: CommandType.Text);
            sqlConnection.Close();
            return result;

        }
    }
}
