using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceFileOrder
    {
        List<DomainFileOrder> GetFileOrdersByOrderId(Guid idOrder);
        bool CreateNewFileOrder(FileOrderViewModel fileOrder);
        bool DeleteFileOrder(int id);
    }
}
