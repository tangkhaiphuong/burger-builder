using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BurgerBuilder;
using ReactiveUI;

namespace BurgerBuilder.Model
{
    /// <inheritdoc />
    public partial class MainViewModel
    {
        public async Task ResetStateAsync()
        {
            await _synchronizationContext;
            IsEnabledMeat = false;
            IsEnabledCheese = false;
            IsEnabledSalad = false;
            IsEnabledTomato = false;
            IsEnabledSkip = false;
            IsEnabledMenuI = true;
            IsEnabledMenuII = true;
        }

        private async Task InitStateAsync()
        {
            await _synchronizationContext;
            IsEnabledSkip = true;
            IsEnabledMenuI = false;
            IsEnabledMenuII = false;
        }

        private async Task OnMenuOneHandleAsync(CancellationToken cancellationToken = default)
        {
            await InitStateAsync().ConfigureAwait(false);

            var result = await GetAsyncBurger(false).Where(c => c != null).ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            await ResetStateAsync().ConfigureAwait(false);

            var burger = string.Join(" - ", result);
            MessageBus.Current.SendMessage<string>(burger, "burger");
        }

        private async Task OnMenuSecondHandleAsync(CancellationToken cancellationToken = default)
        {
            await InitStateAsync().ConfigureAwait(false);

            var result = await GetAsyncBurger(true).Where(c => c != null).ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            await ResetStateAsync().ConfigureAwait(false);

            var burger = string.Join(" - ", result);
            MessageBus.Current.SendMessage<string>(burger, "burger");
        }
    }
}
