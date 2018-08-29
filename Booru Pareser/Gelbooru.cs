using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Gelbooru : Booru
    {
        public Gelbooru(string _tag) :base (_tag)
        {
            while (true)
            {
                if (getPage().Contains("thumbnail-preview") == true) // ручной парсинг страниц так как Gelbooru не предоставляет количество страниц
                {                                                    // более того, предоставляемая цифра картинок тоже не является доступной, и в реальности их меньше
                    current_page++; 
                    total_page++;
                }
                else
                {
                    if(total_page == 1) error = true;
                    break;
                }
            }
            current_page = 1;
        }
        delegate int True_page();
        public override string Url
        {
            get
            {
                True_page get_num = () => // лямбда, запросы в Gelbooru осуществляются не по страницам(хоть и про страницам), а по количество картинок на странице
                {                         // при этом именно в HTML это жестко закреплено на цифре 42
                    int r = 0;
                    if (current_page > 1) r = (current_page - 1) * 42;
                    return r;
                };
                url = "https://gelbooru.com/index.php?page=post&s=list&tags=" + Tag + "&pid=" + get_num();  // переопределение запроса
                return url;
            }
        }

        public override List<string> getPics()  // переопределение метода на получение листа ссылок
        {
            List<string> pic_list = new List<string>();
            string html_0 = "</div>"; // html теги, записаны в переменные для удобности и читаемости
            string html_1 = "href=" + '"' + "//";
            string html_2 = "thumbnail-preview";
            string html_3 = "src=" + '"';
            string page = getPage();
            while (page.Contains(html_2)) // пока на странице есть HTML код
            {
                string buff; // в первом этапе собираются ссылки не на сами картинки, а на ссылки со страницами с ними
                buff = page.Substring(page.IndexOf(html_2));
                buff = buff.Substring(buff.IndexOf(html_1) + html_1.Count());
                page = page.Substring(page.IndexOf(html_0) + html_0.Count());
                buff = buff.Substring(0, buff.IndexOf('"')).Replace("amp;", "");
                if (pic_list.Contains(buff) == false) pic_list.Add(buff);
            }
            for (int i = 0; i < pic_list.Count; i++)
            {
                string code = new WebClient().DownloadString("https://" + pic_list[i]);
                code = code.Substring(code.IndexOf("<img "));
                code = code.Substring(code.IndexOf(html_3) + html_3.Count());
                pic_list[i] = code.Substring(0, code.IndexOf('"'));
            }
            return pic_list;
        }
    }
}
