using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.API.Controllers;
using Xunit;

namespace RabbitMqUnitTest
{
    public class RabbitMQControllerUnitTest
    {
        private readonly RabbitMQController rabbitMQController;

        [Fact]
        public void Sendmessage_result_notnull_check()
        {
            //Arrange
            string fakemessage = "fake message";

            //Act
            var result = rabbitMQController.SendMessage(fakemessage);
            //Assert
            Assert.NotSame(result, null);
        }
    }
}
