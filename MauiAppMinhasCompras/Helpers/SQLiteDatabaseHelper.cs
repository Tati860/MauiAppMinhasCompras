
using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) =>
            _conn.InsertAsync(p);

        public Task<int> Update(Produto p)
            => _conn.UpdateAsync(p);


        public Task<int> Delete(int id)
            => _conn.DeleteAsync<Produto>(id);

        public Task<List<Produto>> GetAll()
            => _conn.Table<Produto>().ToListAsync();
       

        public Task<List<Produto>> Search(string q)
        {

            return _conn.Table<Produto>()
                .Where(i => i.Descricao.Contains(q)).ToListAsync();
        }
    } // Fecha classe SQLiteDatabaseHelper
} // Fecha namespace MauiAppMinhasCompras.Helpers

