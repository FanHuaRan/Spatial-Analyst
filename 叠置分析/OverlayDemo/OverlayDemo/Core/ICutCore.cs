using OverlayDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    /// <summary>
    /// 多边形裁剪接口
    /// </summary>
    interface ICutCore
    {
        /// <summary>
        /// 多边形裁剪线段
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="points"></param>
        /// <returns>线集合</returns>
        List<List<MyPoint>> CutAlgorithm(MyPolygon polygon, List<MyPoint> points);
    }
}
