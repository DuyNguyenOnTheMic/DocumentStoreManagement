﻿using Microsoft.EntityFrameworkCore;

namespace DocumentStoreManagement.DAL.SQL
{
    /// <summary>
    /// Encapsulates all repository transactions.
    /// </summary>
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public SqlUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _disposed = false;
        }

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <returns><see cref="ValueTask"/></returns>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            // Take this object off the finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">Whether or not we are disposing</param> 
        /// <returns><see cref="ValueTask"/></returns>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    await _dbContext.DisposeAsync();
                }

                // Dispose any unmanaged resources here...
                _disposed = true;
            }
        }
    }
}