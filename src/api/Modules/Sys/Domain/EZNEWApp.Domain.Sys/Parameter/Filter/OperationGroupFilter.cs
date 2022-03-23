using System.Collections.Generic;
using System.Net.NetworkInformation;
using System;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 操作分组筛选信息
    /// </summary>
    public class OperationGroupFilter : PagingFilter, IDomainParameter
    {
        #region 筛选条件

        /// <summary>
        /// 编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 排除数据
        /// </summary>
        public List<long> ExcludeIds { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public long? Parent { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets the creation time
        /// </summary>
        public DateTimeOffset? CreationTime { get; set; }

        /// <summary>
        /// Gets the update time
        /// </summary>
        public DateTimeOffset? UpdateTime { get; set; }

        /// <summary>
        /// 是否只查询第一级
        /// </summary>
        public bool LevelOne { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<OperationGroup>(this);
            if (!Ids.IsNullOrEmpty())
            {
                query.And<OperationGroup>(c => Ids.Contains(c.Id));
            }
            if (!ExcludeIds.IsNullOrEmpty())
            {
                query.And<OperationGroup>(c => !ExcludeIds.Contains(c.Id));
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query.And<OperationGroup>(c => c.Name == Name);
            }
            if (Sort.HasValue)
            {
                query.And<OperationGroup>(c => c.Sort == Sort.Value);
            }
            if (Parent.HasValue)
            {
                query.Equal<OperationGroup>(c => c.Parent, Parent.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                query.And<OperationGroup>(c => c.Remark == Remark);
            }
            if (LevelOne)
            {
                query.LessThanOrEqual<OperationGroup>(c => c.Parent, 0);
            }
            return query;
        }

        #endregion
    }
}
