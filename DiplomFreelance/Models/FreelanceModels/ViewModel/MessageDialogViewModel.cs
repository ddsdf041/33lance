using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class MessageDialogViewModel
    {
        public List<MessageViewModel> messages { get; set; }

        public int ID_Chat { get; set; }
    }
}