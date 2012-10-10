using System;
using System.Collections.Generic;
using PruebaArbol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebaArbolTests
{
    [TestClass]
    public class BinaryTreeTests
    {
        [TestMethod]
        public void Bt_CreatingNewTree_ShouldHaveOneNode()
        {
            // Arrange and act
            var sut = new BinaryTree(0);

            // Assert
            Assert.AreEqual(1, sut.ListOfNodes.Count);
        }

        /// <summary>
        /// A unit test for GetRoot
        /// </summary>
        [TestMethod]
        public void GetRoot_WhenJustCreated_ReturnOnlyNodeThere()
        {
            //Arrange
            var sut = new BinaryTree(1);

            //Act
            var root = sut.GetRoot();

            //Assert
            Assert.AreEqual(1, root.Descriptor);
        }

       [TestMethod]
        public void CreateRightChild_ShouldCreateIt()
        {
            // Arrange and act
            var sut = new BinaryTree(1);
            var root = sut.GetRoot();

            // Act
            var rightChild = sut.AddRightChild(2, root);

            // Assert
            Assert.AreEqual(2, sut.ListOfNodes.Count);
            Assert.AreEqual(2, rightChild.Descriptor);
            Assert.AreEqual(root, rightChild.Parent);
            Assert.AreEqual(rightChild, root.RightChild);
        }

       [TestMethod]
       public void CreateLeftChild_ShouldCreateIt()
       {
           // Arrange and act
           var sut = new BinaryTree(1);
           var root = sut.GetRoot();

           // Act
           var leftChild = sut.AddLeftChild(2, root);

           // Assert
           Assert.AreEqual(2, sut.ListOfNodes.Count);
           Assert.AreEqual(2, leftChild.Descriptor);
           Assert.AreEqual(root, leftChild.Parent);
           Assert.AreEqual(leftChild, root.LeftChild);
       }

        [TestMethod]
        public void GetDeepFirstOrder_ShouldReturnExpectedList()
        {
            //Arrange
            var sut = new BinaryTree(1);
            var root = sut.GetRoot();
            var node2 = sut.AddLeftChild(2, root);
            var node3 = sut.AddRightChild(3, root);
            var node4 = sut.AddLeftChild(4, node2);
            var node5 = sut.AddRightChild(5, node3);
            var node6 = sut.AddLeftChild(6, node4);
            var node7 = sut.AddRightChild(7, node4);
            var node8 = sut.AddRightChild(8, node5);
            var node9 = sut.AddLeftChild(9, node8);
            var node10 = sut.AddRightChild(10, node8);

            var controlList = new List<int>
                                        {
                                            1, 2, 4, 6, 7, 3, 5, 8, 9, 10
                                        };
            
            //Act
            var resultsList = new List<int>();
            resultsList = sut.GetDeepFirstOrder(resultsList, root);

            //Assert
            CollectionAssert.AreEqual(controlList, resultsList);
        }
    }
}
