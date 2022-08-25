using Basket.API.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using NUnit.Framework;

namespace Basket.API.UnitTests.Repositories.BasketRepositoryUnitTests
{
    public partial class BasketRepositoryTests
    {
        private readonly Mock<IDistributedCache> distributedCacheMock = new Mock<IDistributedCache>();
        private BasketRepository sytemUnderTest;


        [SetUp]
        public void Initialize()
        {
            this.sytemUnderTest = new BasketRepository(this.distributedCacheMock.Object);
        }
    }
}
