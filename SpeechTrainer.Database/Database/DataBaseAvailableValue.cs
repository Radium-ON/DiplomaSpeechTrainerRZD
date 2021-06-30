using System;
using System.Collections.Generic;
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

        public async Task<List<AvailableValueDto>> SelectAllAsync(bool includeNestedData)
        {
            const string command = "SELECT * FROM AvailableValue";
            var availValues = new List<AvailableValueDto>();

            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var value = dataReader.GetString(1);

                        var valueDto = new AvailableValueDto(id, value);
                        availValues.Add(valueDto);
                    }
                }

                _client.CloseConnection();

                foreach (var value in availValues)
                {
                    value.SetParameterType(await GetTypeForValueAsync(value.Id));
                }

                return availValues;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAvailableValue.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<AvailableValueDto> SelectByIdAsync(int idObject, bool includeNestedData)
        {
            var command = "SELECT * FROM AvailableValue WHERE Id = @ID";
            var availValue = new AvailableValueDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var value = dataReader.GetString(1);

                        availValue = new AvailableValueDto(id, value);
                    }
                }
                _client.CloseConnection();

                availValue.SetParameterType(await GetTypeForValueAsync(availValue.Id));

                return availValue;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAvailableValue.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(AvailableValueDto newObject)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseAvailableValue<AvailableValueDto,bool>

        public async Task<List<AvailableValueDto>> GetAvailableValuesByParameterTypeAsync(int idParameterType)
        {
            var command = "SELECT Id, Value FROM AvailableValue WHERE ParameterType.Id = @ID";
            var values = new List<AvailableValueDto>();
            _client.CloseConnection();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idParameterType);

                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var value = dataReader.GetString(1);

                        var availValue = new AvailableValueDto(id, value);
                        values.Add(availValue);
                    }
                }
                _client.CloseConnection();
                foreach (var value in values)
                {
                    value.SetParameterType(await GetTypeForValueAsync(idParameterType));
                }
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

        public async Task<AvailableValueDto> GetAvailableValueByParameterAsync(int idParameter)
        {
            const string command = "SELECT AvailableValue.Id, AvailableValue.Value FROM AvailableValue, Parameter_Value" +
                                   " WHERE Parameter_Value.ParameterId = @ID" +
                                   " AND Parameter_Value.ValueId = AvailableValue.Id";

            var value = new AvailableValueDto();
            _client.CloseConnection();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idParameter);

                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var val = dataReader.GetString(1);

                        value = new AvailableValueDto(id, val);
                    }
                }
                _client.CloseConnection();

                value.SetParameterType(await GetTypeForValueAsync(value.Id));

                return value;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAvailableValue.GetAvailableValueByParameterAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<ParameterTypeDto> GetTypeForValueAsync(int valueId)
        {
            const string command = "SELECT ParameterTypeId FROM ParameterType_AvailValue" +
                                   " WHERE ParameterType_AvailValue.ValueId = @ID";
            var parmTypeId = 0;
            _client.CloseConnection();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", valueId);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        parmTypeId = dataReader.GetInt32(0);
                    }
                }

                _client.CloseConnection();
                var dbParmType = new DataBaseParameterType();
                return await dbParmType.SelectByIdAsync(parmTypeId, false);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseAvailableValue.GetTypeForValueAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> CreateAsync(int idParameterType, AvailableValueDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        #endregion
    }
}