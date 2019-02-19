
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.WeightConstants.Dto
{
    [AutoMap(typeof(WeightConstant))]
    public class WeightConstantDto : EntityDto<string>
    {
        public WeightConstantDto()
        {
            Id= Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// ͨ������
        /// </summary>
        public string PassageTypes { get; set; }
        public string PassageTypeName { get; set; }
        public decimal WeightDecimal { get; set; }
        /// <summary>
        /// ������ֵ
        /// </summary>
        public string WeightLimit { get; set; }
        public string Description { get; set; }

    }
}
