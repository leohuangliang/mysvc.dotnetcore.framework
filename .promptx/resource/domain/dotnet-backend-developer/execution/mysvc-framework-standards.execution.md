<execution>
  <constraint>
    ## MySvc.Framework技术约束
    - **框架版本固定**：当前基于.NET 8.0和MySvc.Framework 8.0.0-beta4
    - **DDD架构约束**：必须遵循领域驱动设计的分层架构原则
    - **MongoDB优先**：数据存储优先使用MongoDB，遵循框架的数据访问模式
    - **Specification模式**：查询逻辑必须使用框架提供的Specification模式
    - **NuGet包依赖**：只能使用框架官方提供的NuGet包和经过验证的第三方包
    - **null安全要求**：必须启用nullable引用类型，遵循框架的null安全实践
  </constraint>

  <rule>
    ## MySvc.Framework强制规则
    - **聚合根继承**：所有聚合根必须继承框架提供的基类
    - **Repository实现**：数据访问必须通过Repository模式，不得直接操作数据库
    - **Specification组合**：复杂查询必须使用And/Or/AndIf等扩展方法进行安全组合
    - **事件驱动**：领域事件必须通过框架的事件总线机制处理
    - **依赖注入**：所有服务必须通过框架的IoC容器注册和解析
    - **异常处理**：必须使用框架提供的异常处理机制，不得抛出原始异常
    - **日志记录**：必须使用框架集成的Serilog进行结构化日志记录
  </rule>

  <guideline>
    ## MySvc.Framework开发指导
    - **代码组织**：按照DDD分层架构组织代码，清晰分离领域层、应用层、基础设施层
    - **命名规范**：遵循.NET命名约定，使用有意义的英文命名，避免缩写
    - **扩展优先**：优先使用框架提供的扩展方法，如EnumHelper、SpecificationExtensions
    - **性能考虑**：合理使用框架的缓存机制，避免不必要的数据库查询
    - **测试驱动**：为所有业务逻辑编写单元测试，使用框架提供的测试基类
    - **文档维护**：及时更新API文档和架构文档，保持与代码同步
  </guideline>

  <process>
    ## MySvc.Framework开发流程

    ### Phase 1: 需求分析和设计
    1. **领域建模**：
       - 识别聚合根、实体、值对象
       - 定义领域服务和领域事件
       - 设计Repository接口

    2. **Specification设计**：
       - 分析查询需求，设计Specification类
       - 使用框架的null安全扩展方法
       - 编写Specification单元测试

    3. **API设计**：
       - 设计RESTful API接口
       - 定义请求/响应模型
       - 规划异常处理策略

    ### Phase 2: 核心开发实现
    1. **领域层实现**：
       ```csharp
       // 聚合根示例
       public class Product : AggregateRoot<ProductId>
       {
           public string Name { get; private set; } = string.Empty;
           public Money Price { get; private set; } = null!;
           
           protected Product() { } // 框架要求的空构造函数
           
           public Product(ProductId id, string name, Money price)
           {
               Id = id ?? throw new ArgumentNullException(nameof(id));
               Name = name ?? throw new ArgumentNullException(nameof(name));
               Price = price ?? throw new ArgumentNullException(nameof(price));
           }
       }
       ```

    2. **Repository实现**：
       ```csharp
       // Repository接口
       public interface IProductRepository : IRepository<Product, ProductId>
       {
           Task<IEnumerable<Product>> FindBySpecificationAsync(ISpecification<Product> spec);
       }
       
       // MongoDB实现
       public class ProductRepository : MongoRepository<Product, ProductId>, IProductRepository
       {
           public async Task<IEnumerable<Product>> FindBySpecificationAsync(ISpecification<Product> spec)
           {
               return await FindAsync(spec.GetExpression());
           }
       }
       ```

    3. **Specification实现**：
       ```csharp
       public class ProductByNameSpecification : DirectSpecification<Product>
       {
           private readonly string _name;
           
           public ProductByNameSpecification(string name)
           {
               _name = name ?? throw new ArgumentNullException(nameof(name));
           }
           
           public override Expression<Func<Product, bool>> GetExpression()
           {
               return product => product.Name.Contains(_name);
           }
       }
       
       // 安全组合使用
       var spec = new ProductByNameSpecification("iPhone")
           .And(p => p.Price.Amount > 1000)
           .AndIf(categoryId.HasValue, p => p.CategoryId == categoryId.Value);
       ```

    ### Phase 3: 应用层和API实现
    1. **应用服务**：
       ```csharp
       public class ProductApplicationService
       {
           private readonly IProductRepository _repository;
           private readonly ILogger<ProductApplicationService> _logger;
           
           public async Task<ProductDto> GetProductAsync(ProductId id)
           {
               var product = await _repository.GetByIdAsync(id);
               if (product == null)
                   throw new ProductNotFoundException(id);
               
               _logger.LogInformation("Product {ProductId} retrieved", id);
               return product.ToDto();
           }
       }
       ```

    2. **API控制器**：
       ```csharp
       [ApiController]
       [Route("api/[controller]")]
       public class ProductsController : ControllerBase
       {
           private readonly ProductApplicationService _service;
           
           [HttpGet("{id}")]
           public async Task<ActionResult<ProductDto>> GetProduct(string id)
           {
               var productId = new ProductId(id);
               var product = await _service.GetProductAsync(productId);
               return Ok(product);
           }
       }
       ```

    ### Phase 4: 测试和质量保证
    1. **单元测试**：
       ```csharp
       [Test]
       public void ProductByNameSpecification_ShouldFilterCorrectly()
       {
           // Arrange
           var spec = new ProductByNameSpecification("iPhone");
           var products = new List<Product>
           {
               new Product(new ProductId("1"), "iPhone 15", new Money(Currency.USD, 999)),
               new Product(new ProductId("2"), "Samsung Galaxy", new Money(Currency.USD, 899))
           };
           
           // Act
           var result = products.Where(spec.GetExpression().Compile()).ToList();
           
           // Assert
           Assert.That(result.Count, Is.EqualTo(1));
           Assert.That(result[0].Name, Is.EqualTo("iPhone 15"));
       }
       ```

    2. **集成测试**：
       - 使用框架提供的测试基类
       - 测试Repository和数据库交互
       - 测试API端到端功能

    ### Phase 5: 部署和监控
    1. **NuGet包管理**：
       - 确保使用框架官方NuGet包
       - 定期更新到最新稳定版本
       - 管理包依赖冲突

    2. **监控配置**：
       - 配置Serilog结构化日志
       - 设置性能监控指标
       - 配置异常告警机制
  </process>

  <criteria>
    ## MySvc.Framework质量标准

    ### 代码质量标准
    - ✅ 所有CS8618警告已修复，正确使用nullable引用类型
    - ✅ 遵循DDD分层架构，职责分离清晰
    - ✅ 正确使用框架提供的基类和扩展方法
    - ✅ 异常处理完整，使用框架异常处理机制
    - ✅ 日志记录规范，使用结构化日志格式

    ### Specification模式标准
    - ✅ 查询逻辑封装在Specification类中
    - ✅ 使用null安全的And/Or/AndIf扩展方法
    - ✅ Specification类有完整的单元测试覆盖
    - ✅ 复杂查询通过Specification组合实现
    - ✅ 避免在Repository中写复杂查询逻辑

    ### Repository模式标准
    - ✅ 所有数据访问通过Repository接口
    - ✅ Repository实现继承框架基类
    - ✅ 支持Specification模式查询
    - ✅ 正确处理并发和事务
    - ✅ 有完整的集成测试覆盖

    ### API设计标准
    - ✅ RESTful API设计规范
    - ✅ 统一的错误处理和响应格式
    - ✅ 完整的API文档和示例
    - ✅ 合理的缓存和性能优化
    - ✅ 安全认证和授权机制

    ### 测试覆盖标准
    - ✅ 单元测试覆盖率 ≥ 80%
    - ✅ 所有Specification类有测试
    - ✅ 关键业务逻辑有集成测试
    - ✅ API端到端测试覆盖主要场景
    - ✅ 性能测试验证关键指标
  </criteria>
</execution> 