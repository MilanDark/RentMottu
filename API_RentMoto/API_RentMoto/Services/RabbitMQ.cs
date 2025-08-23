
using API_RentMoto.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;
using System.Threading;

namespace API_RentMoto.Services
{
    public class RabbitMQ : IRabbitMQ
    {

        #region Propriedades
        //Host
        private string _RabbitMQHost;
        private string RabbitMQHost
        {
            get
            {
                if (string.IsNullOrEmpty(_RabbitMQHost))
                {
                    _RabbitMQHost = ConfigurationManager.AppSettings["RabbitMQ.Host"];
                }
                return _RabbitMQHost;
            }
            set
            {
                _RabbitMQHost = value;
            }
        }
        //VirtualHost
        private string _RabbitMQVritualHost;
        private string RabbitMQVirtualHost
        {
            get
            {
                if (string.IsNullOrEmpty(_RabbitMQHost))
                {
                    _RabbitMQVritualHost = ConfigurationManager.AppSettings["RabbitMQ.VirtualHost"];
                }
                return _RabbitMQVritualHost;
            }
            set
            {
                _RabbitMQVritualHost = value;
            }
        }
        //Usuario
        private string _RabbitMQUser;
        private string RabbitMQUser
        {
            get
            {
                if (string.IsNullOrEmpty(_RabbitMQUser))
                {
                    _RabbitMQUser = ConfigurationManager.AppSettings["RabbitMQ.User"];
                }
                return _RabbitMQUser;
            }
            set
            {
                _RabbitMQUser = value;
            }
        }
        //Senha
        private string _RabbitMQPassword;
        private string RabbitMQPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_RabbitMQPassword))
                {
                    _RabbitMQPassword = ConfigurationManager.AppSettings["RabbitMQ.Password"];
                }
                return _RabbitMQPassword;
            }
            set
            {
                _RabbitMQPassword = value;
            }
        }
        //port
        private string _RabbitMQPort;
        private string RabbitMQPort
        {
            get
            {
                if (string.IsNullOrEmpty(_RabbitMQPort))
                {
                    _RabbitMQPort = ConfigurationManager.AppSettings["RabbitMQ.Port"];
                }
                return _RabbitMQPort;
            }
            set
            {
                _RabbitMQPort = value;
            }
        }
        //Rabbit Usage flag
        private bool _isRabbit = true;
        public bool isRabbit
        {
            get
            {
                _isRabbit = Boolean.Parse(ConfigurationManager.AppSettings["RabbitMQ"]);
                return _isRabbit;
            }
            set
            {
                _isRabbit = Convert.ToBoolean(value);
            }
        }
        //Topico
        private string _TopicName;
        public string TopicName
        {
            get
            {
                if (string.IsNullOrEmpty(_TopicName))
                {
                    _TopicName = ConfigurationManager.AppSettings["RabbitMQ.Topic"];
                }
                return _TopicName;

            }
            set
            {
                _TopicName = value;
            }
        }

        public ConnectionFactory connectionFactory;
        public IConnection connection;

        #endregion

        public RabbitMQ()
        {
            GetRabbitMQNamespaceAndCredentials();
            CreateFactory();
        }

        #region Rabbit
        private void GetRabbitMQNamespaceAndCredentials()
        {
            try
            {
                var conexao = ConfigurationManager.AppSettings["RabbitMQ.ConnectionString"];
                RabbitMQHost = conexao;

                var virtualHost = ConfigurationManager.AppSettings["RabbitMQ.VirtualHost"];
                if (string.IsNullOrEmpty(virtualHost))
                {
                    virtualHost = null;
                }
                RabbitMQVirtualHost = virtualHost;

                var user = ConfigurationManager.AppSettings["RabbitMQ.User"];
                RabbitMQUser = user;

                var password = ConfigurationManager.AppSettings["RabbitMQ.Password"];
                if (string.IsNullOrEmpty(password))
                    RabbitMQPassword = password;

                var port = ConfigurationManager.AppSettings["RabbitMQ.Port"];
                RabbitMQPort = port;
            }
            catch (Exception e)
            {
            }

        }

        private void CreateFactory()
        {
            if (connectionFactory == null)
            {
                connectionFactory = new ConnectionFactory()
                {
                    HostName = RabbitMQHost,
                    UserName = RabbitMQUser,
                    Password = RabbitMQPassword,
                    VirtualHost = "/",
                    Port = 5672
                };

                var uri = string.Concat("amqp://", RabbitMQUser, ":", RabbitMQPassword, "@", RabbitMQHost, ":", RabbitMQPort, "/");

                if (!string.IsNullOrEmpty(RabbitMQVirtualHost))
                {
                    connectionFactory.VirtualHost = RabbitMQVirtualHost;
                    uri = string.Concat(uri, RabbitMQVirtualHost);
                }
                connectionFactory.Uri = uri; // connectionFactory.Uri = new Uri(uri); alterado par aajuste
            }
        }

        private void CreateFactory(string connectionString)
        {
            connectionFactory.Uri = connectionString; //new Uri(connectionString);  //alterado par aajuste
        }

        public void CreateConnection()
        {
            retentar:
            try
            {
                if (connection == null)
                    connection = connectionFactory.CreateConnection();
            }
            catch (Exception e)
            {
                System.Threading.Thread.Sleep(15000);
                goto retentar;
            }
        }

        public void CreateConnection(string connectionString)
        {
            if (connection == null)
            {
                CreateFactory(connectionString);
                connection = connectionFactory.CreateConnection();
            }
        }

        public void CreateInfrastructure(string queueName)
        {
            try
            {
                try
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclarePassive(TopicName.ToString());
                        QueueDeclareOk ok = channel.QueueDeclarePassive(queueName);
                        if (ok.MessageCount > 0)
                        {
                            channel.QueueBind(queueName, TopicName, string.Empty);
                        }
                    }
                }
                catch
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(TopicName.ToString(), ExchangeType.Topic, true);
                        channel.QueueDeclare(queueName, true, false, false, null);
                        channel.QueueBind(queueName, TopicName, queueName, null);
                    }
                }

            }
            catch (Exception e)
            {
                // NewsGPS.Common.Log.Log.PublicarTrace(e.ToString(), TopicName, true);
            }
        }

        public void Publish(string message, string queueName)
        {
            try
            {
                if (!connection.IsOpen)
                    CreateConnection();

                var body = Encoding.UTF8.GetBytes(message);
                using (var channel = connection.CreateModel())
                {
                    channel.BasicPublish(exchange: TopicName,
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);
                }
            }
            catch (Exception e)
            {
                // NewsGPS.Common.Log.Log.PublicarTrace(e.ToString(), TopicName, true);
            }
        }

        public string Receive(string queueName)
        {
            string model = null;
            try
            {
                using (IModel channel = connection.CreateModel())
                {
                    //channel.QueueDeclare(queue, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    BasicGetResult result = channel.BasicGet(queueName, true);
                    if (result != null)
                    {
                        model = Encoding.UTF8.GetString(result.Body);  //ToArray()); alterado para ajuste
                    }
                }
            }
            catch (Exception e)
            {
                //throw e;
            }
            return model;
        }

        public void closeConnection()
        {
            try
            {
                connection.Close();
            }
            catch
            {

            }
        }

        #endregion



        #region Controles

        public Moto Queue_Moto_Read()
        {
            Moto log = new Moto();
            try
            {
                var rQueue = new API_RentMoto.Services.RabbitMQ();
                var nomeFila = "Motos_Adicionadas";
                rQueue.TopicName = "RentMoto";
                rQueue.CreateConnection();
                rQueue.CreateInfrastructure(nomeFila);
                string receivedMessage = "";

                receivedMessage = rQueue.Receive(nomeFila);

                Thread.Sleep(500);
                if (receivedMessage != null)
                    log = JsonConvert.DeserializeObject<Moto>(receivedMessage);


                rQueue.closeConnection();
                return log;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    


        public bool Queue_Moto_Add(string texto, string nomeFila)
        {

            var rQueue = new RabbitMQ();
            rQueue.TopicName = "RentMoto";
            rQueue.CreateConnection();
            Thread.Sleep(500);
            rQueue.CreateInfrastructure(nomeFila);
            try
            {
                rQueue.Publish(texto, nomeFila);
                Thread.Sleep(500);
                rQueue.closeConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        #endregion
    }
}


