using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AsyncLock : IDisposable
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _semaphoreSlim.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Do(Func<CancellationToken, Task> func,
            int timeout = -1,
            CancellationToken cancellationToken = default)
        {
            await _semaphoreSlim.WaitAsync(timeout, cancellationToken);
            try
            {
                await func.Invoke(cancellationToken);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task Do(Action<CancellationToken> func,
            int timeout = -1,
            CancellationToken cancellationToken = default)
        {
            await _semaphoreSlim.WaitAsync(timeout, cancellationToken);
            try
            {
                func.Invoke(cancellationToken);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
