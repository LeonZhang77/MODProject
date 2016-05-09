using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wicresoft.MODLibrarySystem.Web.Models;

namespace Wicresoft.MODLibrarySystem.Web.Controllers
{
    public class ChartController : Controller
    {

        public ActionResult Demo()
        {
            return View();
        }
        public JsonResult GetDemoLineChartJson()
        {
            _getLineChartModelMethod = () =>
            {
                return new LabelAndDataForChart()
                {
                    XLabels = new string[] { "Lip", "Chang" },
                    YData = new int[] { 0, 100 }
                };
            };
            return Json(_getLineChartModelMethod(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDemoPieChartJson()
        {
            _getPieChartModelMethod = () =>
            {
                return new DataAndColorForChart[]
                {
                    new DataAndColorForChart()
                    {
                        value=50,
                        color="#F38630",
                        highlight="#616774",
                        label="Chang"
                    },
                    new DataAndColorForChart()
                    {
                        value =50,
                        color="#E0E4CC",
                        highlight="#E0E4CC",
                        label="Lip"
                    }
                };
            };
            return Json(_getPieChartModelMethod(), JsonRequestBehavior.AllowGet);
        }

        #region Base
        /// <summary>
        /// Delegate of get line chart model method.
        /// </summary>
        Func<LabelAndDataForChart> _getLineChartModelMethod;

        /// <summary>
        /// Delegate of get pie chart model method.
        /// </summary>
        Func<DataAndColorForChart[]> _getPieChartModelMethod;




        #endregion
    }
}