using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceMessage : IServiceMessage
    {
        private IMessageRepository _messageRepository;

        public ServiceMessage(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }


        private List<DomainMessage> GetMessages(List<Message> messages)
        {
            List<DomainMessage> list = new List<DomainMessage>();
            foreach (var item in messages)
            {
                list.Add(item.ConvertToMessageDomainModel());
            }

            return list;
        }

        public List<DomainMessage> GetMessagesByChatId(int chatid)
        {
            return GetMessages(_messageRepository.GetAllMessagesByChatId(chatid).ToList());
        }

        public bool CreateNewMessage(CreateMessageViewModel item)
        {
            try
            {
                var message = item.ConvertToDbModelFromViewModel();
                _messageRepository.CreateMessage(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}