using System;

namespace Planets
{
    public static class PlanetsExtensions
    {
        public static void TransferTo(this Slot from, Slot to, int limitCount = 0)
        {
            // var count = limitCount > 0 ? Math.Min(limitCount, from.Count) : from.Count;
            //
            // if (from.Item != to.Item && to.Item != null)
            // {
            //     var item = to.Item;
            //     var oldCount = to.Count;
            //
            //     to.Item = from.Item;
            //     to.Count = from.Count;
            //
            //     from.Item = item;
            //     from.Count = oldCount;
            // }
            // else
            // {
            //     to.Count += count;
            //     to.Item = from.Item;
            //
            //     from.Count -= count;
            //     if (from.Count == 0)
            //     {
            //         from.Item = null;
            //     }
            // }

            if (from.Item != null && to.Item == null)
            {
                to.Item = from.Item;
                to.Count = from.Count;
                from.Item = null;
                from.Count = 0;
            }
        }
    }
}
