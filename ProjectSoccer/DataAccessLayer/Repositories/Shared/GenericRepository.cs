using ProjectSoccer.Models.Shared;
using System.Data.SqlClient;

namespace ProjectSoccer.DataAccessLayer.Repositories.Shared;

public abstract class GenericRepository<T> where T : BaseEntity
{
    protected readonly SqlConnection connection;
    protected abstract string TableName { get; }

    protected GenericRepository(SqlConnection connection)
    {
        this.connection = connection;
    }

    protected abstract Task<IList<T>> ExecuteQuery(string query);
    protected abstract Task ExecuteNonQuery(string command, T entity, bool isUpdate = false);

    public abstract Task Create(T entity);
    public abstract Task Update(T entity);
    public abstract Task Delete(int id);
    public abstract Task<IList<T>> GetAll();
    public abstract Task<T> GetById(int id);

    //public async Task<IList<T>> GetAll()
    //{
    //    string query = $"SELECT * FROM {TableName}";
    //    IList<T> result = await ExecuteQuery(query);

    //    return result;
    //}

    //public async Task<T> GetById(int id)
    //{
    //    string query = $"SELECT * FROM {TableName} WHERE Id = {id}";
    //    T result = (await ExecuteQuery(query)).SingleOrDefault();

    //    return result;
    //}

    protected void ConnectionSwitch()
    {
        if (connection.State == System.Data.ConnectionState.Closed)
            connection.Open();
        else if(connection.State == System.Data.ConnectionState.Open)
            connection.Close();
    }
}
