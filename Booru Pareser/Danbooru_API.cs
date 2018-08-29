using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;


namespace Booru_Pareser
{
    class Danbooru_API : Booru
    {
        public Danbooru_API(string _tag): base(_tag)
        {
            while (true)
            {
                if (getPics().Count > 0) // так как API Danbooru не предоставляет информации о страницах или о общем количестве 
                {                        // доступных(некоторые картинки доступны с премиум аккаунтом) или хотя бы всего картинок
                    current_page++;      // то для того что бы узнать сколько всего страниц нужно спарсить предварительно все страницы, потому предпочтителней использовать HTML парсинг
                    total_page++;
                }
                else
                {
                    current_page = 1;
                    break;
                }
            }
        }
        public override string Url
        {
            get
            {
                url = "https://danbooru.donmai.us/posts.xml?post[tags]=" + Tag + "&page=" + current_page; // переопределение запроса 
                return url;
            }
        }

        public override List<string> getPics() // переопределение метода на получение листа ссылок
        {
            List<string> pic_list = new List<string>();
            XmlDocument xml_page = new XmlDocument();
            xml_page.LoadXml(getPage()); // скачивание xml файла 
            XmlElement root = xml_page.DocumentElement; 
            if (root.ChildNodes.Count > 0) // есть ли элементы в xml файле
            {
                foreach (XmlNode node in root) // просмотр всех элементов xml файла
                {
                    foreach (XmlNode child in node.ChildNodes) // просмотр элементов элементов 
                    {
                        if (child.Name == "file-url") // если совпадает имя
                        {
                            pic_list.Add(child.InnerText.Replace("https", "http")); // так как могут быть проблемы с защищеным соединением, то нужна замена https на http
                        }
                    }
                }
            }
            else  if(current_page == 1 )error = true; // если нет элементов и сейчас только первая страница то ошибка
            return pic_list;
        }
    }
}
