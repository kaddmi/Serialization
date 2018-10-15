using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ConsumingData
{
    [Serializable]
    public class Book
    {
        [XmlAttribute]
        public string Title { set; get; }
        [XmlAttribute]
        public string Author { set; get; }
        [XmlElement]
        public int NumberOfPages { set; get; }
        [XmlElement]
        public int PublishingYear { set; get; }

        public Book()
        {
        }

        public Book(string title, string author, int numberOfPages, int publishingYear)
        {
            Title = title;
            Author = author;
            NumberOfPages = numberOfPages;
            PublishingYear = publishingYear;
        }

        public void ReadFromXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            using (StreamReader streamReader = new StreamReader(path))
            {
                Book book = (Book)serializer.Deserialize(streamReader);
                Title = book.Title;
                Author = book.Author;
                NumberOfPages = book.NumberOfPages;
                PublishingYear = book.PublishingYear;
            }         
        }

        public void SaveToXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                serializer.Serialize(streamWriter, this);
            }
        }

        public void SaveToJSON(string path)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, this);
                }
        }

        public void LoadFromJSON(string path)
        {
            string js = File.ReadAllText(path);
            Book book = JsonConvert.DeserializeObject<Book>(js);
            Title = book.Title;
            Author = book.Author;
            NumberOfPages = book.NumberOfPages;
            PublishingYear = book.PublishingYear;
        }
    }
}
