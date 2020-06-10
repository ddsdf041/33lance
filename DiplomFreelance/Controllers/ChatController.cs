using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomFreelance.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
      
        private static IServiceChat _serviceChat;
        private static IServiceMessage _serviceMessage;

        public ChatController(IServiceChat serviceChat,
          IServiceMessage serviceMessage)
        {
            _serviceChat = serviceChat;
            _serviceMessage = serviceMessage;

        }

        //Выводит на страницу чаты авторизированного пользователя
        public ActionResult ChatPartial(string iduser)
        {
            if (!String.IsNullOrEmpty(iduser))
            {
                DomainChat chat = new DomainChat();
                if (User.IsInRole("Customer"))
                    chat = _serviceChat.GetChat(User.Identity.GetUserId(), iduser);
                else
                    chat = _serviceChat.GetChat(iduser, User.Identity.GetUserId());
                if (chat.ID == 0)
                {
                    Chat chatDb;
                    if (User.IsInRole("Customer"))
                    {
                        chatDb = new Chat()
                        {
                            ID_User_1 = User.Identity.GetUserId(),
                            ID_User_2 = iduser
                        };
                    }
                    else
                    {
                        chatDb = new Chat()
                        {
                            ID_User_1 = iduser,
                            ID_User_2 = User.Identity.GetUserId()
                        };
                    }
                    _serviceChat.CreateNewChat(chatDb);
                }
            }
            var identityID = User.Identity.GetUserId();
            var chats = _serviceChat.GetChats(identityID);
           
            return PartialView("_ChatPartial", chats.ConvertToChatViewModel());

        }

        //выводи диалоги к выбранному чату
        public ActionResult DialogPartial(int? idChat)
        {
            if (idChat != null)
            {
                List<MessageViewModel> mvm = _serviceMessage.GetMessagesByChatId((int)idChat).ConvertToMessageViewModel();
                MessageDialogViewModel msg = new MessageDialogViewModel
                {
                   messages = mvm,
                   ID_Chat = (int)idChat
                };
                return PartialView("_DialogPartial", msg);
            }
            else
            {
                return PartialView("_DialogPartial", null);
            }
        }

       //выводит сообщения к определенному чату
        public ActionResult MessageBoxPartial(int? idChat)
        {
                List<MessageViewModel> mvm = _serviceMessage.GetMessagesByChatId((int)idChat).ConvertToMessageViewModel();
                MessageDialogViewModel msg = new MessageDialogViewModel
                {
                    messages = mvm,
                    ID_Chat = (int)idChat
                };
                return PartialView("_MessageBoxPartial", msg);
        }

        //служит для отправки сообщения
        [HttpPost]
        public ActionResult SendMessage(CreateMessageViewModel model)
        {

            model.Sender = User.Identity.GetUserId();
            TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            string moscowTime = (DateTime.UtcNow + moscowTimeZone.BaseUtcOffset).ToString("yyyy-MM-dd HH:mm");
            model.Time_send = Convert.ToDateTime(moscowTime);
            _serviceMessage.CreateNewMessage(model);
            return RedirectToAction("MessageBoxPartial", new { idChat = model.ID_Chat });

        }
    }
}