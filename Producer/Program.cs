using Confluent.Kafka;
using Domain;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Producer
{
  public class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      Publish().Wait();

      Console.ReadKey();
    }

    public static async Task Publish()
    {
      try
      {

        string brokerList = "crsintegration.servicebus.windows.net:9093";
        string connectionString = "Endpoint=sb://crsintegration.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=eAThLxCDoL1vb2G+0ag6DWz4862yu/vwy+AEhCk9R14=";
        string topic = "crsintegration.servicebus.windows.net";

        ProducerConfig config = new ProducerConfig
        {
          BootstrapServers = brokerList,
          SecurityProtocol = SecurityProtocol.SaslSsl,
          SaslMechanism = SaslMechanism.Plain,
          SaslUsername = "$ConnectionString",
          SaslPassword = connectionString
        };

        using (IProducer<long, string> producer = new ProducerBuilder<long, string>(config).SetKeySerializer(Serializers.Int64).SetValueSerializer(Serializers.Utf8).Build())
        {

          //This will then be replaced by Data from CRS. 
          Console.WriteLine($"Sending 5 messages to topic: {topic}, broker(s): {brokerList}");
          for (int x = 0; x < 5; x++)
          {
            string msg = $"Sample message {x} sent at {DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss.ffff")}";
            DeliveryResult<long, string> deliveryReport = await producer.ProduceAsync(topic, new Message<long, string> { Key = DateTime.UtcNow.Ticks, Value = msg });

            Console.WriteLine($"Message {x} sent (value: {msg})");
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine($"Exception Occurred - {e.Message}");
      }
    }

  }
}
