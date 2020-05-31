using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class ChatConvertor
    {
        public static ChatViewModel ConvertToChatViewModel(this DomainChat chat)
        {
            ChatViewModel item = new ChatViewModel()
            {
                ID = chat.ID,
                Customer = chat.Customer.ConvertToCustomerViewModel(),
                Executor = chat.Executor.ConvertToExecutorViewModel()

            };
            return item;
        }
        public static List<ChatViewModel> ConvertToChatViewModel(this List<DomainChat> chats)
        {
            var list = new List<ChatViewModel>();
            foreach (var item in chats)
            {
                list.Add(ConvertToChatViewModel(item));
            }
            return list;
        }
        public static DomainChat ConvertToChatDomainModel(this Chat chat, DomainCustomer customer, DomainExecutor executor)
        {
            DomainChat item = new DomainChat()
            {
                ID = chat.ID,
                Customer = customer,
                Executor = executor
            };
            return item;
        }
    }
}