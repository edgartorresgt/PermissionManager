using PermissionManager.Models.Models;

namespace PermissionManager.Core.Interfaces;

public interface IKafkaProducerService
{
    Task ProduceMessage(OperationDto message);
}