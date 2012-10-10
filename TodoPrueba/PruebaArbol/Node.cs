using System;

namespace PruebaArbol
{
    public class Node : IEquatable<Node>
    {
        public Node(int descriptor)
        {
            Descriptor = descriptor;
        }

        public int Descriptor { get; set; }
        public Node Parent { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }

        public bool IsRoot()
        {
            return Parent == null;
        }

        public bool HasLeftChild()
        {
            return LeftChild != null;
        }

        public bool HasRightChild()
        {
            return RightChild != null;
        }

        #region Equality members

        public bool Equals(Node other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Descriptor == other.Descriptor && Equals(Parent, other.Parent) && Equals(LeftChild, other.LeftChild) && Equals(RightChild, other.RightChild);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Descriptor;
                hashCode = (hashCode*397) ^ (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LeftChild != null ? LeftChild.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (RightChild != null ? RightChild.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}