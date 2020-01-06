using System;
using Xunit;

namespace Domain.Core.Tests
{
    public class ValueObjectTest
    {
        [Fact]
        public void EqualsTest()
        {
            ContactInfo info1 = new ContactInfo("张三","1388888888","xxx@email.com",new Address("CN","广东","深圳","南山","世外桃源"));
            ContactInfo info2 = new ContactInfo("张三","1388888888","xxx@email.com",new Address("CN","广东","深圳","南山","世外桃源"));
            Assert.True(info1 == info2);

        }

        [Fact]
        public void Equals_Null_Test()
        {
            ContactInfo info1 = new ContactInfo("张三",null,"xxx@email.com",new Address("CN","广东","深圳","南山","世外桃源"));
            ContactInfo info2 = new ContactInfo("张三","1388888888","xxx@email.com",new Address("CN","广东","深圳","南山","世外桃源"));
            Assert.True(info1 != info2);

        }

        [Fact]
        public void Equals_Null_Test1()
        {
            ContactInfo info1 = new ContactInfo("张三", "1388888888", "xxx@email.com", new Address("CN", null, "深圳", "南山", "世外桃源"));
            ContactInfo info2 = new ContactInfo("张三", "1388888888", "xxx@email.com", new Address("CN", "广东", "深圳", "南山", "世外桃源"));
            Assert.True(info2 != info1);

        }

        [Fact]
        public void Equals_Null_Test_2()
        {
            ContactInfo info1 = new ContactInfo("张三", null, "xxx@email.com", new Address("CN", "广东", "深圳", "南山", "世外桃源"));
            ContactInfo info2 = new ContactInfo("张三", "1388888888", "xxx@email.com", new Address("CN", "广东", "深圳", "南山", "世外桃源"));
            Assert.True(info2 != info1);

        }

        [Fact]
        public void Equals_Test_3()
        {
            ContactInfo info1 = new ContactInfo("张三", "1388888888", "xxx@email.com", new Address("CN", null, "深圳", "南山", "世外桃源"));
            ContactInfo info2 = new ContactInfo("张三", "1388888888", "xxx@email.com", new Address("CN", null, "深圳", "南山", "世外桃源"));
            Assert.True(info1 == info2);

        }
    }
}
