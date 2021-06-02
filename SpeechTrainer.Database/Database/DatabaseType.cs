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
    public class DatabaseType : IDatabaseType<TypeDTO, bool>
    {
        private readonly DatabaseConnection client;
        public DatabaseType()
        {
            client = DatabaseConnection.Source;
        }

        public async Task<List<TypeDTO>> SelectAllAsync()
        {
            var command = "SELECT * FROM [Type]";

            var types = new List<TypeDTO>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(command, client.OpenConnection()))
                {
                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var type = new TypeDTO(dataReader.GetInt32(0), dataReader.GetString(1));
                        types.Add(type);
                    }
                }
                client.CloseConnection();
                return types;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseType.SelectAll()] Error: " + exception.Message);
                client.CloseConnection();
                return null;
            }
            finally
            {
                client.CloseConnection();
            }
        }

        public async Task<TypeDTO> SelectByIdAsync(int idObject)
        {
            string commandSelectById = $"SELECT * FROM [Type] WHERE [Type].ID = @ID"; 
            var type = new TypeDTO();
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandSelectById, client.OpenConnection()))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = idObject;

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        type = new TypeDTO(dataReader.GetInt32(0), dataReader.GetString(1));
                    }
                }
                client.CloseConnection();
                return type;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseType.SelectById()] Error: " + exception.Message);
                client.CloseConnection();
                return null;
            }
            finally
            {
                client.CloseConnection();
            }
        }

        public async Task<bool> UpdateAsync(TypeDTO newObject)
        {
            string updateCommand =
                $"UPDATE [Type] SET Title='{newObject.Title}' " +
                $"FROM (SELECT * FROM [Type] WHERE ID = {newObject.Id}) AS Selected WHERE [Type].ID = Selected.ID";

            try
            {
                using (SqlCommand cmd = new SqlCommand(updateCommand, client.OpenConnection()))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = newObject.Id;

                    cmd.Parameters.Add("@Title", SqlDbType.VarChar);
                    cmd.Parameters["@Title"].Value = newObject.Title;

                    await cmd.ExecuteNonQueryAsync();
                }
                client.CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DataBaseProfile.Update()] Error: " + exception.Message);
                client.CloseConnection();
                return false;
            }
            finally
            {
                client.CloseConnection();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string commandDelete = $"DELETE Type_Task WHERE IDType = @ID; DELETE [Type] WHERE ID = @ID";
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandDelete, client.OpenConnection()))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = id;
                    var row = await cmd.ExecuteNonQueryAsync();
                }

                client.CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DataBaseProfile.Update()] Error: " + exception.Message);
                client.CloseConnection();
                return false;
            }
            finally
            {
                client.CloseConnection();
            }
        }

        public async Task<List<TypeDTO>> GetTypesTask(int idTask)
        {
            client.CloseConnection();
            string commandGetTypesTask = "SELECT [Type].ID, [Type].Title FROM [Type], Task, Type_Task " +
                                         $"WHERE[Type].ID = Type_Task.IDType AND Task.ID = Type_Task.IDTask AND Task.ID = @ID";
            var types = new List<TypeDTO>();
            try
            {
                using (SqlCommand cmd = new SqlCommand(commandGetTypesTask, client.OpenConnection()))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = idTask;

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var type = new TypeDTO(dataReader.GetInt32(0), dataReader.GetString(1));
                        types.Add(type);
                    }
                }
                client.CloseConnection();
                return types;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseType.GetTypesTask()] Error: " + exception.Message);
                client.CloseConnection();
                return null;
            }
            finally
            {
                client.CloseConnection();
            }
        }
    }
}
