using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurgerBuilder.WinForm
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public partial class MainForm : Form
    {
        private SynchronizationContext _synchronizationContext;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void BurgerBuilderFormOnLoad(object sender, EventArgs e)
        {
            _synchronizationContext = SynchronizationContext.Current;

            while (true)
            {
                await ResetStateAsync().ConfigureAwait(false);

                var desc = await ObserveClick(buttonMenuI).Merge(ObserveClick(buttonMenuII))
                    .Select(c => c != buttonMenuI.Text).FirstAsync();

                await InitStateAsync().ConfigureAwait(false);

                var result = await GetAsyncBurger(desc).Where(c => c != null).ToListAsync().ConfigureAwait(false);

                MessageBox.Show(string.Join(" - ", result), "Your Burger");
            }
        }

        private IAsyncEnumerable<string> GetAsyncBurger(bool desc)
        {
            var burgerDisplay = GetAsyncBurgerDisplay(desc);

            var burgerData = GetAsyncBurgerData(desc);

            return burgerDisplay.Zip(burgerData, (_, data) => data);
        }

        private IAsyncEnumerable<string> GetAsyncBurgerData(bool desc)
        {
            var meats = ObserveClick(buttonMeat).ToAsyncEnumerable();
            var cheeses = ObserveClick(buttonCheese).ToAsyncEnumerable();
            var salads = ObserveClick(buttonSalad).ToAsyncEnumerable();
            var tomatoes = ObserveClick(buttonTomato).ToAsyncEnumerable();

            var skips =
                ObserveClick(buttonSkip).Select(_ => null as string).ToAsyncEnumerable();

            return new BurgerBuilder3(skips, meats, tomatoes, cheeses, salads,
                desc);
        }

        private async IAsyncEnumerable<string> GetAsyncBurgerDisplay(bool desc)
        {
            var dictionary = new Dictionary<string, Button>()
            {
                {nameof(buttonMeat), buttonMeat},
                {nameof(buttonTomato), buttonTomato},
                {nameof(buttonCheese), buttonCheese},
                {nameof(buttonSalad), buttonSalad},
            };

            var builder = new BurgerBuilder3(
                AsyncEnumerable.Empty<string>(),
                AsyncEnumerable.Repeat(nameof(buttonMeat), 1),
                AsyncEnumerable.Repeat(nameof(buttonTomato), 1),
                AsyncEnumerable.Repeat(nameof(buttonCheese), 1),
                AsyncEnumerable.Repeat(nameof(buttonSalad), 1),
                desc).Select(c => dictionary[c]);

            await foreach (var item in builder)
            {
                await _synchronizationContext;
                var isEnabled = item.Enabled;
                item.Enabled = true;

                yield return null;

                await _synchronizationContext;
                item.Enabled = isEnabled;
            }
        }

        private async Task ResetStateAsync()
        {
            // Switch to main thread.
            await _synchronizationContext;

            buttonMeat.Enabled = false;
            buttonCheese.Enabled = false;
            buttonSalad.Enabled = false;
            buttonTomato.Enabled = false;
            buttonSkip.Enabled = false;
            buttonMenuI.Enabled = true;
            buttonMenuII.Enabled = true;
        }

        private async Task InitStateAsync()
        {
            // Switch to main thread.
            await _synchronizationContext;

            buttonSkip.Enabled = true;
            buttonMenuI.Enabled = false;
            buttonMenuII.Enabled = false;
        }

        private static IObservable<string> ObserveClick(Button button) => button.Events().Click.Select(_ => button.Text);
    }
}