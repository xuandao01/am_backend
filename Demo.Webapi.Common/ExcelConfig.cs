using Demo.Webapi.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Webapi.Common
{
    public static class ExcelConfig
    {
        public static List<string> getAccountColName()
        {
            List<string> list = new List<string>();
            list.Add("Số tài khoản");
            list.Add("Tên tài khoản");
            list.Add("Tính chất");
            list.Add("Tên tiếng anh");
            list.Add("Diễn giải");
            list.Add("Trạng thái");
            return list;
        }

        public static List<string> getAccountModelName()
        {
            List<string> list = new List<string>();
            list.Add("AccountNumber");
            list.Add("AccountName");
            list.Add("Property");
            list.Add("EnglishName");
            list.Add("Description");
            list.Add("Status");
            return list;
        }
    }
}
