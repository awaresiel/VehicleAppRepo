using Autofac;
using Autofac.Core;
using AutoMapper;
using Repository;
using Service;
using SQLite;
using System;
using System.IO;
using VehicleApp.Repository;
using VehicleApp.UI;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
namespace VehicleApp
{
    public partial class App : Application
    {
        public static IContainer Container { get; set; }
        static readonly ContainerBuilder builder = new ContainerBuilder();
        public static IMapper Mapper { get; set; }
        public static string repositoryPath = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), "VehicleApp.db3");
        public App()
        {
            RegisterAutofac();
            Mapper = Container.Resolve<IMapper>();
            InitializeComponent();
            using (var scope = Container.BeginLifetimeScope())
            {
                var page = Container.Resolve<MainPage>();
                MainPage = new NavigationPage(page);
            }
        }
        private void RegisterAutofac()
        {
            builder.Register(ctx=> {
               var r= new SQLiteAsyncConnection(repositoryPath);  r.CreateTablesAsync<VehicleMakeEntity,VehicleModelEntity>().Wait();
                return r;
           } ).AsSelf().SingleInstance();
            builder.Register(ctx => new VehicleMakeRepository(ctx.Resolve<SQLiteAsyncConnection>())).As<Irepository<VehicleMakeEntity>>().SingleInstance();
            builder.Register(ctx => new VehicleModelRepository(ctx.Resolve<SQLiteAsyncConnection>())).As<Irepository<VehicleModelEntity>>().SingleInstance();
            builder.Register(ctx => new VehicleMakeService(ctx.Resolve<Irepository<VehicleMakeEntity>>())).As<IVehicleMakeService>();
            builder.Register(ctx => new VehicleModelService(ctx.Resolve<Irepository<VehicleModelEntity>>())).As<IVehicleModelService>();
            builder.Register(ctx => new VehicleMakeViewModel(ctx.Resolve<IVehicleMakeService>(), ctx.Resolve<IVehicleModelService>())).As<IViewModel<VehicleMake>>();
            builder.Register((ctx,parm) =>
            new VehicleModelViewModel(parm.Named<VehicleMake>("VehicleName"), ctx.Resolve<IVehicleMakeService>(), ctx.Resolve<IVehicleModelService>())
            ).As<IViewModel<VehicleModel>>();
            
            builder.Register(ctx => new MainPage(ctx.Resolve<IViewModel<VehicleMake>>()));
            builder.RegisterType<PageMakeVehicle>();
            builder.RegisterType<PageMakeVehicleModel>();
            builder.RegisterType<VehicleModelPage>();
            builder.RegisterType<VehicleMake>();
            builder.RegisterType<VehicleModel>();

            //register automapper
            builder.Register(ctx => new MapperConfiguration(cfg =>
            { cfg.CreateMap<VehicleMake, VehicleMakeEntity>(); 
              cfg.CreateMap<VehicleMakeEntity, VehicleMake>(); 

              cfg.CreateMap<VehicleModelEntity, VehicleModel>(); 
              cfg.CreateMap<VehicleModel, VehicleModelEntity>(); 
            }
            )).AsSelf();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
       .As<IMapper>()
       .InstancePerLifetimeScope();
            if(Container ==null)
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
