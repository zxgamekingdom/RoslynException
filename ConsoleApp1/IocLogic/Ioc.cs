using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.IocLogic
{
    class Ioc : IIoc
    {
        private readonly AsyncLock _lock = new();

        private readonly Dictionary<Type, Dictionary<string, List<ServiceMetadata>>>
            _serviceMetadatas = new();

        public Task Add<TFrom, TTo>(ServiceLifetime lifetime = ServiceLifetime.瞬态,
            string? serviceName = default,
            params object[] parameterValues) where TTo : TFrom
        {
            return Do(token =>
            {
                var serviceMetadata = new ServiceMetadata(typeof(TFrom),
                    typeof(TTo),
                    lifetime,
                    serviceName,
                    parameterValues);
                string key = serviceName ?? string.Empty;
                switch (_serviceMetadatas.TryGetValue(typeof(TFrom), out var value))
                {
                    case true:
                        switch (value!.TryGetValue(key, out var list))
                        {
                            case true:
                                list!.Add(serviceMetadata);
                                break;
                            case false:
                                value.Add(key,
                                    new List<ServiceMetadata> {serviceMetadata});
                                break;
                        }
                        break;
                    case false:
                        _serviceMetadatas.Add(typeof(TFrom),
                            new Dictionary<string, List<ServiceMetadata>>
                            {
                                {key, new List<ServiceMetadata> {serviceMetadata}}
                            });
                        _serviceMetadatas.Add(typeof(TFrom),
                            new Dictionary<string, List<ServiceMetadata>>
                            {
                                {key, new List<ServiceMetadata> {serviceMetadata}}
                            });
                        break;
                }
            });
        }

        private Task Do(Action<CancellationToken> func)
        {
            return _lock.Do(func);
        }

        public void Get<T>()
        {
            throw new NotImplementedException();
        }

        public IIoc CreateScope()
        {
            throw new NotImplementedException();
        }
    }
}
