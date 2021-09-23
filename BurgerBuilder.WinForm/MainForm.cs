using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace BurgerBuilder.WinForm
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void BurgerBuilderFormOnLoad(object sender, EventArgs e)
        {
            while (true)
            {
                var desc = await ObserveClick(buttonMenuI).Merge(ObserveClick(buttonMenuII))
                    .Select(c => c != buttonMenuI.Text).FirstAsync();

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

        private static IObservable<string> ObserveClick(Button button) => button.Events().Click.Select(_ => button.Text);
    }
}