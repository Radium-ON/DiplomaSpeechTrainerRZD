using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseTraining : IDataBaseTraining<TrainingDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseTraining()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<TrainingDto,bool>

        public async Task<List<TrainingDto>> SelectAllAsync()
        {
            const string command = "SELECT * FROM Training";
            var trainings = new List<TrainingDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var scores = dataReader.GetInt32(1);
                        var trainDate = dataReader.GetDateTime(2);
                        var studentId = dataReader.GetInt32(3);
                        var participantId = dataReader.GetInt32(4);

                        trainings.Add(new TrainingDto(id, scores, trainDate, studentId, participantId, null, null));
                    }
                }
                _client.CloseConnection();
                foreach (var training in trainings)
                {
                    training.SetSituation(await GetTrainingSituationAsync(training.Id));
                    //training.SetStudent(await GetTrainingStudentAsync(training.Id));
                    training.SetTrainingLines(await GetTrainingLinesAsync(training.Id));
                }
                return trainings;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTraining.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<List<TrainingLineDto>> GetTrainingLinesAsync(int trainingId)
        {
            var db = new DataBaseTrainingLine();
            return await db.GetLinesByTrainingAsync(trainingId);
        }

        private async Task<StudentDto> GetTrainingStudentAsync(int studentId)
        {
            var db = new DataBaseStudent();
            return await db.SelectByIdAsync(studentId);
        }

        private async Task<SituationDto> GetTrainingSituationAsync(int trainingId)
        {
            var db = new DataBaseSituation();
            return await db.GetSituationByTrainingAsync(trainingId);
        }

        public async Task<TrainingDto> SelectByIdAsync(int idObject)
        {
            const string command = "SELECT * FROM Training WHERE Id = @ID";
            var training = new TrainingDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var scores = dataReader.GetInt32(1);
                        var trainDate = dataReader.GetDateTime(2);
                        var studentId = dataReader.GetInt32(3);
                        var participantId = dataReader.GetInt32(4);

                        training = new TrainingDto(id, scores, trainDate, studentId, participantId, null, null);
                    }
                }
                _client.CloseConnection();

                training.SetSituation(await GetTrainingSituationAsync(training.Id));
                //training.SetStudent(await GetTrainingStudentAsync(training.Id));
                training.SetTrainingLines(await GetTrainingLinesAsync(training.Id));

                return training;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTraining.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(TrainingDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseTraining<TrainingDto,bool>

        public async Task<List<TrainingDto>> GetTrainingsByStudentAsync(int idStudent)
        {
            const string command = "SELECT * FROM Training WHERE Training.StudentId = @ID";
            var trainings = new List<TrainingDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idStudent);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var scores = dataReader.GetInt32(1);
                        var trainDate = dataReader.GetDateTime(2);
                        var studentId = dataReader.GetInt32(3);
                        var participantId = dataReader.GetInt32(4);

                        trainings.Add(new TrainingDto(id, scores, trainDate, studentId, participantId, null, null));
                    }
                }
                _client.CloseConnection();
                foreach (var training in trainings)
                {
                    training.SetSituation(await GetTrainingSituationAsync(training.Id));
                    //training.SetStudent(await GetTrainingStudentAsync(training.Id));
                    training.SetTrainingLines(await GetTrainingLinesAsync(training.Id));
                }
                return trainings;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTraining.GetTrainingsByStudentAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> CreateAsync(int idStudent, SituationDto situation, PositionDto position,
            TrainingDto newObject)
        {
            const string command = "INSERT INTO Training" +
                                   " (ScoresNumber, TrainingDate, StudentId, ParticipantId)" +
                                   " VALUES(@Scores, @Date, @StudentId," +
                                   " (SELECT Id From Participant WHERE Participant.PositionId = @PositionId AND Participant.SituationId = @SituationId))";

            const string lastIndexCommand = "SELECT IDENT_CURRENT('Training') AS [IDENT_CURRENT]";

            decimal? lastIndex = null;
            _client.CloseConnection();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@Scores", newObject.ScoresNumber);
                    cmd.Parameters.AddWithValue("@Date", newObject.TrainingDate);
                    cmd.Parameters.AddWithValue("@StudentId", idStudent);
                    cmd.Parameters.AddWithValue("@PositionId", position.Id);
                    cmd.Parameters.AddWithValue("@SituationId", situation.Id);
                    var row = await cmd.ExecuteNonQueryAsync();
                    Debug.WriteLine("[DatabaseTraining.CreateAsync()] Rows: " + row);
                }
                _client.CloseConnection();
                using (var cmd = new SqlCommand(lastIndexCommand, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        lastIndex = dataReader.GetDecimal(0);
                    }
                }
                _client.CloseConnection();
                return await InsertTrainingLinesAsync((int?)lastIndex, newObject.TrainingLines);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTraining.CreateAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return false;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<bool> InsertTrainingLinesAsync(int? lastIndex, List<TrainingLineDto> newLines)
        {
            _client.CloseConnection();
            var result = false;
            try
            {
                if (lastIndex != null)
                {
                    var db = new DataBaseTrainingLine();
                    foreach (var line in newLines)
                    {
                        result = await db.CreateAsync((int)lastIndex, line);
                    }

                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
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