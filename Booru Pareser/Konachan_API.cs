using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Konachan_API : Booru
    {
        public Konachan_API(string _tag): base(_tag)
        {
            XmlDocument xml_page = new XmlDocument();
            xml_page.LoadXml(getPage()); // получение xml документа
            XmlElement root = xml_page.DocumentElement;
            total_page = (int)Math.Ceiling(Convert.ToDouble(root.Attributes[0].Value) / 21); // так как API Konachan содержит количество картинок по заданому тегу,
            if (total_page == 0) error = true;  // если страниц нет то ошибка                // можно расчитать количество страниц
        }
        public override string Url
        {
            get
            {
                url = "https://konachan.com/post.xml?tags=" + Tag + "&page=" + current_page; // переопределение запроса 
                return url;
            }
        }

        public override List<string> getPics()
        {
            List<string> pic_list = new List<string>();
            XmlDocument xml_page = new XmlDocument();
            xml_page.LoadXml(getPage());  // получение xml документа
            XmlElement root = xml_page.DocumentElement; 
            foreach (XmlNode node in root.ChildNodes) // простотр всех элементов xml документа
            {
                foreach (XmlNode child in node.Attributes) // просмтор аттрибутов элемента
                {
                    if (child.Name == "file_url") // если содержится аттрибут
                    {
                        pic_list.Add(child.InnerText);
                    }
                }
            }
            return pic_list;
        }
    }
}
