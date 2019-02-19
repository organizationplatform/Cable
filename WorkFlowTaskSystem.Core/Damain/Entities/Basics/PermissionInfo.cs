﻿namespace WorkFlowTaskSystem.Core.Damain.Entities.Basics
{
    /// <summary>
    /// 定义权限
    /// </summary>
    public class PermissionInfo: BaseEntity,ITree
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }

        public string ParentId { get; set; }

        public string ParentName { get; set; }

        public string Path { get; set; }
        public string PathName { get; set; }
    }
}
