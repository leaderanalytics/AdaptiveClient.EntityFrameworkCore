using System;
using System.Collections.Generic;
using System.Text;

namespace LeaderAnalytics.AdaptiveClient.EntityFramework.Tests.Artifacts.BackOffice
{
    public class BaseService : IDisposable
    {
        protected IBackOficeServiceManifest ServiceManifest;
        protected Db db;

        public BaseService(Db db, IBackOficeServiceManifest serviceManifest )
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
