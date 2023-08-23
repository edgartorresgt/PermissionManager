using PermissionManager.Core.Interfaces;
using PermissionManager.Models.Entities;
using PermissionManager.Repositories.Interfaces;

namespace PermissionManager.Core.Services;

public class PermissionTypeService : IPermissionTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public PermissionTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PermissionType>> GetAllPermissionTypesAsync()
    {
        return (await _unitOfWork.PermissionTypes.GetAllAsync())!;
    }

    public async Task<PermissionType> GetPermissionTypeByIdAsync(int id)
    {
        return (await _unitOfWork.PermissionTypes.GetByIdAsync(id))!;
    }

    public async Task<bool> AddPermissionTypeAsync(PermissionType type)
    {
        _unitOfWork.PermissionTypes.Add(type);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdatePermissionTypeAsync(PermissionType type)
    {
        _unitOfWork.PermissionTypes.Update(type);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePermissionTypeAsync(int id)
    {
        var permissionType = await _unitOfWork.PermissionTypes.GetByIdAsync(id);
        if (permissionType == null) return false;

        _unitOfWork.PermissionTypes.Delete(permissionType);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}