using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MySvc.Framework.Domain.Core;
using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Specification;
using MySvc.Framework.Infrastructure.Crosscutting.Exceptions;
using MySvc.Framework.Infrastructure.Crosscutting.Options;
using MySvc.Framework.Infrastructure.Data.MongoDB;
using MySvc.Framework.Infrastructure.Data.MongoDB.Impl;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.MongoDB.Tests
{
    public class PersonTest
    {
        private readonly string _connectionString = "mongodb://admin:admin123456@127.0.0.1:27017,127.0.0.1:27018,127.0.0.1:27019/?connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-1";
        private readonly string _dbName = "framework-core-test";
        private readonly IOptions<MongoDBSettings> _options;
        private IContainer _container;
        private IMediator _mediator;
        private readonly ITestOutputHelper _output;
        private readonly Mock<ILogger<MongoDBContext>> _mockLogger;
        private readonly IEntityIdGenerator _entityIdGenerator;

        public PersonTest(ITestOutputHelper output)
        {
            _options = Options.Create<MongoDBSettings>(new MongoDBSettings()
            {
                ConnectionString = _connectionString,
                Database = _dbName
            });
            _output = output;
            _entityIdGenerator = new EntityIdGenerator();

            Build();
            MongoDBContext.RegisterConventions();
            Mapping.Map();
            new MongoDBManager(_options).CreateCollections();

            _mockLogger = new Mock<ILogger<MongoDBContext>>();
            //ILogger 很多扩展方法，但是扩展方法无法moq，直接moq最底部
            _mockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<MongoDBContext>(),
                It.IsAny<Exception>(), It.IsAny<Func<MongoDBContext, Exception, string>>()));
            _mockLogger.Setup(m => m.IsEnabled(It.IsAny<LogLevel>())).Returns(true);

        }

        [Fact]
        public async Task Insert_Test()
        {

            var context = new MongoDBContext(_entityIdGenerator,_options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);
            Person person = new Employee("test") ;
            context.BeginTransaction();
            await personRepository.AddAsync(person);
            await context.CommitAsync();

            var person2 = await personRepository.GetByKeyAsync(person.Id);

            Assert.Equal(person2.Name, person.Name);

        }

        [Fact]
        public async Task Insert_EmployeeTest()
        {
            var context = new MongoDBContext(_entityIdGenerator, _options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);
            Person person = new Employee("test") { EmployeeNo = "1" };
            context.BeginTransaction();
            await personRepository.AddAsync(person);
            await context.CommitAsync();

            var person2 = await personRepository.GetByKeyAsync(person.Id);

            Assert.Equal((string)person2.Name, person.Name);

            var employee2 = person2 as Employee;
            Assert.NotNull(employee2);
            Assert.Equal("1", employee2.EmployeeNo);

        }

        [Fact]
        public async Task Insert_Employee_Version_Test()
        {
            var context = new MongoDBContext(_entityIdGenerator, _options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);
            Person person = new Employee("test") { EmployeeNo = "1", RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks) };
            context.BeginTransaction();
            await personRepository.AddAsync(person);
            await context.CommitAsync();


            var person2 = await personRepository.GetByKeyAsync(person.Id);


            Assert.NotNull(person2);
            Assert.Equal(person.RowVersion, person2.RowVersion);

        }

        [Fact]
        public async Task Insert_Employee_Version_Exception_Test()
        {
            var context = new MongoDBContext(_entityIdGenerator, _options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);
            Person person = new Employee("test");

            context.BeginTransaction();
            await personRepository.AddAsync(person);
            await context.CommitAsync();

            Person person1 = await personRepository.GetByKeyAsync(person.Id);
            byte[] version1 = person1.RowVersion;

            Person person2 = await personRepository.GetByKeyAsync(person.Id);
            byte[] version2 = person1.RowVersion;

            Assert.Equal(version1, version2);
            context.BeginTransaction();
            person1.Name1 = "hello";
            await personRepository.UpdateAsync(person1);
            await context.CommitAsync();


            person1 = await personRepository.GetByKeyAsync(person.Id);

            Assert.NotEqual(version1, person1.RowVersion);

            context.BeginTransaction();
            person2.Name1 = "concurrency";

            await Assert.ThrowsAnyAsync<ConcurrencyException>(() => personRepository.UpdateAsync(person2));
        }

        [Fact]
        public async Task Query_Test()
        {
            var context = new MongoDBContext(_entityIdGenerator, _options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);
            Person person = new Employee("test") ;

            context.BeginTransaction();
            await personRepository.AddAsync(person);
            await context.CommitAsync();

            var persons = await personRepository.GetListAsync(Specification<Person>.Eval(c => c.Name == "test"));
            Assert.True(persons.Any());

        }

        private void Build()
        {
            var services = new ServiceCollection()
                .AddLogging();


            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                //typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(PersonTest).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterInstance(_output).As<ITestOutputHelper>();


            // It appears Autofac returns the last registered types first
            //builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(GenericRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
            //builder.RegisterGeneric(typeof(GenericRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            //builder.RegisterGeneric(typeof(GenericPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ConstrainedRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));
            //builder.RegisterGeneric(typeof(ConstrainedPingedHandler<>)).As(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var container = builder.Build();

            // The below returns:
            //  - RequestPreProcessorBehavior
            //  - RequestPostProcessorBehavior
            //  - GenericPipelineBehavior

            //var behaviors = container
            //    .Resolve<IEnumerable<IPipelineBehavior<Ping, Pong>>>()
            //    .ToList();

            builder.Register<ServiceFactory>(ctx =>
           {
               var c = ctx.Resolve<IComponentContext>();
               return t => c.Resolve(t);
           });
            _mediator = container.Resolve<IMediator>();
            _container = container;
        }

        private List<string> GetCollectionNames()
        {
            List<string> names = new List<string>();
            var aggs = typeof(PersonTest).Assembly.GetTypes()
                .Where(c => c.IsPublic && c.IsClass && c.GetInterfaces().Contains(typeof(IAggregateRoot))).ToList();
            if (aggs.Any())
            {
                foreach (var item in aggs)
                {
                    var name = (item.GetTypeInfo()
                        .GetCustomAttributes(typeof(AggregateRootNameAttribute))
                        .FirstOrDefault() as AggregateRootNameAttribute)?.Name;
                    names.Add(name);
                }
            }

            return names;

        }

        [Fact]
        public async Task Query_Test_2()
        {
            var context = new MongoDBContext(_entityIdGenerator, _options, _mediator, _mockLogger.Object);
            var personRepository = new PersonRepository(context);



            var person2 = await personRepository.GetByKeyAsync(Guid.NewGuid().ToString());

        }

        [Fact]
        public async Task Query_Leader_Test_1()
        {
            var context = new MongoDBContext(_entityIdGenerator, _options, _mediator, _mockLogger.Object);
            var leaderReadOnlyRepository = new LeaderReadOnlyRepository(context);



            var person2 = await leaderReadOnlyRepository.GetByKeyAsync(Guid.NewGuid().ToString());

        }
    }
}
