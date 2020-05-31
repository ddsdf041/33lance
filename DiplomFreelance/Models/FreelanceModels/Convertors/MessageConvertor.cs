using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class MessageConvertor
    {
        public static MessageViewModel ConvertToMessageViewModel(this DomainMessage message)
        {
            MessageViewModel item = new MessageViewModel()
            {
                ID = message.ID,
                Text = message.Text,
                Sender = message.Sender,
                Time_send = message.Time_send

            };
            return item;
        }

        public static List<MessageViewModel> ConvertToMessageViewModel(this List<DomainMessage> messages)
        {
            var list = new List<MessageViewModel>();
            foreach (var item in messages)
            {
                list.Add(ConvertToMessageViewModel(item));
            }
            return list;
        }

        public static DomainMessage ConvertToMessageDomainModel(this Message message)
        {
            DomainMessage item = new DomainMessage()
            {
                ID = message.ID,
                Sender = message.Sender,
                Text = message.Text,
                Time_send = message.Time_send

            };
            return item;
        }

        public static Message ConvertToDbModelFromViewModel(this CreateMessageViewModel message)
        {
            Message item = new Message()
            {
                ID_Chat = message.ID_Chat,
                Text = message.Text,
                Sender = message.Sender,
                Time_send = message.Time_send

            };
            return item;
        }

    }
}