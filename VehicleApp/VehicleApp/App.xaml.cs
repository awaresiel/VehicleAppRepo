using Autofac;
using AutoMapper;
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

        public static IMapper Mapper { get; set; }
        public App()
        {
            
            RegisterAutofac();

            Mapper = Container.Resolve<IMapper>();


            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

       
        private void RegisterAutofac()
        {
            builder.RegisterType<VehicleMake>().As<IVehicleMake>();
            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();

            builder.RegisterType<VehicleModel>().As<IVehicleModel>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();

            builder.RegisterType<VehicleModelViewModel>().As<IVehicleModelViewModel>()
                .WithParameter(new TypedParameter(typeof(string), "VehicleMake"));

            builder.RegisterType<VehicleMakeViewModel>().As<IVehicleMakeViewModel>();

            builder.Register(t => new VehicleMakeService(t.Resolve<IVehicleMake>()));

            builder.Register(t => new VehicleModelService(t.Resolve<IVehicleModel>()));

            //register automapper
            builder.Register(ctx => new MapperConfiguration(cfg =>
            { cfg.CreateMap<VehicleMake, VehicleMakeViewModel>(); 
              cfg.CreateMap<VehicleMakeViewModel, VehicleMake>(); 
              
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
       .As<IMapper>()
       .InstancePerLifetimeScope();

            Container = builder.Build();

            DependencyResolver.ResolveUsing(t => Container.IsRegistered(t) ? Container.Resolve(t) : null);

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
