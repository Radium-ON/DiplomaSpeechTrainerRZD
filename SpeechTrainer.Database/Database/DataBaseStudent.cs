using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseStudent : IDataBaseStudent<StudentDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseStudent()
        {
            _client = DatabaseConnection.Source;
        }
    }
}