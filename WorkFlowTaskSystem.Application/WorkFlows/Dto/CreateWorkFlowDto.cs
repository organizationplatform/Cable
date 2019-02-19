using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.WorkFlows.Dto
{
    [AutoMapTo(typeof(WorkFlow))]
    public class CreateWorkFlowDto : EntityDto<string>
    {
        public CreateWorkFlowDto()
        {
            Id = Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ���̱���
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// ��ڵ� �� ����ڵ�
        /// </summary>
        public List<WorkTask> WorkTasks { get; set; }
        /// <summary>
        /// ��id
        /// </summary>
        public string Formid { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }
        
    }
}
