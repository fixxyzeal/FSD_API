using BO.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceLB
{
    public interface IBrushingInformationService
    {
        Task<BrushingInformation> ManageInformation(BrushingInformation brushingInformation);

        Task<IEnumerable<BrushingInformation>> Get(bool? latest, string userId);
    }
}