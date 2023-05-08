using Demo.Webapi.BLayer.BaseBL;
using Demo.Webapi.Common;
using Demo.Webapi.Common.Entites;
using Demo.Webapi.Common.Entites.DTO;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.Common.Entities.DTO;
using Demo.Webapi.Common.Enums;
using Demo.Webapi.DL;
using Demo.Webapi.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.BLayer
{
    public class AccountBL : BaseBL<Account>, IAccountBL
    {
        IAccountDL _accountDL;

        public AccountBL(IBaseDL<Account> baseDL, IAccountDL accountDL) : base(baseDL)
        {
            _accountDL = accountDL;
        }

        public int UpdateMultipleStatus(string ids, int newStatus)
        {
            return _accountDL.UpdateMultipleStatus(ids, newStatus);
        }
    }
}
