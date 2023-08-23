using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using PermissionManager.Models.Models;
using System.Text.Json;
using PermissionManager.Core.Interfaces;

namespace PermissionManager.Core.Services;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ProducerConfig _config;
    private readonly string _topic;

    public KafkaProducerService(IConfiguration configuration)
    {
        _config = new ProducerConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] };
        _topic = configuration["Kafka:Topic"];
    }

    public async Task ProduceMessage(OperationDto message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        await producer.ProduceAsync(_topic, new Message<Null, string> { Value = JsonSerializer.Serialize(message) });
    }
}
