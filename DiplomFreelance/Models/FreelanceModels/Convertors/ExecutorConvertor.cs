using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class ExecutorConvertor
    {
        public static Executor ConvertFromViewModelToDBModel(this ExecutorViewModel executor)
        {
            Executor item = new Executor()
            {
                ID_User =  executor.ID_User,
                City =executor.City,
                Specialty = executor.Specialty,
                Description = executor.Description,
                Name = executor.Name,
                Photo= executor.Photo,
                Telephone = executor.Telephone
            };
            return item;
        }
        //??
        public static ExecutorViewModel ConvertToExecutorViewModel(this DomainExecutor executor)
        {
            ExecutorViewModel item = new ExecutorViewModel()
            {
                Name = executor.Name,
                ID_User = executor.ID_User,
                Email = executor.Email,
                Specialty = executor.Specialty,
                Telephone = executor.Telephone,
                Raiting = executor.Raiting,
                Description = executor.Description,
                Photo = executor.Photo,
                City = executor.City,
                IsBanned = executor.IsBanned,
                Services = ServiceConvertor.ConvertToServiceViewModel(executor.Services)
            };
            return item;
        }
        public static List<ExecutorViewModel> ConvertToExecutorViewModel(this List<DomainExecutor> executor)
        {
            var list = new List<ExecutorViewModel>();
            foreach (var item in executor)
            {
                list.Add(ConvertToExecutorViewModel(item));
            }
            return list;
        }
        public static DomainExecutor ConvertToExecutorDomainModel(this Executor executor, List<DomainService> services/*, List<DomainOrder> orders, List<DomainResponse> responses*/)
        {
            DomainExecutor item = new DomainExecutor()
            {
                Name = executor.Name,
                ID_User = executor.ID_User,
                Email = executor.Email,
                Specialty = executor.Specialty,
                Telephone = executor.Telephone,
                Raiting = executor.Raiting,
                Description = executor.Description,
                Photo = executor.Photo,
                City = executor.City,
                //Orders = orders,
                Services = services,
                IsBanned = executor.IsBanned
                //Responses = responses
            };

            return item;
        }
    }
}