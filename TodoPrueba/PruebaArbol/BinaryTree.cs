using System.Collections.Generic;

namespace PruebaArbol
{
    public class BinaryTree
    {
        private readonly List<Node> _listOfNodes;

        public BinaryTree(int rootValue)
        {
            _listOfNodes = new List<Node> {new Node(rootValue)};
        }

        public List<Node> ListOfNodes
        {
            get { return _listOfNodes; }
        }

        public Node GetRoot()
        {
            return _listOfNodes.Find(node => node.IsRoot());
        }

        public Node AddLeftChild(int value, Node parentNode)
        {
            var newLeftChild = new Node(value) {Parent = parentNode};
            parentNode.LeftChild = newLeftChild;
            _listOfNodes.Add(newLeftChild);
            return newLeftChild;
        }

        public Node AddRightChild(int value, Node parentNode)
        {
            var newRightChild = new Node(value) { Parent = parentNode };
            parentNode.RightChild = newRightChild;
            _listOfNodes.Add(newRightChild);
            return newRightChild;
        }

        public List<int> GetDeepFirstOrder(List<int> deepFirstList, Node initialNode)
        {
            var currentNode = initialNode;
            while (currentNode != null)
            {
                if (!deepFirstList.Contains(currentNode.Descriptor))
                {
                    deepFirstList.Add(currentNode.Descriptor);
                }

                if (currentNode.HasLeftChild() && !deepFirstList.Contains(currentNode.LeftChild.Descriptor))
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.HasRightChild() && !deepFirstList.Contains(currentNode.RightChild.Descriptor))
                {
                    currentNode = currentNode.RightChild;
                }
                else if (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                }
                else
                {
                    currentNode = null;
                }
            }
            

            return deepFirstList;
        }
    }
}