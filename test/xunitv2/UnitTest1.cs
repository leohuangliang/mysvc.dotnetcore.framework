using Xunit;
using Xunit.Abstractions;
namespace xunitv2
{

    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void Test1()
        {
            Assert.True(true);
            _output.WriteLine($"开卡请求创建成功:");
        }
    }
}