using Basket.API.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace Basket.API.UnitTests.Repositories.BasketRepositoryUnitTests
{
    public partial class BasketRepositoryTests
    {
        [Test]
        [Category("Unit")]
        public void When_CreateInstance_And_ArgumentsAreNull_Then_ShouldThrowArgumentNullException()
        {
            // Act
            Action act = () =>
            {
                new BasketRepository(distributedCache: null);
            };

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
