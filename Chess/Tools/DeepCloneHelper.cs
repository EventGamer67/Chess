using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class DeepCopyHelper
{
    /*
     * ПОНЯТИЯ НЕ ИМЕЮ ЧТО ТУТ ПРОИСХОДИТ НО ОНО ЗАМЕЧАТЕЛЬНО РАБОТАЕТ
     */
    public static T DeepCopy<T>(T obj)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new ArgumentException("The type must be serializable.", nameof(obj));
        }

        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        // Создаем поток для сериализации
        using (var memoryStream = new MemoryStream())
        {
            // Создаем объект BinaryFormatter
            IFormatter formatter = new BinaryFormatter();

            // Сериализуем объект в поток
            formatter.Serialize(memoryStream, obj);

            // Возвращаем поток в начальное положение
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Десериализуем объект из потока и возвращаем копию
            return (T)formatter.Deserialize(memoryStream);
        }
    }
}
