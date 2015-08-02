using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace EntityFramework.Testing.Async
{
	public class AsyncEnumerableQuery<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
	{
		public AsyncEnumerableQuery(IEnumerable<T> enumerable)
			: base(enumerable)
		{
		}

		public AsyncEnumerableQuery(Expression expression)
			: base(expression)
		{
		}

		public IDbAsyncEnumerator<T> GetAsyncEnumerator()
		{
			return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
		}

		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return GetAsyncEnumerator();
		}

		IQueryProvider IQueryable.Provider
		{
			get { return new AsyncQueryProvider<T>(this); }
		}
	}
}