using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.DataAccess
{
    public class DataContextProvider : IDisposable
    {
        private SoccerContext _dataContext;

        private DbContextOptions<SoccerContext> options;

        public DataContextProvider(DbContextOptions<SoccerContext> _options)
        {
            options = _options;
        }

        public SoccerContext Get()
        {
            return _dataContext ?? (_dataContext = new SoccerContext(options));
        }

        private bool _isDisposed;

        ~DataContextProvider()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        protected void DisposeCore()
        {
            _dataContext?.Dispose();
        }

    }

}