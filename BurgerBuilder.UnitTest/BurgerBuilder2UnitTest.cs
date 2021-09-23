using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BurgerBuilder.UnitTest
{
    [TestClass]
    public class BurgerBuilder2UnitTest
    {
        private readonly IAsyncEnumerable<string> _meats = AsyncEnumerable.Repeat("Thịt", int.MaxValue);
        private readonly IAsyncEnumerable<string> _tomatoes = AsyncEnumerable.Repeat("Cà chua", int.MaxValue);
        private readonly IAsyncEnumerable<string> _salads = AsyncEnumerable.Repeat("Xà lách", int.MaxValue);
        private readonly IAsyncEnumerable<string> _cheeses = AsyncEnumerable.Repeat("Phô mai", int.MaxValue);

        [TestMethod]
        public async Task TestBurgerBuilder2Asc()
        {
            var builder = new BurgerBuilder2(
                _meats,
                _tomatoes,
                _cheeses,
                _salads);

            var burger = await builder.ToListAsync().ConfigureAwait(false);

            Assert.AreEqual(burger.Count, 9);

            var salad = await _salads.FirstAsync().ConfigureAwait(false);
            var meat = await _meats.FirstAsync().ConfigureAwait(false);
            var tomato = await _tomatoes.FirstAsync().ConfigureAwait(false);
            var cheese = await _cheeses.FirstAsync().ConfigureAwait(false);

            Assert.AreEqual(burger[0], salad);
            Assert.AreEqual(burger[1], salad);
            Assert.AreEqual(burger[2], meat);
            Assert.AreEqual(burger[3], tomato);
            Assert.AreEqual(burger[4], tomato);
            Assert.AreEqual(burger[5], tomato);
            Assert.AreEqual(burger[6], cheese);
            Assert.AreEqual(burger[7], meat);
            Assert.AreEqual(burger[8], salad);
        }

        [TestMethod]
        public async Task TestBurgerBuilder2Desc()
        {
            var builder = new BurgerBuilder2(
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
            var cheese = await _cheeses.FirstAsync().ConfigureAwait(false);

            Assert.AreEqual(burger[0], salad);
            Assert.AreEqual(burger[1], meat);
            Assert.AreEqual(burger[2], cheese);
            Assert.AreEqual(burger[3], tomato);
            Assert.AreEqual(burger[4], tomato);
            Assert.AreEqual(burger[5], tomato);
            Assert.AreEqual(burger[6], meat);
            Assert.AreEqual(burger[7], salad);
            Assert.AreEqual(burger[8], salad);
        }
    }
}