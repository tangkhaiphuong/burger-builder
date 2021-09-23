using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BurgerBuilder.UnitTest
{
    [TestClass]
    public class BurgerBuilder3UnitTest
    {
        private readonly IAsyncEnumerable<string> _skipsLast = Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(_ => null as string).ToAsyncEnumerable();

        private readonly IAsyncEnumerable<string> _skipsFirst = Observable.Interval(TimeSpan.FromMilliseconds(1))
            .Select(_ => null as string).ToAsyncEnumerable();

        private readonly IAsyncEnumerable<string> _meats = Observable.Interval(TimeSpan.FromMilliseconds(100))
            .Select(_ => "Thịt").ToAsyncEnumerable();

        private readonly IAsyncEnumerable<string> _tomatoes = Observable.Interval(TimeSpan.FromMilliseconds(100))
            .Select(_ => "Cà chua").ToAsyncEnumerable();

        private readonly IAsyncEnumerable<string> _salads = Observable.Interval(TimeSpan.FromMilliseconds(100))
            .Select(_ => "Xà lách").ToAsyncEnumerable();

        private readonly IAsyncEnumerable<string> _cheeses = Observable.Interval(TimeSpan.FromMilliseconds(100))
            .Select(_ => "Phô mai").ToAsyncEnumerable();

        [TestMethod]
        public async Task TestBurgerBuilder2Asc()
        {
            var isSkipFirst = true;

            async IAsyncEnumerable<string> skips()
            {
                isSkipFirst = !isSkipFirst;
                yield return isSkipFirst
                    ? await _skipsFirst.FirstAsync().ConfigureAwait(false)
                    : await _skipsLast.LastAsync().ConfigureAwait(false);
            }

            var builder = new BurgerBuilder3(
                skips(),
                _meats,
                _tomatoes,
                _cheeses,
                _salads,
                false);

            var burger = await builder.ToListAsync().ConfigureAwait(false);

            Assert.AreEqual(burger.Count, 9);

            var salad = await _salads.FirstAsync().ConfigureAwait(false);
            var meat = await _meats.FirstAsync().ConfigureAwait(false);
            var tomato = await _tomatoes.FirstAsync().ConfigureAwait(false);
            var cheese = await _cheeses.FirstAsync().ConfigureAwait(false);

            Assert.AreEqual(burger[0], salad);
            Assert.AreEqual(burger[1], null);
            Assert.AreEqual(burger[2], meat);
            Assert.AreEqual(burger[3], null);
            Assert.AreEqual(burger[4], tomato);
            Assert.AreEqual(burger[5], null);
            Assert.AreEqual(burger[6], cheese);
            Assert.AreEqual(burger[7], null);
            Assert.AreEqual(burger[8], salad);
        }

        [TestMethod]
        public async Task TestBurgerBuilder2Desc()
        {
            var isSkipFirst = false;

            async IAsyncEnumerable<string> skips()
            {
                isSkipFirst = !isSkipFirst;
                yield return isSkipFirst
                    ? await _skipsFirst.FirstAsync().ConfigureAwait(false)
                    : await _skipsLast.LastAsync().ConfigureAwait(false);
            }

            var builder = new BurgerBuilder3(
                skips(),
                _meats,
                _tomatoes,
                _cheeses,
                _salads,
                true);

            var burger = await builder.ToListAsync().ConfigureAwait(false);

            Assert.AreEqual(burger.Count, 9);

            var salad = await _salads.FirstAsync().ConfigureAwait(false);
            var meat = await _meats.FirstAsync().ConfigureAwait(false);
            var tomato = await _tomatoes.FirstAsync().ConfigureAwait(false);

            Assert.AreEqual(burger[0], null);
            Assert.AreEqual(burger[1], meat);
            Assert.AreEqual(burger[2], null);
            Assert.AreEqual(burger[3], tomato);
            Assert.AreEqual(burger[4], null);
            Assert.AreEqual(burger[5], tomato);
            Assert.AreEqual(burger[6], null);
            Assert.AreEqual(burger[7], salad);
            Assert.AreEqual(burger[8], null);
        }
    }
}