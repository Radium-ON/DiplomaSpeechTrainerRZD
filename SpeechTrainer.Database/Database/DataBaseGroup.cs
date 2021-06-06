using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using SpeechTrainer.Database.Entities;
using SpeechTrainer.Database.Interfaces;

namespace SpeechTrainer.Database.Database
{
    public class DataBaseGroup : IDataBaseGroup<GroupDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseGroup()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<GroupDto,bool>

        public async Task<List<GroupDto>> SelectAllAsync()
        {
            const string command = "SELECT * FROM Group";
            var groups = new List<GroupDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var name = dataReader.GetString(1);

                        groups.Add(new GroupDto(id, name));
                    }
                }

                _client.CloseConnection();
                return groups;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseGroup.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<GroupDto> SelectByIdAsync(int idObject)
        {
            const string command = "SELECT * FROM Group WHERE Id = @ID";
            var group = new GroupDto();
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

                        group = new GroupDto(id, name);
                    }
                }

                _client.CloseConnection();
                return group;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseGroup.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(GroupDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseGroup<GroupDto,bool>

        public async Task<bool> CreateAsync(GroupDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<GroupDto> GetGroupByStudentAsync(int idStudent)
        {
            const string command = "SELECT Group.Id, Group.Name" +
                                   "FROM Group, Student_Group WHERE Student_Group.StudentId = @ID" +
                                   "AND Student_Group.GroupId = Group.Id";
            var group = new GroupDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idStudent);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var name = dataReader.GetString(1);

                        group = new GroupDto(id, name);
                    }
                }

                _client.CloseConnection();
                return group;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseGroup.GetGroupByStudentAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        #endregion
    }
}
