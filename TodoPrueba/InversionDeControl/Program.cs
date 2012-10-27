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
            container.Register(typeof(ICreditCard), typeof(MasterCard));
            var cart = container.Resolve<ShoppingCart>();
            // var cart = new ShoppingCart(new MasterCard());
            cart.Pay();

            Console.ReadKey();
        }
    }

//    internal class CustomResolver
//    {
//        Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();
//
//        public T Resolve<T>()
//        {
//            if (!_registeredTypes.ContainsKey(typeof(T)))
//            {
//                throw new Exception("Not registered");
//            }
//
//            var typeOfRequestedObject = _registeredTypes[typeof (T)];
//
//            var constructorsList = typeOfRequestedObject.GetConstructors();
//            var firstConstructor = constructorsList[0];
//            var parametersInfo = firstConstructor.GetParameters();
//
//            if (!parametersInfo.Any())
//            {
//                return Activator.CreateInstance<T>();
//            }
//            else
//            {
//
//                var paramObjects = new List<object>();
//                foreach (ParameterInfo paramInfo in parametersInfo)
//                {
//                    var paramType = paramInfo.ParameterType;
//                    paramObjects.Add(Resolve<paramType>());
//                }
//            }
//        }
//
//        public void Register<T, T1>()
//        {
//            _registeredTypes.Add(typeof (T), typeof (T1));
//        }
//    }

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

        public T Resolve<T>()
        {
            return (T)Resolve(typeof (T));
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