
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto
{
    [AutoMap(typeof(BridgeConstant))]
    public class BridgeConstantDto : EntityDto<string>
    {
        public BridgeConstantDto()
        {
            Id= Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// �żܱ���
        /// </summary>
        public string BridgeCode { get; set; }
        /// <summary>
        /// ϵ��
        /// </summary>
        public string Series { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public string Types { get; set; }

        /// <summary>
        /// �ͺ�
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        public decimal Lenght { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// ͨ������
        /// </summary>
        public string PassageType { get; set; }
        /// <summary>
        /// �żܽ����
        /// </summary>
        public string SectionalArea { get; set; }
        /// <summary>
        /// �ݻ�����ֵ
        /// </summary>
        public string PlotRatioLimit { get; set; }
        /// <summary>
        /// ������ֵ
        /// </summary>
        public string WeightLimit { get; set; }
        public string Description { get; set; }

    }
}
