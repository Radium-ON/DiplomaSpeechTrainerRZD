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
    public class DataBaseStudent : IDataBaseStudent<StudentDto, bool>
    {
        private readonly DatabaseConnection _client;
        public DataBaseStudent()
        {
            _client = DatabaseConnection.Source;
        }

        #region Implementation of IDatabase<StudentDto,bool>

        public async Task<List<StudentDto>> SelectAllAsync()
        {
            const string command = "SELECT * FROM Student";
            var students = new List<StudentDto>();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var firstName = dataReader.GetString(1);
                        var lastName = dataReader.GetString(2);
                        var studentCode = dataReader.GetString(3);

                        students.Add(new StudentDto(id, firstName, lastName, studentCode, null));
                    }
                }
                _client.CloseConnection();

                foreach (var student in students)
                {
                    student.SetGroup(await GetStudentGroupAsync(student.Id));
                    student.SetTrainings(await GetStudentTrainingsAsync(student.Id));
                }

                return students;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseStudent.SelectAllAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        public async Task<StudentDto> SelectByIdAsync(int idObject)
        {
            const string command = "SELECT * FROM Student WHERE Id = @ID";
            var student = new StudentDto();
            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", idObject);
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        var id = dataReader.GetInt32(0);
                        var firstName = dataReader.GetString(1);
                        var lastName = dataReader.GetString(2);
                        var studentCode = dataReader.GetString(3);

                        student = new StudentDto(id, firstName, lastName, studentCode, null);
                    }
                }

                student.SetGroup(await GetStudentGroupAsync(student.Id));
                student.SetTrainings(await GetStudentTrainingsAsync(student.Id));

                _client.CloseConnection();
                return student;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseStudent.SelectByIdAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return null;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<List<TrainingDto>> GetStudentTrainingsAsync(int studentId)
        {
            var db = new DataBaseTraining();
            return await db.GetTrainingsByStudentAsync(studentId);
        }

        private async Task<GroupDto> GetStudentGroupAsync(int studentId)
        {
            var db = new DataBaseGroup();
            return await db.GetGroupByStudentAsync(studentId);
        }

        public async Task<bool> UpdateAsync(StudentDto newObject)
        {
            throw new NotImplementedException();//todo
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();//todo
        }

        #endregion

        #region Implementation of IDataBaseStudent<StudentDto,bool>

        public async Task<bool> CreateAsync(StudentDto newObject)
        {
            const string command = "INSERT INTO Student" +
                                   "(FirstName, LastName, StudentCode)" +
                                   "VALUES(@FirstName, @LastName, @StudentCode)";
            const string lastIndexCommand = "SELECT IDENT_CURRENT('Student') AS [IDENT_CURRENT]";

            decimal? lastIndex = null;

            try
            {
                using (var cmd = new SqlCommand(command, _client.OpenConnection()))
                {
                    cmd.Parameters.AddWithValue("@FirstName", newObject.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newObject.LastName);
                    cmd.Parameters.AddWithValue("@StudentCode", newObject.StudentCode);
                    var row = await cmd.ExecuteNonQueryAsync();
                    Debug.WriteLine("[DatabaseStudent.CreateAsync()] Rows: " + row);
                }
                _client.CloseConnection();
                using (var cmd = new SqlCommand(lastIndexCommand, _client.OpenConnection()))
                {
                    var dataReader = await cmd.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        lastIndex = dataReader.GetDecimal(0);
                    }
                }
                _client.CloseConnection();
                return await InsertStudentGroupAsync((int?)lastIndex, newObject.Group);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("[DatabaseStudent.CreateAsync()] Error: " + exception.Message);
                _client.CloseConnection();
                return false;
            }
            finally
            {
                _client.CloseConnection();
            }
        }

        private async Task<bool> InsertStudentGroupAsync(int? lastIndex, GroupDto group)
        {
            const string insertGroupCommand = "INSERT Student_Group VALUES (@IDStudent, @IDGroup)";
            try
            {
                if (lastIndex != null)
                {

                    using (var cmd = new SqlCommand(insertGroupCommand, _client.OpenConnection()))
                    {
                        cmd.Parameters.Add("@IDStudent", SqlDbType.Int);
                        cmd.Parameters["@IDStudent"].Value = lastIndex;

                        cmd.Parameters.Add("@IDGroup", SqlDbType.Int);
                        cmd.Parameters["@IDGroup"].Value = group.Id;

                        await cmd.ExecuteNonQueryAsync();
                    }
                    _client.CloseConnection();

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

        #endregion
    }
}