using OverlayDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    interface ICutCore
    {
        List<List<MyPoint>> CutAlgorithm(MyPolygon polygon, List<MyPoint> points);
    }
}
