using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;
using Xunit;

namespace EntityFramework.Testing.Async.Tests
{
	public class AsyncEnumerableQueryTest
	{
		[Fact]
		public void AsAsyncQueryable_Should_Return_AsyncEnumerableQuery()
		{
			// Arrange
			var seq = new[] { new object() };

			// Act
			var asyncSeq = new AsyncEnumerableQuery<object>(seq);

			// Assert
			asyncSeq.Should().BeOfType<AsyncEnumerableQuery<object>>();
		}

		[Fact]
		public void ToArrayAsync_Should_Throw_Exception()
		{
			// Arrange
			var seq = new[] { new object() };

			// Act
			Func<Task> action = async () => await seq.AsQueryable().ToArrayAsync();

			// Assert
			action.ShouldThrow<InvalidOperationException>();
		}

		[Fact]
		public void ToArrayAsync_Should_Not_Throw_Exception()
		{
			// Arrange
			var seq = new[] { new object() };

			// Act
			Func<Task> action = async () => await new AsyncEnumerableQuery<object>(seq).ToArrayAsync();

			// Assert
			action.ShouldNotThrow<InvalidOperationException>();
		}

		[Fact]
		public async Task ToArrayAsync_Should_Await_And_Not_Throw_Exception()
		{
			// Arrange
			var expected = new[] { new object() };

			// Act
			var actual = await new AsyncEnumerableQuery<object>(expected).ToArrayAsync();

			// Assert
			actual.ShouldBeEquivalentTo(expected);
		}
	}
}