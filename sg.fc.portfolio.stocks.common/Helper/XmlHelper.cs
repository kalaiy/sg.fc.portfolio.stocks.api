using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Sg.Fc.Portfolio.Stocks.Common.Helper
{
    public static class XmlHelper
    {
        static ReaderWriterLock locker = new ReaderWriterLock();


        public static T DeserializeToObject<T>(string filepath) where T : new()
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            if (File.Exists(filepath))
            {
                using (FileStream sr = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return (T)ser.Deserialize(sr);
                }
            }
            else
            {
                return new T();
            }
        }

        public static void SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());
                locker.AcquireWriterLock(int.MaxValue);
                using (StreamWriter writer = new StreamWriter(xmlFilePath))
                {
                    xmlSerializer.Serialize(writer, anyobject);
                }
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }
    }
}
