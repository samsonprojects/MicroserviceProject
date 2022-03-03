using System;
using System.Text;
using System.Text.Json;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Options.v1;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public class CustomerUpdateSender : ICustomerUpdateSender
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _queueName;
        private IConnection _connection;

        public CustomerUpdateSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();
        }

        public void SendCustomer(Customer customer)
        {
            
            if(ConnectionExists())
            {
                using(var channel = _connection.CreateModel())
                {
                    Console.WriteLine("creating queue");
                    channel.QueueDeclare(queue:_queueName,durable:false,exclusive:false,autoDelete:false,arguments:null);
                    var json = JsonSerializer.Serialize(customer);
                    var body = Encoding.UTF8.GetBytes(json);
                    Console.WriteLine("connection created");

                    channel.BasicPublish(exchange:"", routingKey:_queueName, basicProperties:null, body:body);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory() 
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                Console.WriteLine($"Rabbit Mq connection created, hostname is: {_hostname},{_username}");
                _connection = factory.CreateConnection();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if(_connection != null)
            {
                return true;
            }
            CreateConnection();
            return _connection != null;
        }
    }
}