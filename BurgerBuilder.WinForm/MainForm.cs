using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using BurgerBuilder.Model;
using ReactiveUI;

namespace BurgerBuilder.WinForm
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public partial class MainForm : Form, IViewFor<MainViewModel>
    {
        public MainForm()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.IsEnabledMenuI, v => v.buttonMenuI.Enabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledMenuII, v => v.buttonMenuII.Enabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledSkip, v => v.buttonSkip.Enabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledMeat, v => v.buttonMeat.Enabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledCheese, v => v.buttonCheese.Enabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledSalad, v => v.buttonSalad.Enabled).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.IsEnabledTomato, v => v.buttonTomato.Enabled).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeSkip,
                    v => v.buttonSkip, nameof(Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeCheese,
                v => v.buttonCheese, nameof(Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeSalad,
                v => v.buttonSalad, nameof(Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeMeat,
                v => v.buttonMeat, nameof(Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.TakeTomato,
                v => v.buttonTomato, nameof(Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.ProcessMenuI,
                v => v.buttonMenuI, nameof(Click)).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.ProcessMenuII,
                v => v.buttonMenuII, nameof(Click)).DisposeWith(d);

                MessageBus.Current.Listen<string>("burger").ObserveOn(RxApp.MainThreadScheduler).Subscribe(c =>
                {
                    MessageBox.Show(c, "Your burger");
                }).DisposeWith(d);
            });
        }

        public MainViewModel ViewModel { get; set; }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            ViewModel = new MainViewModel(SynchronizationContext.Current);
            await ViewModel.ResetStateAsync();
        }
    }
}