using ProjectSoccer.DataAccessLayer.Repositories.Shared;
using ProjectSoccer.Models;
using ProjectSoccer.Models.Shared;
using System.Data.SqlClient;

namespace ProjectSoccer.DataAccessLayer.Repositories
{
    public class PlayerRepository : GenericRepository<Player>
    {
        protected override string TableName => "[ProjectSoccer].[dbo].[Players]";

        public PlayerRepository(SqlConnection connection) : base(connection)
        {
        }

        protected override async Task<IList<Player>> ExecuteQuery(string query)
        {
            ConnectionSwitch();

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = await command.ExecuteReaderAsync();
            IList<Player> result = new List<Player>();

            while (reader.Read())
            {
                Player player = new()
                {
                    Id = Convert.ToInt32(reader[nameof(Player.Id)]),
                    FirstName = reader[nameof(Player.FirstName)].ToString(),
                    LastName = reader[nameof(Player.LastName)].ToString(),
                    DateOfBirth = Convert.ToDateTime(reader[nameof(Player.DateOfBirth)]),
                    ClubId = Convert.ToInt32(reader[nameof(Player.ClubId)]),
                    CurrentClub = reader[nameof(Club.Logo)].ToString()
                };

                result.Add(player);
            }

            ConnectionSwitch();

            return result;
        }

        protected override async Task ExecuteNonQuery(string command, Player entity, bool isUpdate = false)
        {
            ConnectionSwitch();

            SqlCommand sqlCommand = new SqlCommand(command, connection);
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Player.FirstName)}", entity.FirstName));
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Player.LastName)}", entity.LastName));
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Player.DateOfBirth)}", entity.DateOfBirth));
            sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(Player.ClubId)}", entity.ClubId));

            if (isUpdate)
                sqlCommand.Parameters.Add(new SqlParameter($"@{nameof(BaseEntity.Id)}", entity.Id));

            await sqlCommand.ExecuteNonQueryAsync();

            ConnectionSwitch();
        }
        public override async Task Create(Player entity)
        {
            string command = @$"INSERT INTO {TableName} (FirstName, LastName, DateOfBirth, ClubId) Values(@FirstName, @LastName, @DateOfBirth, @ClubId)";

            await ExecuteNonQuery(command, entity);
        }

        public override async Task Update(Player entity)
        {
            string command = $@"UPDATE {TableName} SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, ClubId = @ClubId WHERE Id = @Id";

            await ExecuteNonQuery(command, entity, isUpdate: true);
        }

        public override async Task<IList<Player>> GetAll()
        {
            string query = $"SELECT * FROM {TableName} JOIN Clubs ON {TableName}.ClubId = Clubs.Id";
            IList<Player> result = await ExecuteQuery(query);

            return result;
        }

        public override async Task<Player> GetById(int id)
        {
            string query = $"SELECT * FROM {TableName} JOIN Clubs ON {TableName}.ClubId = Clubs.Id WHERE Players.Id = {id}";
            Player result = (await ExecuteQuery(query)).SingleOrDefault();

            return result;
        }

        public override async Task Delete(int Id)
        {
            string command = $@"DELETE FROM {TableName} WHERE Id = {Id};";

            ConnectionSwitch();
            SqlCommand sqlCommand = new SqlCommand(command,connection);
            sqlCommand.Parameters.AddWithValue($"@{nameof(Player.Id)}", Id);
            await sqlCommand.ExecuteNonQueryAsync();
            ConnectionSwitch();
        }
    }
}
