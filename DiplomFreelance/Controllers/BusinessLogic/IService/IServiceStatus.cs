using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceStatus
    {
        DomainStatus GetStatus(int id);
    }
}
