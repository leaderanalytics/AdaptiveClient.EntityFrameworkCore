using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.StoreFront
{
    public class BaseService : IDisposable
    {
        protected IStoreFrontServiceManifest ServiceManifest;
        protected Db db;

        public BaseService(Db db, IStoreFrontServiceManifest serviceManifest)
        {
            this.db = db;
            this.ServiceManifest = serviceManifest;
        }


        #region IDisposable
        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    disposed = true;
                    db.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
