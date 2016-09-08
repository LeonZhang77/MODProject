using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wicresoft.MODLibrarySystem.Entity;

namespace Wicresoft.MODLibrarySystem.Unity.Helper
{
    public class PagingContentFormat2 <TEntity> : PagingContent<TEntity>
    {
        public PagingContentFormat2():base()
        {
           
        }
        public PagingContentFormat2(IEnumerable<TEntity> list, Int32 pageIndex, Int32 pageSize = UntityContent.PageSize)
            :base(list, pageIndex, pageSize)
        {
            this.PagingViewContent = GetPagingViewContent();
        }
        
        public new String GetPagingViewContent()
        {
            StringBuilder strContent = new StringBuilder();
            StringBuilder strurl = new StringBuilder();
            Int32 displayCurrentIndex = this.CurrentPageIndex + 1;
            NameValueCollection collection = HttpContext.Current.Request.QueryString;

            strurl.Append(HttpContext.Current.Request.Url.AbsolutePath + "?pageindex={0}");
            string[] keys = collection.AllKeys;
            if (keys.Length > 0)
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i].ToLower() != "pageindex")
                        strurl.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
                }
            }


            strContent.Append("<table><tr><td id=\"pagesData\">");
            strContent.AppendFormat("Total:{0}, {2}/{1} &nbsp Pages </td></tr><tr><td>", this.TotalCount, this.TotalPage, displayCurrentIndex);

            if (this.CurrentPageIndex == 0)
            {
                strContent.Append("<span id=\"disableFirst\">First</span>&nbsp;");
            }
            else
            {
                string url1 = string.Format(strurl.ToString(), 0);

                strContent.AppendFormat("<span><a href={0}>First</a></span>&nbsp;", url1);
            }

            if (this.CurrentPageIndex > 0)
            {
                string url1 = string.Format(strurl.ToString(), this.CurrentPageIndex - 1);

                strContent.AppendFormat("<span><a href={0}>Previous</a></span>&nbsp;", url1);
            }

            if (this.CurrentPageIndex < this.TotalPage - 1)
            {
                string url1 = string.Format(strurl.ToString(), this.CurrentPageIndex + 1);
                strContent.AppendFormat("<span><a href={0}>Next</a></span>&nbsp;", url1);
            }

            if (this.CurrentPageIndex == this.TotalPage - 1)
            {
                strContent.Append("<span id=\"disableLast\">Last</span>&nbsp;");
            }
            else
            {
                string url1 = string.Format(strurl.ToString(), this.TotalPage - 1);
                strContent.AppendFormat("<span><a href={0}>Last</a></span>&nbsp;", url1);
            }

            strContent.Append("</td></tr></table>");

            return strContent.ToString();
        }
    }
}
