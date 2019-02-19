﻿using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Forms.Dto
{
    [AutoMap(typeof(FormInstance))]
    public class FormInstanceDto : EntityDto<string>
    {
        public string WorkFlowInsId { get; set; }

        /// <summary>
        /// 表单
        /// </summary>
        public FormDto Forms { get; set; }

        public dynamic PageData { get; set; }

        public List<Attachment> Attachments { get; set; }
    }
    
}
