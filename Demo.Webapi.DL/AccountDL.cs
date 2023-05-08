using Demo.Webapi.Common.Entites;
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
            return result;
        }

    }
}
