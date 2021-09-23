using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace BurgerBuilder
{
    public static class Utilities
    {
        /// <summary>
        /// Implement GetAwaiter for support async/await keyword
        /// </summary>
        public static YieldAwaitable.YieldAwaiter GetAwaiter(this SynchronizationContext synchronizationContext)
        {
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            return Task.Yield().GetAwaiter();
        }
    }
}