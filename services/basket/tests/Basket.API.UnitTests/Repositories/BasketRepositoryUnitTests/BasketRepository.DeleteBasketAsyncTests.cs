using Moq;
using NUnit.Framework;

namespace Basket.API.UnitTests.Repositories.BasketRepositoryUnitTests
{
    public partial class BasketRepositoryTests
    {
        [Test]
        [Category("Unit")]
         public async Task When_DeleteBasketAsyncIsCalled_Then_ShouldDeleteBasket()

        {
            // Arrange
            var userName = "krazy8";
            var cancellationToken = CancellationToken.None;

            this.distributedCacheMock
                .Setup(x => x.RemoveAsync(userName, cancellationToken))
                .Returns(Task.CompletedTask);

            // Act
            await this.sytemUnderTest.DeleteBasketAsync(userName, cancellationToken);

            // Assert
            this.distributedCacheMock.Verify(x => x.RemoveAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
