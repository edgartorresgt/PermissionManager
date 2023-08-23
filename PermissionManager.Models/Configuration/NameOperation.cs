using System.ComponentModel;

namespace PermissionManager.Models.Configuration;

public enum NameOperation
{
    [Description("Request Permission")]
    Request,

    [Description("Modify Permission")]
    Modify,

    [Description("Get Permissions")]
    Get
}
