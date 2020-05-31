using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class ChatRepository: IChatRepository
    {
        private string _connectString;
        private DataConnection _db;

        public ChatRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        public void CreateChat(Chat item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Chat(User_ID_1,User_ID_2) 
                                       VALUES(N'{item.ID_User_1}',
                                               N'{item.ID_User_2}')");
                                              
        }

        public IEnumerable<Chat> GetAllChatsByUserId(string iduser)
        {

            return _db.ToList($"SELECT * FROM Chat WHERE Chat.User_ID_1 = '{iduser}' or Chat.User_ID_2 = '{iduser}'", Mapper.MapChats);
        }
        public Chat GetChatByUsersId(string iduser1, string iduser2)
        {

            return _db.ToObject($"SELECT * FROM Chat WHERE Chat.User_ID_1 = '{iduser1}' and Chat.User_ID_2 = '{iduser2}'", Mapper.MapChat);
        }
    }
}