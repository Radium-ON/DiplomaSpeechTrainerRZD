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
    public class DataBaseAvailableValue : IDataBaseAvailableValue<AvailableValueDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseAvailableValue()
        {
            _client = DatabaseConnection.Source;
        }


        #region Implementation of IDatabase<AvailableValueDto,bool>

        public async Task<List<AvailableValueDto>> SelectAllAsync()
        {
            var command = "SELECT * FROM AvailableValue";
            var availValues = new List<AvailableValueDto>();
            var parmTypeIds = new List<int>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var value = dataReader.GetString(1);
                        var parmTypeId = dataReader.GetInt32(2);

                        var valueDto = new AvailableValueDto(id, parmTypeId, value);
                        availValues.Add(valueDto);
                    }
                }

                foreach (var value in availValues)
                {
                    value.SetParameterType(await GetTypeForValueAsync(value.ParmTypeId));
                }

                _client.CloseConnection();
                return availValues;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseParameterType.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<AvailableValueDto> SelectByIdAsync(int id)
        {
            return null;
        }

        public async Task<bool> UpdateAsync(AvailableValueDto newObject)
        {
            return false;//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return false;//todo
        }

        #endregion

        #region Implementation of IDataBaseAvailableValue<AvailableValueDto,bool>

        public async Task<List<AvailableValueDto>> GetAvailableValuesByParameterTypeAsync(int idParameterType)
        {
            var command = "SELECT Id, Value FROM AvailableValue WHERE ParameterType.Id = @IDParameterType";
            var values = new List<AvailableValueDto>();

            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@IDParameterType", SqlDbType.Int);
                    cmd.Parameters["@IDParameterType"].Value = idParameterType;

                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var value = dataReader.GetString(1);

                        var availValue = new AvailableValueDto(id, null, value);
                        values.Add(availValue);
                    }
                }
                foreach (var value in values)
                {
                    value.SetParameterType(await GetTypeForValueAsync(idParameterType));
                }
                _client.CloseConnection();
                return values;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAvailableValue.GetAvailableValuesByParameterTypeAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<ParameterTypeDto> GetTypeForValueAsync(int parmTypeId)
        {
            var dbParmType = new DataBaseParameterType();
            return await dbParmType.SelectByIdAsync(parmTypeId);
        }

        public async Task<bool> CreateAsync(int idParameterType, AvailableValueDto newObject)
        {
            return false;//todo
        }

        #endregion
    }
}