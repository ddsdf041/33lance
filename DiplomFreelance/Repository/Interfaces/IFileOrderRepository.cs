using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IFileOrderRepository
    {
        IEnumerable<FileOrder> GetAllFile();
        FileOrder GetFileById(int id);
        void CreateFile(FileOrder item); // создание объекта
        void UpdateFile(FileOrder item); // обновление объекта ??
        void DeleteFile(int id); // удаление объекта по id
        IEnumerable<FileOrder> GetFileByOrderId(Guid id);
    }
}