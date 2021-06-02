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
    public class DatabaseTask : IDataBaseTask<TaskEntityDTO, bool>
    {
        private readonly DatabaseConnection _client;
        public DatabaseTask()
        {
            _client = DatabaseConnection.Source;
        }

        public async Task<List<TaskEntityDTO>> SelectAllAsync()
        {
            var command = "SELECT * FROM Task WHERE IsClosed = 0";
            var tasks = new List<TaskEntityDTO>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var title = dataReader.GetString(1);
                        var subtitle = dataReader.GetString(2);
                        var description = dataReader.GetString(3);
                        var addTime = dataReader.GetDateTime(4);
                        var lastChangeTime = dataReader.GetDateTime(5);
                        var isClosed = dataReader.GetBoolean(6);

                        var task = new TaskEntityDTO(id, title, subtitle, description, addTime, lastChangeTime,
                            isClosed, null, null);
                        tasks.Add(task);
                    }
                }

                foreach (var task in tasks)
                {
                    task.SetTypes(await GetTypesTask(task.Id));
                    task.SetIntervals(await GetIntervalsTask(task.Id));
                }

                _client.CloseConnection();
                return tasks;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTask.SelectAll()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<TaskEntityDTO> SelectByIdAsync(int idObject)
        {
            string commandSelect = $"SELECT * FROM Task WHERE ID = @ID";
            var task = new TaskEntityDTO();
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandSelect, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var title = dataReader.GetString(1);
                        var subtitle = dataReader.GetString(2);
                        var description = dataReader.GetString(3);
                        var addTime = dataReader.GetDateTime(4);
                        var lastChangeTime = dataReader.GetDateTime(5);
                        var isClosed = dataReader.GetBoolean(6);

                        task = new TaskEntityDTO(id, title, subtitle, description, addTime, lastChangeTime, isClosed, null, null);
                    }
                }
                task.SetTypes(await GetTypesTask(task.Id));
                task.SetIntervals(await GetIntervalsTask(task.Id));
                _client.CloseConnection();
                return task;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTask.SelectById()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<List<TaskEntityDTO>> GetTaskByProfile(int idProfile)
        {
            _client.CloseConnection();
            string command = "SELECT Task.ID, Task.Title, Task.Subtitle, Task.[Description], Task.AddTime, Task.LastChangeTime, Task.IsClosed " +
                                             "FROM [Profile_Task], Task " +
                                             $"WHERE Profile_Task.IDProfile = @IDProfile AND Profile_Task.IDTask = Task.ID AND Task.IsClosed = 0";

            var tasks = new List<TaskEntityDTO>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@IDProfile", SqlDbType.Int);
                    cmd.Parameters["@IDProfile"].Value = idProfile;

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var title = dataReader.GetString(1);
                        var subtitle = dataReader.GetString(2);
                        var description = dataReader.GetString(3);
                        var addTime = dataReader.GetDateTime(4);
                        var lastChangeTime = dataReader.GetDateTime(5);
                        var isClosed = dataReader.GetBoolean(6);

                        var task = new TaskEntityDTO(id, title, subtitle, description, addTime, lastChangeTime, isClosed, null, null);
                        tasks.Add(task);
                    }
                }

                foreach (var task in tasks)
                {
                    task.SetTypes(await GetTypesTask(task.Id));
                    task.SetIntervals(await GetIntervalsTask(task.Id));
                }
                _client.CloseConnection();
                return tasks;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTask.SelectAll()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(TaskEntityDTO newObject)
        {
            var isClosed = newObject.IsClosed ? 1 : 0;
            string updateCommand = "UPDATE Task " +
                                   $"SET Title = @Title, " +
                                   $"Subtitle = @Subtitle, " +
                                   $"[Description] = @Description, " +
                                   $"LastChangeTime = @LastChangeTime, " +
                                   $"IsClosed = @isClosed " +
                                   $"FROM(SELECT * FROM Task WHERE ID = @ID) AS Selected WHERE Task.ID = Selected.ID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(updateCommand, _client.OpenConnection()))
                {

                    cmd.Parameters.Add("@Title", SqlDbType.VarChar);
                    cmd.Parameters["@Title"].Value = newObject.Title;

                    cmd.Parameters.Add("@Subtitle", SqlDbType.VarChar);
                    cmd.Parameters["@Subtitle"].Value = newObject.Subtitle;

                    cmd.Parameters.Add("@Description", SqlDbType.VarChar);
                    cmd.Parameters["@Description"].Value = newObject.Description;

                    cmd.Parameters.Add("@LastChangeTime", SqlDbType.DateTime);
                    cmd.Parameters["@LastChangeTime"].Value = newObject.LastChangeTime;

                    cmd.Parameters.Add("@isClosed", SqlDbType.Bit);
                    cmd.Parameters["@isClosed"].Value = isClosed;

                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = newObject.Id;

                    var row = await cmd.ExecuteNonQueryAsync();
                }
                _client.CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTask.Update()] Error: " + exception.Message);
                _client.CloseConnection();
                return false;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string commandDelete = $"DELETE Type_Task WHERE IDTask = @ID; DELETE Task WHERE Task.ID = @ID";
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandDelete, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value =id;

                    var row = await cmd.ExecuteNonQueryAsync();
                }
                _client.CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTask.Delete()] Error: " + exception.Message);
                _client.CloseConnection();
                return false;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<bool> Create(int idProfile, TaskEntityDTO newObject)
        {
            string commandCreateTask = "INSERT Task VALUES" +
                                       $"(@Title, " +
                                       $"@Subtitle, " +
                                       $"@Description, " +
                                       $"@AddTime, @LastChangeTime, " +
                                       $"0)";
            string commandGetLastAddIndex = "SELECT IDENT_CURRENT('Task') AS [IDENT_CURRENT]";

            decimal? lastIndex = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand(commandCreateTask, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar);
                    cmd.Parameters["@Title"].Value = newObject.Title;

                    cmd.Parameters.Add("@Subtitle", SqlDbType.VarChar);
                    cmd.Parameters["@Subtitle"].Value = newObject.Subtitle;

                    cmd.Parameters.Add("@Description", SqlDbType.VarChar);
                    cmd.Parameters["@Description"].Value = newObject.Description;

                    cmd.Parameters.Add("@AddTime", SqlDbType.DateTime);
                    cmd.Parameters["@AddTime"].Value = newObject.AddTime;

                    cmd.Parameters.Add("@LastChangeTime", SqlDbType.DateTime);
                    cmd.Parameters["@LastChangeTime"].Value = newObject.LastChangeTime;

                    cmd.ExecuteNonQuery();
                }
                _client.CloseConnection();
                using (SqlCommand cmd = new SqlCommand(commandGetLastAddIndex, _client.OpenConnection()))
                {

                    

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        lastIndex = dataReader.GetDecimal(0);
                    }
                }
                _client.CloseConnection();
                var insertTypeTask = InsertTypeTask((int?)lastIndex, newObject.Types);
                var insertProfileTask = InsertProfileTask(idProfile, (int?)lastIndex);

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

        public async Task<bool> CloseTask(int idTask, bool isClosed)
        {
            var isClosedValue = isClosed ? 1 : 0;
            var command = $"UPDATE Task SET IsClosed = {isClosedValue}" +
                          $" FROM (SELECT * FROM Task WHERE ID = {idTask}) AS Selected WHERE Task.ID = Selected.ID";
            try
            {
                using (SqlCommand cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var row = await cmd.ExecuteNonQueryAsync();
                }
                _client.CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseTask.CloseTask()] Error: " + exception.Message);
                _client.CloseConnection();
                return false;
            }
            finally
            {
                _client.CloseConnection();
            }
        }


        private async Task<List<TypeDTO>> GetTypesTask(int taskId)
        {
            var databaseType = new DatabaseType();
            return await databaseType.GetTypesTask(taskId);
        }

        private async Task<List<IntervalDTO>> GetIntervalsTask(int id)
        {
            _client.CloseConnection();
            string commandGetIntervalsTask = "SELECT [Task_Interval].ID, [Task_Interval].StartTime, [Task_Interval].FinishTime, [Task_Interval].Rating FROM Task_Interval " +
                                             $"WHERE [Task_Interval].IDTask = {id}";
            var intervals = new List<IntervalDTO>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandGetIntervalsTask, _client.OpenConnection()))
                {
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var type = new IntervalDTO(dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetDateTime(2), dataReader.GetDouble(3));
                        intervals.Add(type);
                    }
                }
                _client.CloseConnection();
                return intervals;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DataBaseTask.GetIntervalsTask()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<bool> InsertTypeTask(int? lastIndexTask, List<TypeDTO> types)
        {
            try
            {
                if (lastIndexTask != null)
                {
                    foreach (var type in types)
                    {
                        string commandInsertTypeTask = $"INSERT Type_Task VALUES (@IDTask, @IDType)";
                        using (SqlCommand cmd = new SqlCommand(commandInsertTypeTask, _client.OpenConnection()))
                        {
                            cmd.Parameters.Add("@IDTask", SqlDbType.Int);
                            cmd.Parameters["@IDTask"].Value = lastIndexTask;

                            cmd.Parameters.Add("@IDType", SqlDbType.Int);
                            cmd.Parameters["@IDType"].Value = type.Id;

                            cmd.ExecuteNonQuery();
                        }
                        _client.CloseConnection();
                    }
                    return true;
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

        private async Task<bool> InsertProfileTask(int idProfile, int? idTask)
        {
            _client.CloseConnection();
            string commandInsertProfileTask = $"INSERT Profile_Task VALUES ( @IDProfile, @IDTask )";

            try
            {
                using (SqlCommand cmd = new SqlCommand(commandInsertProfileTask, _client.OpenConnection()))
                {
                    cmd.Parameters.Add("@IDProfile", SqlDbType.Int);
                    cmd.Parameters["@IDProfile"].Value = idProfile;

                    cmd.Parameters.Add("@IDTask", SqlDbType.Int);
                    cmd.Parameters["@IDTask"].Value = idTask;
                    var row = await cmd.ExecuteNonQueryAsync();
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
    }
}
