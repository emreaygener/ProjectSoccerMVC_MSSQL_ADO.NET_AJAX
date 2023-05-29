using ProjectSoccer.DataAccessLayer.Repositories.Shared;
using ProjectSoccer.Models;
using ProjectSoccer.Models.Shared;
using System.Data.SqlClient;

namespace ProjectSoccer.DataAccessLayer.Repositories
{
    public class ClubRepository : GenericRepository<Club>
    {
        protected override string TableName => "[ProjectSoccer].[dbo].[Clubs]";

        public ClubRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override async Task<IList<Club>> ExecuteQuery(string query)
        {
            ConnectionSwitch();

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            IList<Club> result = new List<Club>();

            while (reader.Read())
            {
                Club club = new()
                {
                    Id = Convert.ToInt32(reader[nameof(Club.Id)]),
                    Name = reader[nameof(Club.Name)].ToString(),
                    ShortName = reader[nameof(Club.ShortName)].ToString(),
                    Logo = reader[nameof(Club.Logo)] != DBNull.Value ? reader[nameof(Club.Logo)].ToString() : null
                };

                result.Add(club);
            }

            ConnectionSwitch();

            return result;
        }


        protected override async Task ExecuteNonQuery(string command, Club entity, bool isUpdate = false)
        {
            ConnectionSwitch();

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Club.Name)}", entity.Name));
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Club.ShortName)}", entity.ShortName));
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Club.Logo)}", entity.Logo));

            if (isUpdate)
                sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(BaseEntity.Id)}", entity.Id));

            await sqlCommand.ExecuteNonQueryAsync();

            ConnectionSwitch();
        }

        public override async Task Create(Club entity)
        {
            string command = @$"INSERT INTO {TableName} (Name, ShortName, Logo) Values(@Name, @ShortName, @Logo)";

            await ExecuteNonQuery(command, entity);
        }

        public override async Task Update(Club entity)
        {
            string command = $@"UPDATE {TableName} SET Name = @Name, ShortName = @ShortName, Logo = @Logo WHERE Id = @Id";

            await ExecuteNonQuery(command, entity, isUpdate: true);
        }

        public override async Task<IList<Club>> GetAll()
        {
            string query = $"SELECT * FROM {TableName}";
            IList<Club> result = await ExecuteQuery(query);

            return result;
        }

        public override async Task<Club> GetById(int id)
        {
            string query = $"SELECT * FROM {TableName} WHERE Id = {id}";
            Club result = (await ExecuteQuery(query)).SingleOrDefault();

            return result;
        }

        public override async Task Delete(int id)
        {
            string command = $@"DELETE FROM {TableName} WHERE Id = {id};";

            ConnectionSwitch();
            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(Club.Id)}", id);
            await sqlCommand.ExecuteNonQueryAsync();
            ConnectionSwitch();
        }
    }
}
