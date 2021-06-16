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
    public class DataBasePosition : IDataBasePosition<PositionDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBasePosition()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<PositionDto,bool>

        public async Task<List<PositionDto>> SelectAllAsync(bool includeNestedData)
        {
            const string command = "SELECT * FROM Position";
            var positions = new List<PositionDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var fullPos = dataReader.GetString(1);
                        var responsibilities = dataReader.GetString(2);
                        var shortName = dataReader.GetString(3);

                        var position = new PositionDto(id, shortName, fullPos, responsibilities);
                        positions.Add(position);
                    }
                }

                _client.CloseConnection();
                return positions;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePosition.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<PositionDto> SelectByIdAsync(int idObject, bool includeNestedData)
        {
            const string command = "SELECT * FROM Position WHERE Id = @ID";
            var position = new PositionDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var fullPos = dataReader.GetString(1);
                        var responsibilities = dataReader.GetString(2);
                        var shortName = dataReader.GetString(3);

                        position = new PositionDto(id, shortName, fullPos, responsibilities);
                    }
                }

                _client.CloseConnection();
                return position;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePosition.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(PositionDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBasePosition<PositionDto,bool>

        public async Task<List<PositionDto>> GetPositionsBySituationAsync(int idSituation)
        {
            const string command = "SELECT Position.Id, FullPosition, Responsibilities, ShortName" +
                                   " FROM Participant, Position WHERE Participant.SituationId = @ID AND Participant.PositionId = Position.Id";
            var positions = new List<PositionDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idSituation);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var fullPos = dataReader.GetString(1);
                        var responsibilities = dataReader.GetString(2);
                        var shortName = dataReader.GetString(3);

                        positions.Add(new PositionDto(id, shortName, fullPos, responsibilities));
                    }
                }

                _client.CloseConnection();
                return positions;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePosition.GetPositionsBySituationAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<PositionDto> GetPositionByFormAsync(int idForm)
        {
            const string command = "SELECT Position.Id, FullPosition, Responsibilities, ShortName" +
                                   " FROM Participant, Position, AnswerForm_Phrase_Participant WHERE AnswerForm_Phrase_Participant.FormId = @ID" +
                                   " AND AnswerForm_Phrase_Participant.ParticipantId = Participant.Id" +
                                   " AND Participant.PositionId = Position.Id";
            var position = new PositionDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idForm);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var fullPos = dataReader.GetString(1);
                        var responsibilities = dataReader.GetString(2);
                        var shortName = dataReader.GetString(3);

                        position = new PositionDto(id, shortName, fullPos, responsibilities);
                    }
                }

                _client.CloseConnection();
                return position;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePosition.GetPositionByFormAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<PositionDto> GetPositionByTrainingAsync(int idTraining)
        {
            const string command = "SELECT Position.Id, FullPosition, Responsibilities, ShortName" +
                                   " FROM Participant, Position, Training WHERE Training.Id = @ID AND Training.ParticipantId = Participant.Id" +
                                   " AND Participant.PositionId = Position.Id";
            var position = new PositionDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idTraining);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var fullPos = dataReader.GetString(1);
                        var responsibilities = dataReader.GetString(2);
                        var shortName = dataReader.GetString(3);

                        position = new PositionDto(id, shortName, fullPos, responsibilities);
                    }
                }

                _client.CloseConnection();
                return position;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePosition.GetPositionByTrainingAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> CreateAsync(PositionDto newObject)
        {
            var commandCreateParameterType = "INSERT Position VALUES" +
                                             "(@FullPosition," +
                                             "@Resp," +
                                             "@ShortName)";

            try
            {
                using (var cmd = new SqlCommand(commandCreateParameterType, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@FullPosition", SqlDbType.NVarChar);
                    cmd.Parameters["@FullPosition"].Value = newObject.FullPosition;

                    cmd.Parameters.Add("@Resp", SqlDbType.NVarChar);
                    cmd.Parameters["@Resp"].Value = newObject.Responsibilities;

                    cmd.Parameters.Add("@ShortName", SqlDbType.NVarChar);
                    cmd.Parameters["@ShortName"].Value = newObject.ShortName;

                    await cmd.ExecuteNonQueryAsync();
                }
                _client.CloseConnection();

                return true;
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