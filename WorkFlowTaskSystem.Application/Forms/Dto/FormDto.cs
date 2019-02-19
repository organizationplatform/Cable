using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Forms.Dto
{
    [AutoMap(typeof(Form))]
    public class FormDto : EntityDto<string>
    {
        /// <summary>
        /// ����
        /// </summary>
        public List<FormItem> FormItems { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public string Name { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
       
    }
    
}
