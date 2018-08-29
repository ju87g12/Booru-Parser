using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booru_Pareser
{
    class Yande_re_API : Konachan_API
    {
        public Yande_re_API(string _tag) : base(_tag)
        { }

        public override string Url
        {
            get
            {
                url = "https://yande.re/post.xml?tags=" + Tag + "&page=" + current_page; // API Yande.re полностью аналогичен API Konachan за исключенем запроса
                return url;
            }
        }
    }
}
