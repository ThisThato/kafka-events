using System;
using System.Configuration;

namespace Domain
{
  public class Configurations
  {
    public string BrokerList = ConfigurationManager.AppSettings["EH_FQDN"];
    public string ConnectionString = ConfigurationManager.AppSettings["EH_CONNECTION_STRING"];
    public string Topic = ConfigurationManager.AppSettings["EH_NAME"];
    public string CaCertLocation = ConfigurationManager.AppSettings["CA_CERT_LOCATION"];
    public string ConsumerGroup = ConfigurationManager.AppSettings["CONSUMER_GROUP"];
  }
}
