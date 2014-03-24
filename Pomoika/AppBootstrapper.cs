using Ninject;

namespace Pomoika {
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;

    public class AppBootstrapper : BootstrapperBase {
        IKernel container;

        public AppBootstrapper() {
            Start();
        }

        protected override void Configure()
        {
            var t = typeof (Castle.Core.Configuration.IConfiguration);

            container = new StandardKernel();

            container.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            container.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            container.Bind<IShell>().To<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            var instance = container.Get(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAll(service);
        }

        protected override void BuildUp(object instance) {
            container.Inject(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }
    }
}