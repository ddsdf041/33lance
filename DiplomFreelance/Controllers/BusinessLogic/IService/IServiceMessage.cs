using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceMessage
    {
        List<DomainMessage> GetMessagesByChatId(int chatid);
        bool CreateNewMessage(CreateMessageViewModel model);
    }
}