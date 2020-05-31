using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class FileOrderConvertor
    {
        public static FileOrder ConvertFromViewModelToDBModel(this FileOrderViewModel fileOrder)
        {
            FileOrder item = new FileOrder()
            {
                Name = fileOrder.Name,
                Image = fileOrder.Image,
                ID_Order = fileOrder.ID_Order
            };
            return item;
        }

        public static FileOrderViewModel ConvertToFileOrderViewModel(this DomainFileOrder file)
        {
            FileOrderViewModel item = new FileOrderViewModel()
            {
                ID = file.ID,
                Name = file.Name,
                Image = file.Image
            };
            return item;
        }
        public static List<FileOrderViewModel> ConvertToFileOrderViewModel(this List<DomainFileOrder> file)
        {
            var list = new List<FileOrderViewModel>();
            foreach (var item in file)
            {
                list.Add(item.ConvertToFileOrderViewModel());
            }
            return list;
        }
        public static DomainFileOrder ConvertToFileOrderDomainModel(this FileOrder file)
        {
            DomainFileOrder item = new DomainFileOrder()
            {
                ID = file.ID,
                Name = file.Name,
                Image = file.Image,
                ID_Order = file.ID_Order
            };
            return item;
        }
        public static List<DomainFileOrder> ConvertToFileOrderDomainModel(this List<FileOrder> file)
        {
            var list = new List<DomainFileOrder>();
            foreach (var item in file)
            {
                list.Add(ConvertToFileOrderDomainModel(item));
            }
            return list;
        }
    }
}