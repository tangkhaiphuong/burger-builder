using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.Controls;

namespace BurgerBuilder.Avalonia
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public partial class MainWindow : Window
    {
        private readonly SynchronizationContext _synchronizationContext;

        public MainWindow()
        {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;
        }

        private async void WindowOnOpened(object sender, EventArgs e)
        {
            while (true)
            {
                var desc = await ObserveClick(buttonMenuI).Merge(ObserveClick(buttonMenuII))
                    .Select(c => c != buttonMenuI.Content.ToString()).FirstAsync();

                var result = await GetAsyncBurgerData(desc).Where(c => c != null).ToListAsync().ConfigureAwait(false);

                // Switch to main thread.
                await _synchronizationContext;

                await MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Your Burger",
                    string.Join(" - ", result)).ShowDialog(this);
            }
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

        private static IObservable<string> ObserveClick(Button button) => button.Events().Click.Select(_ => button.Content.ToString());
    }
}