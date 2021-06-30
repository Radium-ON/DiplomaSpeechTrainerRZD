using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseParameter : IDataBaseParameter<ParameterDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseParameter()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<ParameterDto,bool>

        public async Task<List<ParameterDto>> SelectAllAsync(bool includeNestedData)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<ParameterDto> SelectByIdAsync(int id, bool includeNestedData)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> UpdateAsync(ParameterDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseParameter<ParameterDto,bool>

        public async Task<List<ParameterDto>> GetParametersByAnswerFormAsync(int idForm)
        {
            const string command = "SELECT Parameter.Id, Parameter.OrderNum, Parameter.FormId FROM Parameter" +
                                   " WHERE Parameter.FormId = @ID";

            var parms = new List<ParameterDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idForm);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var orderNum = dataReader.GetInt32(1);
                        var formId = dataReader.GetInt32(2);

                        var parm = new ParameterDto(id, orderNum, null, formId);
                        parms.Add(parm);
                    }
                }
                _client.CloseConnection();
                foreach (var parm in parms)
                {
                    parm.SetAvailableValue(await GetParameterValueAsync(parm.Id));
                }
                return parms;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseParameter.GetParametersByAnswerFormAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<AvailableValueDto> GetParameterValueAsync(int parmId)
        {
            var db = new DataBaseAvailableValue();
            return await db.GetAvailableValueByParameterAsync(parmId);
        }

        public async Task<bool> CreateAsync(int idForm, int idParameterType, ParameterDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        #endregion
    }
}