using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkFlowTaskSystem.Core;


namespace WorkFlowTaskSystem.Controllers
{
    public  class WorkFlowTaskSystemControllerBase : AbpController
    {
        ////���ظ����AbpSession
        //public new IAbpSessionExtension AbpSession { get; set; }

        public WorkFlowTaskSystemControllerBase()
        {
            LocalizationSourceName = WorkFlowTaskAbpConsts.LocalizationSourceName;
        }

       

    }
}
