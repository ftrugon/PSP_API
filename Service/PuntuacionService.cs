using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class PuntuacionService
{
    private readonly IMongoCollection<Puntuacion> _booksCollection;

    public PuntuacionService(
        IOptions<DbSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Puntuacion>(
            bookStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<Puntuacion>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Puntuacion?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Puntuacion newPuntuacion) =>
        await _booksCollection.InsertOneAsync(newPuntuacion);

    public async Task UpdateAsync(string id, Puntuacion updatedPuntuacion) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedPuntuacion);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);

}