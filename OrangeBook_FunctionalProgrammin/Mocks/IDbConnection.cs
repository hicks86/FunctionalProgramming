using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBook_FunctionalProgrammin.Mocks
{
    interface IDbConnection
    {
        object Execute(string v);
        void Open();
    }
}
