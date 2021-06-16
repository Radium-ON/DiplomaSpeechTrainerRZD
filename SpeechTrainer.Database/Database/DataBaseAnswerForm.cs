using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseAnswerForm : IDataBaseAnswerForm<AnswerFormDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseAnswerForm()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<AnswerFormDto,bool>

        public async Task<List<AnswerFormDto>> SelectAllAsync(bool includeNestedData)
        {
            const string command = "SELECT * FROM AnswerForm";
            var answerForms = new List<AnswerFormDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var orderNum = dataReader.GetInt32(1);

                        var answerForm = new AnswerFormDto(id, orderNum, null, null);
                        answerForms.Add(answerForm);
                    }
                }
                _client.CloseConnection();
                if (includeNestedData)
                {
                    foreach (var form in answerForms)
                    {
                        form.SetSituation(await GetFormSituationAsync(form.Id));
                        form.SetPosition(await GetFormPositionAsync(form.Id));
                        form.SetParameters(await GetFormParametersAsync(form.Id));
                        form.SetPhrase(await GetFormPhraseAsync(form.Id));
                    }
                }
                return answerForms;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<PhraseDto> GetFormPhraseAsync(int formId)
        {
            const string command = "SELECT PhraseId FROM AnswerForm_Phrase_Participant" +
                                   " WHERE AnswerForm_Phrase_Participant.FormId = @ID";
            var phraseId = 0;

            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", formId);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        phraseId = dataReader.GetInt32(0);
                    }
                }
                _client.CloseConnection();

                var db = new DataBasePhrase();
                return await db.SelectByIdAsync(phraseId, true);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.GetFormPhraseAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<PositionDto> GetFormPositionAsync(int formId)
        {
            const string command = "SELECT PositionId FROM AnswerForm_Phrase_Participant, Participant" +
                                   " WHERE AnswerForm_Phrase_Participant.FormId = @ID" +
                                   " AND AnswerForm_Phrase_Participant.ParticipantId = Participant.Id";
            var positionId = 0;

            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", formId);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        positionId = dataReader.GetInt32(0);
                    }
                }

                _client.CloseConnection();
                var db = new DataBasePosition();
                return await db.SelectByIdAsync(positionId, false);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.GetFormPositionAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<SituationDto> GetFormSituationAsync(int formId)
        {
            const string command = "SELECT SituationId FROM AnswerForm_Phrase_Participant, Participant" +
                                   " WHERE AnswerForm_Phrase_Participant.FormId = @ID" +
                                   " AND AnswerForm_Phrase_Participant.ParticipantId = Participant.Id";
            var situationId = 0;

            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", formId);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        situationId = dataReader.GetInt32(0);
                    }
                }

                _client.CloseConnection();
                var db = new DataBaseSituation();
                return await db.SelectByIdAsync(situationId, false);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.GetFormSituationAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<AnswerFormDto> SelectByIdAsync(int idObject, bool includeNestedData)
        {
            const string command = "SELECT * FROM AnswerForm WHERE Id = @ID";
            var form = new AnswerFormDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var orderNum = dataReader.GetInt32(1);

                        form = new AnswerFormDto(id, orderNum, null, null);
                    }
                }

                _client.CloseConnection();
                if (includeNestedData)
                {
                    form.SetSituation(await GetFormSituationAsync(form.Id));
                    form.SetPosition(await GetFormPositionAsync(form.Id));
                    form.SetParameters(await GetFormParametersAsync(form.Id));
                    form.SetPhrase(await GetFormPhraseAsync(form.Id));
                }
                return form;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<List<ParameterDto>> GetFormParametersAsync(int formId)
        {
            var db = new DataBaseParameter();
            return await db.GetParametersByAnswerFormAsync(formId);
        }

        public async Task<bool> UpdateAsync(AnswerFormDto newObject)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IDataBaseAnswerForm<AnswerFormDto,bool>

        public async Task<List<AnswerFormDto>> GetFormsByPositionAsync(int idPosition)
        {
            const string command = "SELECT AnswerForm.Id, AnswerForm.OrderNum FROM AnswerForm, AnswerForm_Phrase_Participant, Participant" +
                                   " WHERE AnswerForm_Phrase_Participant.FormId = AnswerForm.Id" +
                                   " AND AnswerForm_Phrase_Participant.ParticipantId = Participant.Id" +
                                   " AND Participant.PositionId = @ID";

            var answerForms = new List<AnswerFormDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idPosition);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var orderNum = dataReader.GetInt32(1);

                        var answerForm = new AnswerFormDto(id, orderNum, null, null);
                        answerForms.Add(answerForm);
                    }
                }

                _client.CloseConnection();

                foreach (var form in answerForms)
                {
                    form.SetSituation(await GetFormSituationAsync(form.Id));
                    form.SetPosition(await GetFormPositionAsync(form.Id));
                    form.SetParameters(await GetFormParametersAsync(form.Id));
                    form.SetPhrase(await GetFormPhraseAsync(form.Id));
                }
                return answerForms;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.GetFormsByPositionAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<List<AnswerFormDto>> GetFormsBySituationAsync(int idSituation)
        {
            const string command = "SELECT AnswerForm.Id, AnswerForm.OrderNum FROM AnswerForm, AnswerForm_Phrase_Participant, Participant" +
                                   " WHERE AnswerForm_Phrase_Participant.FormId = AnswerForm.Id" +
                                   " AND AnswerForm_Phrase_Participant.ParticipantId = Participant.Id" +
                                   " AND Participant.SituationId = @ID";

            var answerForms = new List<AnswerFormDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idSituation);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var orderNum = dataReader.GetInt32(1);

                        var answerForm = new AnswerFormDto(id, orderNum, null, null);
                        answerForms.Add(answerForm);
                    }
                }
                _client.CloseConnection();
                foreach (var form in answerForms)
                {
                    form.SetSituation(await GetFormSituationAsync(form.Id));
                    form.SetPosition(await GetFormPositionAsync(form.Id));
                    form.SetParameters(await GetFormParametersAsync(form.Id));
                    form.SetPhrase(await GetFormPhraseAsync(form.Id));
                }
                return answerForms;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAnswerForm.GetFormsBySituationAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> CreateAsync(int idParticipant, AnswerFormDto newObject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}