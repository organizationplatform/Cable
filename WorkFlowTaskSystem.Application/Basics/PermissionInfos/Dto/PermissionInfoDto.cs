using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Application.Basics.PermissionInfos.Dto
{
    [AutoMap(typeof(PermissionInfo))]
    public class PermissionInfoDto : EntityDto<string>, ITree
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }

        
        public string Description { get; set; }

        public string ParentId { get; set; }

        public string ParentName { get; set; }


    }
    
}
