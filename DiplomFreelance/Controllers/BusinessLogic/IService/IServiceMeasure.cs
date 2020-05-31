using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceMeasure
    {
        DomainMeasure GetMeasure(int id);
        List<DomainMeasure> GetAllMeasure();
    }
}
