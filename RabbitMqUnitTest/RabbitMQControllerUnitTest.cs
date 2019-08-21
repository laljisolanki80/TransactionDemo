using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.API.Controllers;
using Xunit;
using RabbitMQ.Client;
using Moq;
using EventBusRabbitMQ;

namespace RabbitMqUnitTest
{
    public class RabbitMQControllerUnitTest
    {
        private readonly RabbitMQController rabbitMQController;
        private Mock<IConnectionFactory> _connectionFactoryMock;

        public RabbitMQControllerUnitTest()
        {
            _connectionFactoryMock = new Mock<IConnectionFactory>();

        }

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

        [Fact]
        public void Recieve_Message()

        {
            //Arrange
            string fakemessage = "test";
            

            //Act
            var result1 = rabbitMQController.SendMessage(fakemessage);
            var result2 = rabbitMQController.ReceiveMessage();

            //Assert
            Assert.Equal(result1, result2);
        }
        
    }
}