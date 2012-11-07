using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace InversionConCastleWindsor
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Shopper>());
            container.Register(Component.For<ICreditCard>().ImplementedBy<MasterCard>());

            var shopper = container.Resolve<Shopper>();
            shopper.ExecutePayment();

            var otherShopper = container.Resolve<Shopper>();
            shopper.ExecutePayment();
            Console.ReadKey();
        }
    }

    public class Shopper
    {
        private readonly ICreditCard _creditCard;

        public Shopper(ICreditCard creditCard)
        {
            _creditCard = creditCard;
        }

        public void ExecutePayment()
        {
            _creditCard.Charge();
            Console.WriteLine(string.Format("This card has been used {0} time(s).", _creditCard.ChargeCount));
        }
    }

    public interface ICreditCard
    {
        void Charge();
        int ChargeCount { get; }
    }

    class Visa : ICreditCard
    {
        private int _chargeCount = -1;
        public void Charge()
        {
            Console.WriteLine("Pasando la Visa!");
        }

        public int ChargeCount
        {
            get { return _chargeCount; }
        }
    }

    class MasterCard : ICreditCard
    {
        private int _chargeCount;

        public MasterCard()
        {
            _chargeCount = 0;
        }

        public void Charge()
        {
            _chargeCount++;
            Console.WriteLine("Pasando la master card");
        }

        public int ChargeCount
        {
            get { return _chargeCount; }
        }
    }
}
