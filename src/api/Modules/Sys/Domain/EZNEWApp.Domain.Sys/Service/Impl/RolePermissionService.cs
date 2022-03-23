using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEW.Model;
using EZNEW.Development.Domain.Repository;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 角色授权服务
    /// </summary>
    public class RolePermissionService : IRolePermissionService
    {
        readonly IRepository<RolePermission> rolePermissionRepository;

        public RolePermissionService(IRepository<RolePermission> rolePermissionRepository)
        {
            this.rolePermissionRepository = rolePermissionRepository;
        }

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="roleAuthorizes">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyRolePermissionParameter modifyRolePermission)
        {
            if (modifyRolePermission == null || (modifyRolePermission.Bindings.IsNullOrEmpty() && modifyRolePermission.Unbindings.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何要修改的绑定信息");
            }
            //解绑
            if (!modifyRolePermission.Unbindings.IsNullOrEmpty())
            {
                rolePermissionRepository.Remove(modifyRolePermission.Unbindings);
            }
            //绑定
            if (!modifyRolePermission.Bindings.IsNullOrEmpty())
            {
                rolePermissionRepository.Save(modifyRolePermission.Bindings);
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 根据角色清除角色授权

        /// <summary>
        /// 根据角色清除角色授权
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByRole(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何角色信息");
            }
            rolePermissionRepository.RemoveByRelationData(roleIds.Select(c => new Role { Id = c }));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 根据权限清除角色授权

        /// <summary>
        /// 根据权限清除角色授权
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何权限信息");
            }
            rolePermissionRepository.RemoveByRelationData(permissionIds.Select(c => new Permission() { Id = c }));
            return Result.SuccessResult("修改成功");
        }

        #endregion
    }
}
