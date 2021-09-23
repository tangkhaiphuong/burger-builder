using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BurgerBuilder
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class BurgerBuilder1 : IAsyncEnumerable<string>
    {
        private readonly IAsyncEnumerable<string> _meats;
        private readonly IAsyncEnumerable<string> _tomatoes;
        private readonly IAsyncEnumerable<string> _cheeses;
        private readonly IAsyncEnumerable<string> _salads;

        public BurgerBuilder1(
            IAsyncEnumerable<string> meats,
            IAsyncEnumerable<string> tomatoes,
            IAsyncEnumerable<string> cheeses,
            IAsyncEnumerable<string> salads)
        {
            _meats = meats;
            _tomatoes = tomatoes;
            _cheeses = cheeses;
            _salads = salads;
        }

        public async IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            await foreach (var item in _salads.Take(2).WithCancellation(cancellationToken))
                yield return item;

            await foreach (var item in _meats.Take(1).WithCancellation(cancellationToken))
                yield return item;

            await foreach (var item in _tomatoes.Take(3).WithCancellation(cancellationToken))
                yield return item;

            await foreach (var item in _cheeses.Take(1).WithCancellation(cancellationToken))
                yield return item;

            await foreach (var item in _meats.Take(1).WithCancellation(cancellationToken))
                yield return item;

            await foreach (var item in _salads.Take(1).WithCancellation(cancellationToken))
                yield return item;
        }
    }
}