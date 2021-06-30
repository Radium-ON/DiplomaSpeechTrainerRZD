using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBasePhrase : IDataBasePhrase<PhraseDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBasePhrase()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<PhraseDto,bool>

        public async Task<List<PhraseDto>> SelectAllAsync(bool includeNestedData)
        {
            var command = "SELECT * FROM Phrase";
            var phrases = new List<PhraseDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var text = dataReader.GetString(1);

                        var phrase = new PhraseDto(id, text);
                        phrases.Add(phrase);
                    }
                }

                _client.CloseConnection();
                return phrases;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePhrase.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<PhraseDto> SelectByIdAsync(int idObject, bool includeNestedData)
        {
            var command = "SELECT * FROM Phrase WHERE Id = @ID";
            var phraseDto = new PhraseDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var text = dataReader.GetString(1);

                        phraseDto = new PhraseDto(id, text);
                    }
                }

                _client.CloseConnection();
                return phraseDto;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabasePhrase.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(PhraseDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion
    }
}