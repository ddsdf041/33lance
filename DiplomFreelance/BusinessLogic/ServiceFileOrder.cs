using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceFileOrder: IServiceFileOrder
    {
        private IFileOrderRepository _fileOrderRepository;

        public ServiceFileOrder(IFileOrderRepository fileOrderRepository)
        {
            _fileOrderRepository = fileOrderRepository;
        }

        //public DomainFileOrder GetFileOrderById(int id)
        //{
        //    return GetFileOrder(_fileOrderRepository.GetFileById(id));
        //}

        public List<DomainFileOrder> GetFileOrderByOrderId(Guid id)
        {
            return _fileOrderRepository.GetFileByOrderId(id).ToList().ConvertToFileOrderDomainModel();
        }

        public bool CreateNewFileOrder(FileOrderViewModel item)
        {
            try
            {
                _fileOrderRepository.CreateFile(item.ConvertFromViewModelToDBModel());
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool DeleteFileOrder(int id)
        {
            try
            {
                _fileOrderRepository.DeleteFile(id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public List<DomainFileOrder> GetFileOrdersByOrderId(Guid idOrder)
        {


            return FileOrderConvertor.ConvertToFileOrderDomainModel(_fileOrderRepository.GetFileByOrderId(idOrder).ToList());
        }
    }
}