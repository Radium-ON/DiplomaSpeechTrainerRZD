using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseTrainingLine : IDataBaseTrainingLine<TrainingLineDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseTrainingLine()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<TrainingLineDto,bool>

        public async Task<List<TrainingLineDto>> SelectAllAsync()
        {
            const string command = "SELECT * FROM TrainingLine";
            var lines = new List<TrainingLineDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var studentAnswer = dataReader.GetString(1);
                        var trainingId = dataReader.GetInt32(2);
                        var completeForm = dataReader.GetString(3);

                        lines.Add(new TrainingLineDto(id, studentAnswer, completeForm, trainingId));
                    }
                }

                _client.CloseConnection();
                return lines;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTrainingLine.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<TrainingLineDto> SelectByIdAsync(int idObject)
        {
            const string command = "SELECT * FROM TrainingLine WHERE Id = @ID";
            var line = new TrainingLineDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var studentAnswer = dataReader.GetString(1);
                        var trainingId = dataReader.GetInt32(2);
                        var completeForm = dataReader.GetString(3);

                        line = new TrainingLineDto(id, studentAnswer, completeForm, trainingId);
                    }
                }

                _client.CloseConnection();
                return line;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTrainingLine.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(TrainingLineDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseTrainingLine<TrainingLineDto,bool>

        public async Task<List<TrainingLineDto>> GetLinesByTrainingAsync(int idTraining)
        {
            const string command = "SELECT * FROM TrainingLine WHERE TrainingId = @ID";
            var lines = new List<TrainingLineDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idTraining);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var studentAnswer = dataReader.GetString(1);
                        var trainingId = dataReader.GetInt32(2);
                        var completeForm = dataReader.GetString(3);

                        lines.Add(new TrainingLineDto(id, studentAnswer, completeForm, trainingId));
                    }
                }

                _client.CloseConnection();
                return lines;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTrainingLine.GetLinesByTrainingAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> CreateAsync(int idTraining, TrainingLineDto newObject)
        {
            const string command = "INSERT INTO TrainingLine" +
                                   "(StudentAnswer, TrainingId, CompleteForm)" +
                                   "VALUES(@StudentAnswer, @TrainingId, @CompleteForm)";
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@StudentAnswer", newObject.StudentAnswer);
                    cmd.Parameters.AddWithValue("@TrainingId", idTraining);
                    cmd.Parameters.AddWithValue("@CompleteForm", newObject.CompleteForm);
                    var row = await cmd.ExecuteNonQueryAsync();
                    Debug.WriteLine("[DatabaseTrainingLine.CreateAsync()] Rows: " + row);
                }
                _client.CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTrainingLine.CreateAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return false;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        #endregion
    }
}