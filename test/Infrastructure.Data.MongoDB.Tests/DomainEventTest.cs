using Autofac.Extensions.DependencyInjection;
using Castle.Core.Configuration;
using global::Autofac;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Infrastructure.Crosscutting.Options;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Data.MongoDB.Tests
{
    public class DomainEventTest
    {
        //private readonly string _connectionString = "mongodb://admin:admin123456@127.0.0.1:27017/admin?replicaSet=rs0&authSource=admin";
        private readonly string _connectionString = "mongodb://admin:admin123456@127.0.0.1:27017,127.0.0.1:27018,127.0.0.1:27019/?connect=replicaSet";
        private readonly string _dbName = "framework-core-test";
        private readonly ITestOutputHelper _output;
        private readonly IMediator _mediator;
        private readonly Mock<ILogger<MongoDBContext>> _mockLogger;
        private readonly IEntityIdGenerator _entityIdGenerator;
        public DomainEventTest(ITestOutputHelper output)
        {
            _output = output;
            _mediator = BuildMediator();
            _entityIdGenerator = new EntityIdGenerator();
            _mockLogger = new Mock<ILogger<MongoDBContext>>();
            //ILogger 很多扩展方法，但是扩展方法无法moq，直接moq最底部
            _mockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<MongoDBContext>(),
                It.IsAny<Exception>(), It.IsAny<Func<MongoDBContext, Exception, string>>()));
            _mockLogger.Setup(m => m.IsEnabled(It.IsAny<LogLevel>())).Returns(true);

        }

        [Fact]
        public async Task Insert_Test()
        {
            IOptions<MongoDBSettings> options = Options.Create<MongoDBSettings>(new MongoDBSettings()
            {
                ConnectionString = _connectionString,
                Database = _dbName
            });

            var context = new MongoDBContext(_entityIdGenerator, options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);
            Employee employee = new Employee("Employee1") { EmployeeNo = "1" };
            context.BeginTransaction();
            await personRepository.AddAsync(employee);
            await context.CommitAsync();

            context.BeginTransaction();
            var employee2 = await personRepository.GetByKeyAsync(employee.Id) as Employee;
            employee2.Dimission();
            await personRepository.UpdateAsync(employee2);
            await context.CommitAsync();

        }

        //private IMediator BuildMediator()
        //{
        //    var builder = new ContainerBuilder();
        //    builder.
        //    builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
        //    builder.RegisterMediatR(configuration);

        //    var mediatrOpenTypes = new[]
        //    {
        //        //typeof(IRequestHandler<,>),
        //        typeof(INotificationHandler<>),
        //    };

        //    foreach (var mediatrOpenType in mediatrOpenTypes)
        //    {
        //        builder
        //            .RegisterAssemblyTypes(typeof(EmployeeDismmisionDomainEvent).GetTypeInfo().Assembly)
        //            .AsClosedTypesOf(mediatrOpenType)
        //            .AsImplementedInterfaces();
        //    }

        //    builder.RegisterInstance(_output).As<ITestOutputHelper>();

        //    // It appears Autofac returns the last registered types first
        //    //builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        //    //builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        //    //builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
        //    //builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
        //    //builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        //    //builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
        //    //builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));

        //    //builder.Register<ServiceFactory>(ctx =>
        //    //{
        //    //    var c = ctx.Resolve<IComponentContext>();
        //    //    return t => c.Resolve(t);
        //    //});

        //    var container = builder.Build();

        //    // The below returns:
        //    //  - RequestPreProcessorBehavior
        //    //  - RequestPostProcessorBehavior
        //    //  - GenericPipelineBehavior

        //    //var behaviors = container
        //    //    .Resolve<IEnumerable<IPipelineBehavior<Ping, Pong>>>()
        //    //    .ToList();

        //    var mediator = container.Resolve<IMediator>();

        //    return mediator;
        //}

        private IMediator BuildMediator()
        {

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(INotificationHandler<>),
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
                typeof(IStreamRequestHandler<,>)
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(EmployeeDismmisionDomainEvent).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    // when having a single class implementing several handler types
                    // this call will cause a handler to be called twice
                    // in general you should try to avoid having a class implementing for instance `IRequestHandler<,>` and `INotificationHandler<>`
                    // the other option would be to remove this call
                    // see also https://github.com/jbogard/MediatR/issues/462
                    .AsImplementedInterfaces();
            }


            builder.RegisterInstance(_output).As<ITestOutputHelper>();
            // It appears Autofac returns the last registered types first
            // builder.RegisterGeneric(typeof(GenericStreamPipelineBehavior<,>)).As(typeof(IStreamPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
            //builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            //builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            //builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));


            var services = new ServiceCollection();

            builder.Populate(services);

            // The below returns:
            //  - RequestPreProcessorBehavior
            //  - RequestPostProcessorBehavior
            //  - GenericPipelineBehavior
            //  - GenericStreamPipelineBehavior
            //  - RequestExceptionActionProcessorBehavior
            //  - RequestExceptionProcessorBehavior

            //var behaviors = container
            //    .Resolve<IEnumerable<IPipelineBehavior<Ping, Pong>>>()
            //    .ToList();

            var container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(container);
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            return mediator;
        }
    }
}
