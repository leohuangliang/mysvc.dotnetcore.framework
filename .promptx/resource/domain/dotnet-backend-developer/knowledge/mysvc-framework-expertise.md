# MySvc.Framework专业知识体系

## 框架核心架构

### DDD分层架构
MySvc.Framework采用经典的DDD四层架构：

```
┌─────────────────────────────────────┐
│           Presentation Layer        │  ← API Controllers, Web UI
├─────────────────────────────────────┤
│           Application Layer         │  ← Application Services, DTOs
├─────────────────────────────────────┤
│             Domain Layer            │  ← Aggregates, Entities, Domain Services
├─────────────────────────────────────┤
│          Infrastructure Layer       │  ← Repositories, External Services
└─────────────────────────────────────┘
```

### 核心组件体系

#### 1. Domain.Core (8.0.0-beta4)
- **聚合根基类**：`AggregateRoot<TId>`
- **实体基类**：`Entity<TId>`
- **值对象基类**：`ValueObject`
- **Repository接口**：`IRepository<TEntity, TId>`
- **Specification模式**：完整的查询规范实现
- **领域事件**：`IDomainEvent`、`DomainEventDispatcher`
- **货币模型**：`Currency`、`Money`（支持ISO 4217标准）

#### 2. Infrastructure层组件
- **MongoDB集成**：`MongoRepository<TEntity, TId>`
- **Redis缓存**：分布式缓存支持
- **IdentityServer4**：多客户端认证授权
- **Serilog日志**：结构化日志记录
- **Hangfire作业**：后台任务调度
- **AutoMapper**：对象映射

#### 3. Crosscutting横切组件
- **EnumHelper**：枚举扩展方法
- **异常处理**：统一异常处理机制
- **事件总线**：领域事件发布订阅
- **IoC抽象**：依赖注入抽象层

## Specification模式深度应用

### 核心Specification类
```csharp
// 基础规范接口
public interface ISpecification<T>
{
    Expression<Func<T, bool>> GetExpression();
}

// 直接规范实现
public abstract class DirectSpecification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> GetExpression();
}

// 组合规范
public class AndSpecification<T> : CompositeSpecification<T>
{
    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        : base(left, right) { }
        
    public override Expression<Func<T, bool>> GetExpression()
    {
        return Left.GetExpression().And(Right.GetExpression());
    }
}
```

### null安全扩展方法
```csharp
// 安全And组合
public static ISpecification<T> And<T>(this ISpecification<T>? left, ISpecification<T> right)
{
    if (left == null) return right;
    return new AndSpecification<T>(left, right);
}

// 条件And组合
public static ISpecification<T> AndIf<T>(this ISpecification<T>? left, bool condition, ISpecification<T> right)
{
    if (!condition) return left ?? new AnySpecification<T>();
    return left.And(right);
}

// 表达式重载
public static ISpecification<T> And<T>(this ISpecification<T>? left, Expression<Func<T, bool>> expression)
{
    return left.And(new DirectSpecification<T>(expression));
}
```

### 实际应用模式
```csharp
// 1. 基础查询规范
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

// 2. 复杂查询组合
public class ProductQueryBuilder
{
    public static ISpecification<Product> BuildQuery(ProductSearchCriteria criteria)
    {
        ISpecification<Product>? spec = null;
        
        spec = spec.AndIf(!string.IsNullOrEmpty(criteria.Name), 
            new ProductByNameSpecification(criteria.Name));
            
        spec = spec.AndIf(criteria.MinPrice.HasValue,
            p => p.Price.Amount >= criteria.MinPrice.Value);
            
        spec = spec.AndIf(criteria.CategoryId.HasValue,
            p => p.CategoryId == criteria.CategoryId.Value);
            
        return spec ?? new AnySpecification<Product>();
    }
}

// 3. Repository中使用
public class ProductRepository : MongoRepository<Product, ProductId>
{
    public async Task<IEnumerable<Product>> FindBySpecificationAsync(ISpecification<Product> specification)
    {
        var expression = specification.GetExpression();
        return await Collection.Find(expression).ToListAsync();
    }
}
```

## Repository模式最佳实践

### 接口设计
```csharp
public interface IProductRepository : IRepository<Product, ProductId>
{
    // 基础CRUD继承自IRepository
    
    // 业务特定查询
    Task<Product?> FindBySkuAsync(string sku);
    Task<IEnumerable<Product>> FindBySpecificationAsync(ISpecification<Product> specification);
    Task<PagedResult<Product>> FindPagedAsync(ISpecification<Product> specification, int page, int size);
    
    // 聚合查询
    Task<int> CountBySpecificationAsync(ISpecification<Product> specification);
    Task<bool> ExistsAsync(ISpecification<Product> specification);
}
```

### MongoDB实现
```csharp
public class ProductRepository : MongoRepository<Product, ProductId>, IProductRepository
{
    public ProductRepository(IMongoDatabase database) : base(database) { }
    
    public async Task<Product?> FindBySkuAsync(string sku)
    {
        return await Collection.Find(p => p.Sku == sku).FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<Product>> FindBySpecificationAsync(ISpecification<Product> specification)
    {
        var expression = specification.GetExpression();
        return await Collection.Find(expression).ToListAsync();
    }
    
    public async Task<PagedResult<Product>> FindPagedAsync(ISpecification<Product> specification, int page, int size)
    {
        var expression = specification.GetExpression();
        var total = await Collection.CountDocumentsAsync(expression);
        var items = await Collection.Find(expression)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
            
        return new PagedResult<Product>(items, total, page, size);
    }
}
```

