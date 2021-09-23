using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                var desc = await ObserveClick(buttonMenuI).Merge(ObserveClick(buttonMenuII))
                    .Select(c => c != buttonMenuI.Content.ToString()).FirstAsync();

                var result = await GetAsyncBurgerData(desc).Where(c => c != null).ToListAsync().ConfigureAwait(false);

                MessageBox.Show(string.Join(" - ", result), "Your Burger");
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