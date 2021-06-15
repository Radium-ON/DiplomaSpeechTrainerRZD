using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
                        var isCorrect = dataReader.GetBoolean(4);

                        lines.Add(new TrainingLineDto(id, studentAnswer, completeForm, trainingId, isCorrect));
                    }
                }
                _client.CloseConnection();

                if (lines.Count != 0)
                {
                    var forms = await GetAnswerNumbersAsync(lines.First().TrainingId);//todo некрасиво, но делать нечего

                    for (var i = 0; i < lines.Count; i++)
                    {
                        var line = lines[i];
                        line.SetNumber(forms[i].OrderNum);
                    }
                }

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

        private async Task<List<AnswerFormDto>> GetAnswerNumbersAsync(int trainingId)
        {
            var dbSituation = new DataBaseSituation();
            var dbAnswerForm = new DataBaseAnswerForm();
            var dbPosition = new DataBasePosition();
            var situation = await dbSituation.GetSituationByTrainingAsync(trainingId);
            var studentPosition = await dbPosition.GetPositionByTrainingAsync(trainingId);
            var forms = await dbAnswerForm.GetFormsBySituationAsync(situation.Id);
            return forms.FindAll(form => form.Position.Id == studentPosition.Id).OrderBy(n => n.OrderNum).ToList();
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
                        var isCorrect = dataReader.GetBoolean(4);

                        line = new TrainingLineDto(id, studentAnswer, completeForm, trainingId, isCorrect);
                    }
                }

                _client.CloseConnection();

                if (line.Id != 0)
                {
                    var form = await GetAnswerNumbersAsync(line.TrainingId);//todo некрасиво, но делать нечего
                    line.SetNumber(form.First().OrderNum);
                }
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
            _client.CloseConnection();
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
                        var isCorrect = dataReader.GetBoolean(4);

                        lines.Add(new TrainingLineDto(id, studentAnswer, completeForm, trainingId, isCorrect));
                    }
                }

                _client.CloseConnection();

                if (lines.Count != 0)
                {
                    var forms = await GetAnswerNumbersAsync(lines.First().TrainingId);//todo некрасиво, но делать нечего

                    for (var i = 0; i < lines.Count; i++)
                    {
                        var line = lines[i];
                        line.SetNumber(forms[i].OrderNum);
                    }
                }

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
            _client.CloseConnection();
            const string command = "INSERT INTO TrainingLine" +
                                   " (StudentAnswer, TrainingId, CompleteForm, IsCorrect)" +
                                   " VALUES(@StudentAnswer, @TrainingId, @CompleteForm, @IsCorrect)";
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@StudentAnswer", newObject.StudentAnswer);
                    cmd.Parameters.AddWithValue("@TrainingId", idTraining);
                    cmd.Parameters.AddWithValue("@CompleteForm", newObject.CompleteForm);
                    cmd.Parameters.AddWithValue("@IsCorrect", newObject.IsCorrect);
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