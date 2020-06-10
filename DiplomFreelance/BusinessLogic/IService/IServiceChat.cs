using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceChat
    {
        List<DomainChat> GetChats(string userid);
        DomainChat GetChat(string UserId1, string UserId2);
        bool CreateNewChat(Chat item);

    }
}