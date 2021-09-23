using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BurgerBuilder
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class BurgerBuilder2 : IAsyncEnumerable<string>
    {
        private readonly List<IAsyncEnumerable<string>> _factories;

        public BurgerBuilder2(
            IAsyncEnumerable<string> meats,
            IAsyncEnumerable<string> tomatoes,
            IAsyncEnumerable<string> cheeses,
            IAsyncEnumerable<string> salads,
            bool descending = false)
        {
            _factories = new List<IAsyncEnumerable<string>>()
            {
                salads.Take(2),
                meats.Take(1),
                tomatoes.Take(3),
                cheeses.Take(1),
                meats.Take(1),
                salads.Take(1),
            };
            if (descending) _factories.Reverse();
        }

        public async IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            foreach (var eventSource in _factories)
                await foreach (var item in eventSource.WithCancellation(cancellationToken))
                    yield return item;
        }
    }
}