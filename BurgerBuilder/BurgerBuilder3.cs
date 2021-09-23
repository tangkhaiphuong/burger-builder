using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BurgerBuilder
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class BurgerBuilder3 : IAsyncEnumerable<string>
    {
        private readonly List<IAsyncEnumerable<string>> _factories;

        public BurgerBuilder3(
            IObservable<string> skips,
            IObservable<string> meats,
            IObservable<string> tomatoes,
            IObservable<string> cheeses,
            IObservable<string> salads,
            bool descending = false)
        {
            _factories = new List<IAsyncEnumerable<string>>()
            {
                salads.Merge(skips).ToAsyncEnumerable().Take(1),
                salads.Merge(skips).ToAsyncEnumerable().Take(1),
                meats.Merge(skips).ToAsyncEnumerable().Take(1),
                tomatoes.Merge(skips).ToAsyncEnumerable().Take(1),
                tomatoes.Merge(skips).ToAsyncEnumerable().Take(1),
                tomatoes.Merge(skips).ToAsyncEnumerable().Take(1),
                cheeses.Merge(skips).ToAsyncEnumerable().Take(1),
                meats.Merge(skips).ToAsyncEnumerable().Take(1),
                salads.Merge(skips).ToAsyncEnumerable().Take(1),
            };
            if (descending) _factories.Reverse();
        }

        public BurgerBuilder3(
            IAsyncEnumerable<string> skips,
            IAsyncEnumerable<string> meats,
            IAsyncEnumerable<string> tomatoes,
            IAsyncEnumerable<string> cheeses,
            IAsyncEnumerable<string> salads,
            bool descending = false) : this(
            skips.ToObservable(),
            meats.ToObservable(),
            tomatoes.ToObservable(),
            cheeses.ToObservable(),
            salads.ToObservable(),
            descending
        )
        { }

        public async IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            foreach (var eventSource in _factories)
            await foreach (var item in eventSource.WithCancellation(cancellationToken))
                yield return item;
        }
    }
}