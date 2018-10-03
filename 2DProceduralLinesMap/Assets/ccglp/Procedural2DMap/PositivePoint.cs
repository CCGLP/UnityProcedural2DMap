using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ccglp.Procedural
{

    [System.Serializable]
    public struct PositivePoint
    {
        public uint x;
        public uint y;

        public PositivePoint(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }

        public void Change(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }
    }

}