## 货币和金额处理

### Currency枚举
```csharp
public enum Currency
{
    USD = 840,  // 美元
    EUR = 978,  // 欧元
    CNY = 156,  // 人民币
    JPY = 392,  // 日元
    GBP = 826   // 英镑
}

// 扩展方法
public static class CurrencyExtensions
{
    public static string GetName(this Currency currency)
    {
        return currency switch
        {
            Currency.USD => "美元",
            Currency.EUR => "欧元",
            Currency.CNY => "人民币",
            Currency.JPY => "日元",
            Currency.GBP => "英镑",
            _ => currency.ToString()
        };
    }
    
    public static string GetSymbol(this Currency currency)
    {
        return currency switch
        {
            Currency.USD => "$",
            Currency.EUR => "€",
            Currency.CNY => "¥",
            Currency.JPY => "¥",
            Currency.GBP => "£",
            _ => currency.ToString()
        };
    }
}
```

### Money值对象
```csharp
public class Money : ValueObject
{
    public Currency Currency { get; private set; }
    public decimal Amount { get; private set; }
    
    public Money(Currency currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
    
    // 运算符重载
    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("不能对不同货币进行运算");
            
        return new Money(left.Currency, left.Amount + right.Amount);
    }
    
    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("不能对不同货币进行运算");
            
        return new Money(left.Currency, left.Amount - right.Amount);
    }
    
    // 比较操作
    public static bool operator >(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("不能比较不同货币");
            
        return left.Amount > right.Amount;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Amount;
    }
}
```

## 事件驱动架构

### 领域事件定义
```csharp
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    Guid EventId { get; }
}

public class ProductCreatedEvent : IDomainEvent
{
    public DateTime OccurredOn { get; }
    public Guid EventId { get; }
    public ProductId ProductId { get; }
    public string ProductName { get; }
    
    public ProductCreatedEvent(ProductId productId, string productName)
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ProductId = productId;
        ProductName = productName;
    }
}
```

### 事件处理器
```csharp
public interface IDomainEventHandler<in T> where T : IDomainEvent
{
    Task HandleAsync(T domainEvent);
}

public class ProductCreatedEventHandler : IDomainEventHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;
    
    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task HandleAsync(ProductCreatedEvent domainEvent)
    {
        _logger.LogInformation("Product created: {ProductId} - {ProductName}", 
            domainEvent.ProductId, domainEvent.ProductName);
            
        // 执行业务逻辑，如发送通知、更新缓存等
        await Task.CompletedTask;
    }
}
```

## 性能优化策略

### 1. 查询优化
- 使用Specification模式避免N+1查询
- 合理使用MongoDB索引
- 实现查询结果缓存

### 2. 内存管理
- 正确实现IDisposable接口
- 避免大对象堆分配
- 使用对象池减少GC压力

### 3. 异步编程
- 全面使用async/await模式
- 避免异步方法中的阻塞调用
- 合理配置TaskScheduler

### 4. 缓存策略
- 使用Redis分布式缓存
- 实现多级缓存架构
- 合理设置缓存过期策略

## 测试策略

### 单元测试
```csharp
[TestFixture]
public class ProductSpecificationTests
{
    [Test]
    public void ProductByNameSpecification_WithValidName_ShouldReturnCorrectExpression()
    {
        // Arrange
        var specification = new ProductByNameSpecification("iPhone");
        var products = new List<Product>
        {
            new Product(new ProductId("1"), "iPhone 15", new Money(Currency.USD, 999)),
            new Product(new ProductId("2"), "Samsung Galaxy", new Money(Currency.USD, 899))
        };
        
        // Act
        var expression = specification.GetExpression();
        var result = products.Where(expression.Compile()).ToList();
        
        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0].Name, Contains.Substring("iPhone"));
    }
}
```

### 集成测试
```csharp
[TestFixture]
public class ProductRepositoryIntegrationTests
{
    private IMongoDatabase _database;
    private ProductRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        _database = client.GetDatabase("test_db");
        _repository = new ProductRepository(_database);
    }
    
    [Test]
    public async Task FindBySpecificationAsync_WithValidSpecification_ShouldReturnProducts()
    {
        // Arrange
        var product = new Product(new ProductId("test-1"), "Test Product", new Money(Currency.USD, 100));
        await _repository.AddAsync(product);
        
        var specification = new ProductByNameSpecification("Test");
        
        // Act
        var results = await _repository.FindBySpecificationAsync(specification);
        
        // Assert
        Assert.That(results.Count(), Is.EqualTo(1));
        Assert.That(results.First().Name, Is.EqualTo("Test Product"));
    }
}
```

## 部署和运维

### NuGet包管理
- 使用MySvc.Framework官方包
- 定期更新到最新稳定版本
- 管理传递依赖冲突

### 监控和日志
- 配置Serilog结构化日志
- 使用APM工具监控性能
- 设置关键指标告警

### 安全最佳实践
- 启用HTTPS和安全头
- 实现JWT认证和授权
- 定期进行安全扫描 