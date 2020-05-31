using DiplomFreelance.Models.FreelanceModels.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAllMessagesByChatId(int idchat);
        void CreateMessage(Message item); // создание объекта     
    }
}