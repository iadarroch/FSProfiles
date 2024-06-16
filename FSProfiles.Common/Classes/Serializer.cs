using System.Xml.Serialization;

namespace FSProfiles.Common.Classes
{
    public static class Serializer
    {
        public static T DeserializeObject<T>(TextReader reader)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }

        public static T DeserializeFromFile<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                var serializer = new XmlSerializer(typeof(T));
                var result = (T)serializer.Deserialize(reader);
                return result;
            }
        }

        public static void SerializeObject<T>(TextWriter textWriter, T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(textWriter, obj);
        }

        public static void SerializeToFile<T>(this T subject, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                Serializer.SerializeObject(writer, subject);
            }
        }

    }
}