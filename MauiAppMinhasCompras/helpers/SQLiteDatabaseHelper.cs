using MauiAppMinhasCompras.models;
using SQLite;

namespace MauiAppMinhasCompras.helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p); 
        }

        public Task<List<Produto>>  Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade =?, Preco=? where id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
                );
        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> Getall() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto where Descricao like '%"+ q +"%'";

            return _conn.QueryAsync<Produto>(sql);

        }


    }
}
