using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wicresoft.MODLibrarySystem.Web.Models
{
    /// <summary>
    /// Chart data model for those types:
    /// Polar area
    /// Pie
    /// Doughnut
    /// </summary>
    public class DataAndColorForChart
    {
        public int value { get; set; }
        public string color { get; set; }
        public string highlight { get; set; }
        public string label { get; set; }
    }

    /// <summary>
    /// Chart data model for those types:
    /// Line
    /// Bar
    /// Radar
    /// </summary>
    public class LabelAndDataForChart
    {
        public string[] XLabels { get; set; }
        public int[] YData { get; set; }
        public int[] YData2 { get; set; }
    }
}