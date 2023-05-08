using Demo.Webapi.Common.Entites;
using Demo.Webapi.Common.Entities;
using Demo.Webapi.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.DL
{
    public interface IAccountDL : IBaseDL<Account>
    {
        public int UpdateMultipleStatus(string ids, int newStatus);
    }
}
