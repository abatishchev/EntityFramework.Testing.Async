[![NuGet](https://img.shields.io/nuget/v/EntityFramework.Testing.Async.svg)](https://www.nuget.org/packages/﻿EntityFramework.Testing.Async)

﻿EntityFramework.Testing.Async
===

A set of extensions to simplify the testing of async methods in Entity Framework and LINQ to Entities .


Examples
===

Let's you have the repository with a method returning `IQueryable<T>`:

```c#
public interface IRepository<T>
{
    IQueryable<T> GetAll();
}
```

It's implmentation using Entity Framework:

```c#
public class EntityOrderRepository : IRepository<Order>
{
    public IQueryable<Order> GetAll()
    {
	    return _db.Orders;
    }
}
```

You use it in an async manner:

```csharp
var repository = new EntityOrderRepository();
var orders = await repository.GetAll().ToArrayAsync();
```

Then you have mock it up in your unit test like this:

```csharp
var expected = new[] { new Order() };
var orderRepository = new Mock<IRepository<IOrder>>();
orderRepository.Setup(r => r.GetAll()).Returns(new AsyncEnumerableQuery<T>(expected)); // or roll out your own extension method
```

License
===

Licensed under the [MIT License](http://opensource.org/licenses/MIT)


Attribution
===

Based on [FakeDbSet<T>](https://gist.github.com/taschmidt/9663503) and [discussion #435590](http://entityframework.codeplex.com/discussions/435590)
