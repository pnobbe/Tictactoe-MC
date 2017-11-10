using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class UCT
    {
        public static double uctValue(int totalVisit, double nodeWinScore, int nodeVisit)
        {
            if (nodeVisit == 0)
            {
                return Int32.MaxValue;
            }
            return ((double)nodeWinScore / (double)nodeVisit)
              + 1.41 * Math.Sqrt(Math.Log(totalVisit) / (double)nodeVisit);
        }

        public static Node findBestNodeWithUCT(Node node)
        {

            int parentVisit = node.getState().getVisitCount();
            return node.getChildArray().OrderByDescending(n => uctValue(parentVisit, n.getState().getWinScore(), n.getState().getVisitCount())).First();
        }
    }
}
