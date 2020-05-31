using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceChat : IServiceChat
    {
        private IChatRepository _chatRepository;
        private IServiceCustomer _serviceCustomer;
        private IServiceExecutor _serviceExecutor;

        public ServiceChat(IChatRepository chatRepository, IServiceCustomer serviceCustomer, IServiceExecutor serviceExecutor)
        {
            _chatRepository = chatRepository;
            _serviceCustomer = serviceCustomer;
            _serviceExecutor = serviceExecutor;
        }


        private DomainChat GetChat(Chat chat)
        {
            var customer = _serviceCustomer.GetCustomerByUserID(chat.ID_User_1);
            var executor = _serviceExecutor.GetExecutorByIdUser(chat.ID_User_2);
            return chat.ConvertToChatDomainModel(customer, executor);
        }

        private List<DomainChat> GetChats(List<Chat> chat)
        {
            List<DomainChat> list = new List<DomainChat>();
            foreach (var item in chat)
            {
                var customer = _serviceCustomer.GetCustomerByUserID(item.ID_User_1);
                var executor = _serviceExecutor.GetExecutorByIdUser(item.ID_User_2);
                list.Add(item.ConvertToChatDomainModel(customer, executor));
            }          
           
            return list;
        }
        public List<DomainChat> GetChats(string UserId)
        {

            return GetChats(_chatRepository.GetAllChatsByUserId(UserId).ToList());
                                                                 
        }

        public DomainChat GetChat(string UserId1, string UserId2)
        {

            return GetChat(_chatRepository.GetChatByUsersId(UserId1, UserId2));

        }

        public bool CreateNewChat(Chat item)
        {
            try
            {              
                _chatRepository.CreateChat(item);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}