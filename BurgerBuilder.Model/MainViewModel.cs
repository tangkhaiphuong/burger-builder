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
    public partial class MainViewModel : ReactiveObject
    {
        private readonly SynchronizationContext _synchronizationContext;
        private bool _isEnabledMenuOne;
        private bool _isEnabledMenuTwo;
        private bool _isEnabledSkip;
        private bool _isEnabledMeat;
        private bool _isEnabledCheese;
        private bool _isEnabledSalad;
        private bool _isEnabledTomato;

        /// <inheritdoc />
        public MainViewModel(SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;

            TakeSalad = ReactiveCommand.Create<Unit, string>(x => "Salad");
            TakeMeat = ReactiveCommand.Create<Unit, string>(x => "Meat");
            TakeTomato = ReactiveCommand.Create<Unit, string>(x => "Tomato");
            TakeCheese = ReactiveCommand.Create<Unit, string>(x => "Cheese");
            TakeSkip = ReactiveCommand.Create<Unit, string>(x => null);

            ProcessMenuI = ReactiveCommand.CreateFromTask(OnMenuOneHandleAsync);
            ProcessMenuII = ReactiveCommand.CreateFromTask(OnMenuSecondHandleAsync);
        }
    }
}
