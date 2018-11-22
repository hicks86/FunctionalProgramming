using OrangeBook_FunctionalProgrammin.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBook_FunctionalProgrammin.Helpers
{
    internal static class ConnectionHelper
    {
        public static R Connect<R>(string connString, Func<IDbConnection, R> func)
        {
            using (var conn = new MockSqlConnection(connString))
            {
                conn.Open();
                return func(conn);
            }
        }
    }

}
