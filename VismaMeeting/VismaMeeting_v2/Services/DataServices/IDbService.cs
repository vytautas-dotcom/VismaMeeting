namespace VismaMeeting_v2.Services.DataServices
{
    public interface IDbService<T>
    {
        void Save(T obj);
        T Get();
    }
}
