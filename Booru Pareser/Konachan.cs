using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Konachan : Booru
    {
        public Konachan(string _tag) : base(_tag)
        {
            string html_0 = "<a href=" + '"' + "/post?page="; // html теги, записаны в переменные для удобности и читаемости
            string html_1 = "class=" + '"' + "pagination";
            string page = this.getPage();
            if (page.Contains("thumb"))
            {
                if (page.Contains(html_1)) // если страница содержит нужный html код
                {
                    page = page.Substring(page.IndexOf(html_1));
                    string buff = "";
                    while (page.Contains(html_0)) // так как нет определенного тега который описывает последнюю страницу, нужно перебирать все теги со страницами
                    {
                        page = page.Substring(page.IndexOf(html_0) + html_0.Count());
                        buff = page;
                    }
                    buff = buff.Substring(0, page.IndexOf('&'));
                    total_page = Convert.ToInt32(buff);
                }
            }
            else error = true; // если код отсутствует то ошибка
        }

        public override string Url
        {
            get
            {
                url = "http://konachan.com/post?page=" + current_page + "&tags=" + Tag; // переопределение запроса 
                return url;
            }
        }

        public override List<string> getPics()  // переопределение метода на получение листа ссылок
        {
            List<string> pic_list = new List<string>();
            string html_0 = "class=" + '"' + "directlink ";  // html теги, записаны в переменные для удобности и читаемости
            string html_1 = "href=" + '"';
            string page = getPage();
            while (page.Contains(html_0)) // пока страница содержит код
            {
                string buff = page.Substring(page.IndexOf(html_0));
                buff = buff.Substring(buff.IndexOf(html_1) + html_1.Count());
                pic_list.Add(buff.Substring(0, buff.IndexOf('"')));
                page = page.Substring(page.IndexOf(pic_list[pic_list.Count - 1]));
            }
            return pic_list;
        }
    }
}
