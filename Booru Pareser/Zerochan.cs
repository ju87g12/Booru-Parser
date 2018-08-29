using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Zerochan : Booru
    {
        public Zerochan(string _tag) : base(_tag)
        {
            string html_0 = "<a href=" + '"' + "?p=2"; // html теги, записаны в переменные для удобности и читаемости
            string page = getPage();
            if (page != "") // Zerochan на отсутсвие тега отвечает 404, потому метод getPage() который определен в базовом классе выдает пустую строку
            {
                page = page.Substring(page.IndexOf(html_0) - 15);
                page = page.Substring(0, 15);
                page = page.Replace(" ", "");
                page = page.Replace(",", "");
                page = page.Substring(page.IndexOf("of") + 2);
                total_page = Convert.ToInt32(page);
            }
            else { error = true; }
        }
        public override string Url
        {
            get
            {
                url = "https://www.zerochan.net/" + Tag + "?p=" + current_page; // переопределение запроса 
                return url;
            }
        }

        public override List<string> getPics() // переопределение метода на получение листа ссылок
        {
            List<string> pic_list = new List<string>();
            string html_0 = "<ul id=" + '"' + "thumbs2"; // html теги, записаны в переменные для удобности и читаемости
            string html_1 = "<a href=" + '"' + 'h';
            string page = getPage();
            if (page.Contains(html_0) == false)
            {
                return pic_list;
            }
            page = page.Substring(page.IndexOf(html_0));
            page = page.Substring(0, page.IndexOf("</ul>"));
            while (page.Contains(html_1))
            {
                string buff = page.Substring(page.IndexOf(html_1) + html_1.Count() - 1);
                pic_list.Add(buff.Substring(0, buff.IndexOf('"')));
                page = page.Substring(page.IndexOf(pic_list[pic_list.Count - 1]));
            }
            return pic_list;
        }

    }
}
