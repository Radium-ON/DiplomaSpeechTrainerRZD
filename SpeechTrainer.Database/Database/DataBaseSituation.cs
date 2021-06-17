using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseSituation : IDataBaseSituation<SituationDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseSituation()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<SituationDto,bool>

        public async Task<List<SituationDto>> SelectAllAsync(bool includeNestedData)
        {
            const string command = "SELECT * FROM Situation";
            var situations = new List<SituationDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var name = dataReader.GetString(1);
                        var description = dataReader.GetString(2);

                        var situation = new SituationDto(id, name, description);
                        situations.Add(situation);
                    }
                }
                _client.CloseConnection();
                if (includeNestedData)
                {
                    foreach (var situation in situations)
                    {
                        situation.SetPositions(await GetPositionsBySituationAsync(situation.Id));
                    }
                }
                return situations;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseSituation.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<List<PositionDto>> GetPositionsBySituationAsync(int situationId)
        {
            var dbPosition = new DataBasePosition();
            return await dbPosition.GetPositionsBySituationAsync(situationId);
        }

        private async Task<List<AnswerFormDto>> GetFormsBySituationAsync(int situationId)
        {
            var dbForm = new DataBaseAnswerForm();
            return await dbForm.GetFormsBySituationAsync(situationId);
        }

        public async Task<SituationDto> SelectByIdAsync(int idObject, bool includeNestedData)
        {
            const string command = "SELECT * FROM Situation WHERE Id = @ID";
            var situation = new SituationDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var name = dataReader.GetString(1);
                        var description = dataReader.GetString(2);

                        situation = new SituationDto(id, name, description);
                    }
                }

                _client.CloseConnection();
                if (includeNestedData)
                {
                    situation.SetPositions(await GetPositionsBySituationAsync(situation.Id));
                }
                return situation;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseSituation.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(SituationDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseSituation<SituationDto,bool>

        public async Task<List<SituationDto>> GetSituationsByPositionAsync(int idPosition)
        {
            const string command = "SELECT Situation.Id, Situation.Name, Situation.Description FROM Situation, Participant" +
                                   " WHERE Participant.PositionId = @ID" +
                                   " AND Participant.SituationId = Situation.Id";
            var situations = new List<SituationDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idPosition);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var name = dataReader.GetString(1);
                        var description = dataReader.GetString(2);

                        situations.Add(new SituationDto(id, name, description));
                    }
                }

                _client.CloseConnection();

                foreach (var situation in situations)
                {
                    situation.SetPositions(await GetPositionsBySituationAsync(situation.Id));
                }
                return situations;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseSituation.GetSituationsByPositionAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<SituationDto> GetSituationByTrainingAsync(int idTraining)
        {
            _client.CloseConnection();
            const string command = "SELECT Situation.Id, Situation.Name, Situation.Description FROM Situation, Participant, Training" +
                                   " WHERE Training.Id = @ID AND Training.ParticipantId = Participant.Id AND Participant.SituationId = Situation.Id";
            var situation = new SituationDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idTraining);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var name = dataReader.GetString(1);
                        var description = dataReader.GetString(2);

                        situation = new SituationDto(id, name, description);
                    }
                }

                _client.CloseConnection();

                situation.SetPositions(await GetPositionsBySituationAsync(situation.Id));

                return situation;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseSituation.GetSituationByTrainingAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> CreateAsync(int idPosition, SituationDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        #endregion
    }
}