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
        private Mock<IModel> mockChannel;
        private Mock<IConnection> mockConnection;

        public RabbitMQControllerUnitTest()
        {
            _connectionFactoryMock = new Mock<IConnectionFactory>();
            mockChannel = new Mock<IModel>();
            mockConnection = new Mock<IConnection>();
           IRabbitMQPersistentConnection mQPersistentConnection = new DefaultRabbitMQPersistentConnection(_connectionFactoryMock.Object);
            rabbitMQController = new RabbitMQController(mQPersistentConnection);
        }

        [Fact]
        public void Sendmessage_result_notnull_check()
        {
            //Arrange
            string fakemessage = "fake-message4";

            //Act
            var result = rabbitMQController.SendMessage(fakemessage);
            //Assert
            Assert.NotSame(result, null);
        }

        [Fact]
        public void Recieve_Message()

        {
            //Arrange
            mockConnection.Setup(m => m.CreateModel()).Returns(mockChannel.Object);
            //Act
            var result2 = rabbitMQController.ReceiveMessage();

            //Assert
            Assert.NotSame(result2, null); //Assert.Equal(result2, result1);
        }


    }
}