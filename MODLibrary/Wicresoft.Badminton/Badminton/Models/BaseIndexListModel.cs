using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;

namespace Badminton.Models
{
    public class BaseIndexListModel<TEntity>
    {
        public PagingContent<TEntity> PagingContent
        {
            get;
            set;
        }
    }

    public class PagingContent<TEntity>
    {
        public Int32 PageSize
        {
            get;
            set;
        }

        public Int32 CurrentPageIndex
        {
            get;
            set;
        }

        public Int32 TotalCount
        {
            get;
            set;
        }

        public Int32 TotalPage
        {
            get;
            set;
        }

        public List<TEntity> EntityList
        {
            get;
            set;
        }

        public String PagingViewContent
        {
            get;
            set;
        }

        public PagingContent()
        {

        }

        public PagingContent(IEnumerable<TEntity> list, Int32 pageIndex, Int32 pageSize = 10)
        {
            this.EntityList = new List<TEntity>();

            this.CurrentPageIndex = pageIndex;

            this.PageSize = pageSize;

            this.TotalCount = list.Count();

            this.TotalPage = (int)Math.Ceiling(TotalCount / (double)pageSize);

            EntityList.AddRange(list.Skip(pageIndex * this.PageSize).Take(this.PageSize));

            this.PagingViewContent = GetPagingViewContent();
        }

        public String GetPagingViewContent()
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


            strContent.Append("<table><tr><td>");
            strContent.AppendFormat("{0}&nbsp Items,{1}&nbsp Pages,Num.{2}&nbsp;&nbsp;", this.TotalCount, this.TotalPage, displayCurrentIndex);

            if (this.CurrentPageIndex == 0)
            {
                strContent.Append("<span>First</span>&nbsp;");
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
                strContent.Append("<span>Last</span>&nbsp;");
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