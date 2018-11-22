using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBook_FunctionalProgrammin.Mocks
{
    internal class MockSqlConnection : IDisposable, IDbConnection
    {
        private string _someConnString;

        public MockSqlConnection(string someConnString)
        {
            _someConnString = someConnString;
        }


        public void Open()
        {

        }

        public object Execute(string v)
        {
            return "resuts";
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }


                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
