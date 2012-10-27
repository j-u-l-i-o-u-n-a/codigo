using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InversionDeControl
{
    class Program
    {
        private static void Main(string[] args)
        {
            var container = new IoC();
            container.Register(typeof(ShoppingCart), typeof(ShoppingCart));
            container.Register(typeof(ICreditCard), typeof(Visa));
            var cart = container.Resolve(typeof(ShoppingCart)) as ShoppingCart;
            // var cart = new ShoppingCart(new MasterCard());
            cart.Pay();

            Console.ReadKey();
        }
    }

    internal class IoC
    {
        private readonly Dictionary<Type, Type> _registry;

        public IoC()
        {
            _registry = new Dictionary<Type, Type>();
        }

        public void Register(Type type, Type mapToType)
        {
            _registry.Add(type, mapToType);
        }

        public object Resolve(Type type)
        {
            if (!_registry.ContainsKey(type))
            {
                throw new ArgumentOutOfRangeException("type", "Not registered on the IoC");
            }

            var requiredType = _registry[type];

            var constructors = requiredType.GetConstructors();
            var firstConstructor = constructors[0];
            var parameterInfos = firstConstructor.GetParameters();

            if (!parameterInfos.Any())
            {
                return Activator.CreateInstance(requiredType);
            }
            else
            {
                var paramObjects = new List<object>();
                foreach (ParameterInfo paramInfo in parameterInfos)
                {
                    paramObjects.Add(Resolve(paramInfo.ParameterType));
                }
                return Activator.CreateInstance(requiredType, paramObjects.ToArray());
            }
        }
    }

    internal class ShoppingCart
    {
        private readonly ICreditCard _creditCard;

        public ShoppingCart(ICreditCard creditCard)
        {
            _creditCard = creditCard;
        }

        public void Pay()
        {
            _creditCard.Pay();
        }
    }

    internal interface ICreditCard
    {
        void Pay();
    }

    public class MasterCard : ICreditCard
    {
        #region ICreditCard Members

        public void Pay()
        {
            Console.Write("Paying with MasterCard");
        }

        #endregion
    }

    public class Visa : ICreditCard
    {
        #region ICreditCard Members

        public void Pay()
        {
            Console.Write("Paying with Visa");
        }

        #endregion
    }
}