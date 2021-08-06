using System.Threading.Tasks;

namespace ConsoleApp1.IocLogic
{
    public interface IIoc
    {
        public Task Add<TFrom, TTo>(ServiceLifetime lifetime = ServiceLifetime.瞬态,
            string? serviceName = default,
            params object[] parameterValues) where TTo : TFrom;

        public static IIoc Create()
        {
            return new Ioc();
        }

        public void Get<T>();
        IIoc CreateScope();
    }
}
