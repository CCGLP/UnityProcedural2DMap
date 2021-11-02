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
            => branchLongitude;

        public uint BranchQuantity
            => branchQuantity;

        public uint NumberOfLines
            => numberOfLines;
    }
}
