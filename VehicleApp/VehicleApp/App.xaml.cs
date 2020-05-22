using Autofac;
using Repository;
using Service;
using System;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace VehicleApp
{
    public partial class App : Application
    {
       public static IContainer Container { get; set; }
        static readonly ContainerBuilder builder = new ContainerBuilder();
        public App()
        {
         
            builder.RegisterType<VehicleMake>().As<IVehicleMake>();
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();

            builder.RegisterType<VehicleModel>().As<IVehicleModel>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();

            builder.RegisterType<VehicleModelViewModel>().As<IVehicleModelViewModel>()
                .WithParameter(new TypedParameter(typeof(string), "VehicleMake"));

            builder.RegisterType<VehicleMakeViewModel>().As<IVehicleMakeViewModel>();

            //builder.Register(t => new VehicleMakeViewModel());
            builder.Register(t => new VehicleMakeService(t.Resolve<IVehicleMake>())) ;

            //builder.Register(t => new VehicleModelViewModel(t.Resolve<IVehicleModelService>()));
            builder.Register(t => new VehicleModelService(t.Resolve<IVehicleModel>()));

           



            Container = builder.Build();

            DependencyResolver.ResolveUsing(t => Container.IsRegistered(t) ? Container.Resolve(t) : null);

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static void RegisterType<T>() where T : class
        {
            builder.RegisterType<T>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
