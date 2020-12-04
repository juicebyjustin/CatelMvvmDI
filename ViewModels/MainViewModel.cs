using Catel;
using Catel.Data;
using Catel.Fody;
using Catel.MVVM;
using CatelMvvmDI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatelMvvmDI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Service _service;
        public Command GetGuidCommand { get; private set; }

        public MainViewModel()
        {
            MainModel = new Main() { WindowTitle = "Hello World!" };

            GetGuidCommand = new Command(OnGetGuidCommandExecute);
        }

        public MainViewModel(Service service) : this()
        {
            Argument.IsNotNull(() => service);
            _service = service;

            MainModel.Guid = service.GenerateGuidAsString();
            MainModel.WindowTitle = "DI is working with MVVM.";
        }

        public override string Title { get { return "Main"; } }

        [Model]
        [Expose("Guid")]
        [Expose("WindowTitle")]
        public Main MainModel { get; set; }


        private void OnGetGuidCommandExecute()
        {
            if (_service is null)
                MainModel.Guid = "Service is null. DI is not working with MVVM.";
            else
                MainModel.Guid = _service.GenerateGuidAsString();
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: Add initialization logic like subscribing to events
        }

        protected override async Task CloseAsync()
        {
            // TODO: Add uninitialization logic like unsubscribing from events

            await base.CloseAsync();
        }
    }
}
