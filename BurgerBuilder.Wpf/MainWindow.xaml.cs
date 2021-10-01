using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BurgerBuilder.Model;
using ReactiveUI;

namespace BurgerBuilder.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.IsEnabledMenuI, v => v.buttonMenuI.IsEnabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledMenuII, v => v.buttonMenuII.IsEnabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledSkip, v => v.buttonSkip.IsEnabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledMeat, v => v.buttonMeat.IsEnabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledCheese, v => v.buttonCheese.IsEnabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledSalad, v => v.buttonSalad.IsEnabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledTomato, v => v.buttonTomato.IsEnabled).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeSkip,
                    v => v.buttonSkip, nameof(Button.Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeCheese,
                v => v.buttonCheese, nameof(Button.Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeSalad,
                v => v.buttonSalad, nameof(Button.Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeMeat,
                v => v.buttonMeat, nameof(Button.Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeTomato,
                v => v.buttonTomato, nameof(Button.Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.ProcessMenuI,
                v => v.buttonMenuI, nameof(Button.Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.ProcessMenuII,
                v => v.buttonMenuII, nameof(Button.Click)).DisposeWith(d);

                MessageBus.Current.Listen<string>("burger").ObserveOn(RxApp.MainThreadScheduler).Subscribe(c =>
                {
                    MessageBox.Show(c, "Your burger");
                }).DisposeWith(d);
            });
        }

        private async void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MainViewModel(SynchronizationContext.Current);
            await ViewModel.ResetStateAsync();
        }
    }
}