﻿using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFramework.Testing.Async
{
	public class AsyncQueryProvider<T> : IDbAsyncQueryProvider
	{
		private readonly IQueryProvider _inner;

		public AsyncQueryProvider(IQueryProvider inner)
		{
			_inner = inner;
		}

		public IQueryable CreateQuery(Expression expression)
		{
			return new AsyncEnumerableQuery<T>(expression);
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			return new AsyncEnumerableQuery<TElement>(expression);
		}

		public object Execute(Expression expression)
		{
			return _inner.Execute(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
			return _inner.Execute<TResult>(expression);
		}

		public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Execute(expression));
		}

		public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			return Task.FromResult(Execute<TResult>(expression));
		}
	}
}