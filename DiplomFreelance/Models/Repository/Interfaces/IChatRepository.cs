using DiplomFreelance.Models.FreelanceModels.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IChatRepository
    {
        IEnumerable<Chat> GetAllChatsByUserId(string iduser);
        void CreateChat(Chat item); // создание объекта
        Chat GetChatByUsersId(string iduser1, string userid2);
    }
}