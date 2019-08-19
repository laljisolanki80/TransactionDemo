using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace UnitTestRabbitMQ
{
    /// <summary>
    /// Summary description for RabbitMQUnitTest
    /// </summary>
    [TestClass]
    public class RabbitMQUnitTest
    {
        [Fact]
        public void SendMessageTest()
        {
            var FakeMessage = "Hello..Fakemessage";

        }

    }
}