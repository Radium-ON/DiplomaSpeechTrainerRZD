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
    public class DataBaseParameterType : IDataBaseParameterType<ParameterTypeDto, bool>
    {
        private readonly DatabaseConnection _client;

        public DataBaseParameterType()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<ParameterTypeDto,bool>

        public async Task<List<ParameterTypeDto>> SelectAllAsync(bool includeNestedData)
        {
            var command = "SELECT * FROM ParameterType";
            var parmTypes = new List<ParameterTypeDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var type = dataReader.GetString(1);
                        
                        var parmType = new ParameterTypeDto(id, type);
                        parmTypes.Add(parmType);
                    }
                }

                _client.CloseConnection();
                return parmTypes;
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

        public async Task<ParameterTypeDto> SelectByIdAsync(int idObject, bool includeNestedData)
        {
            var command = "SELECT * FROM ParameterType WHERE Id = @ID";
            var parmType = new ParameterTypeDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var type = dataReader.GetString(1);
                        
                        parmType = new ParameterTypeDto(id, type);
                    }
                }

                _client.CloseConnection();
                return parmType;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseParameterType.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(ParameterTypeDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseParameterType<ParameterTypeDto,bool>

        public async Task<bool> CreateAsync(ParameterTypeDto newObject)
        {
            var commandCreateParameterType = "INSERT ParameterType VALUES (@Type)";
            try
            {
                using (var cmd = new SqlCommand(commandCreateParameterType, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar);
                    cmd.Parameters["@Type"].Value = newObject.TypeName;

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