using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BurgerBuilder.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public partial class MainWindow
    {
        private SynchronizationContext _synchronizationContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            _synchronizationContext = SynchronizationContext.Current;

            while (true)
            {
                await ResetStateAsync().ConfigureAwait(false);

                var desc = await ObserveClick(buttonMenuI).Merge(ObserveClick(buttonMenuII))
                    .Select(c => c != buttonMenuI.Content.ToString()).FirstAsync();

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
                var isEnabled = item.IsEnabled;
                item.IsEnabled = true;

                yield return null;

                await _synchronizationContext;
                item.IsEnabled = isEnabled;
            }
        }

        private async Task ResetStateAsync()
        {
            // Switch to main thread.
            await _synchronizationContext;

            buttonMeat.IsEnabled = false;
            buttonCheese.IsEnabled = false;
            buttonSalad.IsEnabled = false;
            buttonTomato.IsEnabled = false;
            buttonSkip.IsEnabled = false;
            buttonMenuI.IsEnabled = true;
            buttonMenuII.IsEnabled = true;
        }

        private async Task InitStateAsync()
        {
            // Switch to main thread.
            await _synchronizationContext;

            buttonSkip.IsEnabled = true;
            buttonMenuI.IsEnabled = false;
            buttonMenuII.IsEnabled = false;
        }

        private static IObservable<string> ObserveClick(Button button) => button.Events().Click.Select(_ => button.Content.ToString());
    }
}