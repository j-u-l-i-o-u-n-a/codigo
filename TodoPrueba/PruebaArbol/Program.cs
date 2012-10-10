using System;

namespace PruebaArbol
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Program starting...");
            var tree = new BinaryTree(1);
            Console.WriteLine("Program stopped.");
            Console.ReadKey();
        }
    }
}