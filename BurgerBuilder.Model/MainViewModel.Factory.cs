using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;

namespace BurgerBuilder.Model
{
    /// <inheritdoc />
    public partial class MainViewModel
    {
        private IAsyncEnumerable<string> GetAsyncBurger(bool desc)
        {
            var burgerDisplay = GetAsyncBurgerDisplay(desc);

            var burgerData = GetAsyncBurgerData(desc);

            return burgerDisplay.Zip(burgerData, (_, data) => data);
        }

        private IAsyncEnumerable<string> GetAsyncBurgerData(bool desc)
        {
            var meats = TakeMeat.ToAsyncEnumerable();
            var cheeses = TakeCheese.ToAsyncEnumerable();
            var salads = TakeSalad.ToAsyncEnumerable();
            var tomatoes = TakeTomato.ToAsyncEnumerable();

            var skips = TakeSkip.ToAsyncEnumerable();

            return new BurgerBuilder3(skips, meats, tomatoes, cheeses, salads, desc);
        }

        private async IAsyncEnumerable<string> GetAsyncBurgerDisplay(bool desc)
        {
            var builder = new BurgerBuilder3(
                AsyncEnumerable.Empty<string>(),
                AsyncEnumerable.Repeat(nameof(IsEnabledMeat), 1),
                AsyncEnumerable.Repeat(nameof(IsEnabledTomato), 1),
                AsyncEnumerable.Repeat(nameof(IsEnabledCheese), 1),
                AsyncEnumerable.Repeat(nameof(IsEnabledSalad), 1),
                desc);

            await foreach (var item in builder)
            {
                var property = GetType().GetProperty(item)!;

                await _synchronizationContext;

                var isEnabled = property!.GetValue(this);
                property.SetValue(this, true);

                yield return null;

                await _synchronizationContext;
                property.SetValue(this, isEnabled);
            }
        }
    }
}
