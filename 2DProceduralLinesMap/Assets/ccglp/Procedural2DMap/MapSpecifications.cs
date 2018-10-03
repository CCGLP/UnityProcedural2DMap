using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ccglp.Procedural
{
    [System.Serializable]
    public struct MapSpecifications
    {
        [SerializeField]
        private uint branchQuantity, branchLongitude, numberOfLines;

        public MapSpecifications(uint branchQuantity, uint branchLongitude, uint numberOfLines)
        {
            this.branchLongitude = branchLongitude;
            this.branchQuantity = branchQuantity;
            this.numberOfLines = numberOfLines;
        }

        public uint BranchLongitude
        {
            get
            {
                return branchLongitude;
            }
        }

        public uint BranchQuantity
        {
            get
            {
                return branchQuantity;
            }
        }

        public uint NumberOfLines
        {
            get
            {
                return numberOfLines;
            }
        }
    }
}