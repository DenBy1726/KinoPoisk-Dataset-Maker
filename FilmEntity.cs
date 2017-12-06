using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskParser
{
    [DataContract]
    public class FilmEntity
    {
        private static int g_id = 0;
        [DataMember]
        private int id;
        [DataMember]
        private string name;
        [DataMember]
        private string author;
        [DataMember]
        private DateTime date;
        [DataMember]
        private string title;
        [DataMember]
        private string text;
        [DataMember]
        private string note;

        public FilmEntity(string name, string author, DateTime date, string title, string text,string note)
        {
            this.id = g_id++;
            this.name = name;
            this.author = author;
            this.date = date;
            this.title = title;
            this.text = text;
            this.note = note;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Author { get => author; set => author = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Title { get => title; set => title = value; }
        public string Text { get => text; set => text = value; }
        public string Note { get => note; set => note = value; }
    }
}
