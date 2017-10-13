using System;

namespace Extension.ExtraTypes.IntVector2
{
    [Serializable]
    public struct IntVector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public IntVector2(int xCoordinate, int yCoordinate)
        {
            X = xCoordinate;
            Y = yCoordinate;
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(this, obj))
                return true;

            if (!(obj is IntVector2))
                return false;

            IntVector2 target = (IntVector2)obj;
            return this.X == target.X && this.Y == target.Y;
        }

        public static bool operator == (IntVector2 intVector2A, IntVector2 intVector2B)
        {
            if (IsNull(intVector2A) && !IsNull(intVector2B))
                return false;

            if (!IsNull(intVector2A) && IsNull(intVector2B))
                return false;

            if (IsNull(intVector2A) && IsNull(intVector2B))
                return true;

            return intVector2A.X.Equals(intVector2B.X) && intVector2A.Y.Equals(intVector2B.Y);
        }

        public static bool operator != (IntVector2 intVector2A, IntVector2 intVector2B)
        {
            return !(intVector2A == intVector2B);
        }

        public override int GetHashCode()
        {
            int SEED = 26;
            return (X.GetHashCode() + Y.GetHashCode()) * SEED;
        }

        public override string ToString()
        {
            return string.Format("[{0} : {1}]", X, Y);
        }

        private static bool IsNull(object obj)
        {
            return ReferenceEquals(obj, null);
        }
    }
}