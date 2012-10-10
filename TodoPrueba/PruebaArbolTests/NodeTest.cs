using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PruebaArbol;

namespace PruebaArbolTests
{
    [TestClass]
    public class NodeTest
    {
        /// <summary>
        /// A unit test for IsRoot
        /// </summary>
        [TestMethod]
        public void IsRoot_WhenTreeIsJustCreated_RootIsOnlyElementInList()
        {
            //Arrange
            var tree = new BinaryTree(1);
            var root = tree.ListOfNodes[0];

            //Act
            var isRoot = root.IsRoot();

            //Assert
            Assert.AreEqual(true, isRoot);
        }
    }
}
