using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading;
using ReactiveUI;

namespace BurgerBuilder.Model
{
    /// <inheritdoc />
    public partial class MainViewModel
    {
        public bool IsEnabledMenuI
        {
            get => _isEnabledMenuOne;
            set => this.RaiseAndSetIfChanged(ref _isEnabledMenuOne, value);
        }

        public bool IsEnabledMenuII
        {
            get => _isEnabledMenuTwo;
            set => this.RaiseAndSetIfChanged(ref _isEnabledMenuTwo, value);
        }

        public bool IsEnabledSkip
        {
            get => _isEnabledSkip;
            set => this.RaiseAndSetIfChanged(ref _isEnabledSkip, value);
        }

        public bool IsEnabledMeat
        {
            get => _isEnabledMeat;
            set => this.RaiseAndSetIfChanged(ref _isEnabledMeat, value);
        }

        public bool IsEnabledCheese
        {
            get => _isEnabledCheese;
            set => this.RaiseAndSetIfChanged(ref _isEnabledCheese, value);
        }

        public bool IsEnabledSalad
        {
            get => _isEnabledSalad;
            set => this.RaiseAndSetIfChanged(ref _isEnabledSalad, value);
        }

        public bool IsEnabledTomato
        {
            get => _isEnabledTomato;
            set => this.RaiseAndSetIfChanged(ref _isEnabledTomato, value);
        }

        public ReactiveCommand<Unit, string> TakeMeat { get;  }
        public ReactiveCommand<Unit, string> TakeTomato { get; }
        public ReactiveCommand<Unit, string> TakeSalad { get;  }
        public ReactiveCommand<Unit, string> TakeCheese { get; }
        public ReactiveCommand<Unit, string> TakeSkip { get;  }

        public ReactiveCommand<Unit, Unit> ProcessMenuI { get; }
        public ReactiveCommand<Unit, Unit> ProcessMenuII { get; }
    }
}
