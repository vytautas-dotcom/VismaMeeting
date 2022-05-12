namespace VismaMeeting.Serialization
{
    internal interface ISerialazer<T>
    {
        void JsonSerialize(T obj);
        T Deserialize();
    }
}
