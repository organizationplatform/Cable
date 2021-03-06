﻿using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Repositories.Basics
{
    public interface IOrganizationUnitRepository : IRepository<OrganizationUnit, string>
    {
        
    }
}