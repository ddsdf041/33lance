using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private string _connectString;
        private DataConnection _db;

        public MessageRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        public void CreateMessage(Message item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Message(ID_Chat,Sender,Text,Time_send) 
                                       VALUES({item.ID_Chat},
                                               N'{item.Sender}',
                                               N'{item.Text}',
                                              N'{item.Time_send}')");
                                             
        }

        public IEnumerable<Message> GetAllMessagesByChatId(int idchat)
        {
            return _db.ToList($"SELECT * FROM Message WHERE Message.ID_Chat = {idchat}", Mapper.MapMessages);
        }
    }
}