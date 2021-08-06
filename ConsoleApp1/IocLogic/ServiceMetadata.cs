using System;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1.IocLogic
{
    public record ServiceMetadata
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public Type TFrom { get; }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public Type TTo { get; }

        public string? ServiceName { get; }
        public object[] ParameterValues { get; }
        public ServiceLifetime ServiceLifetime { get; }

        public ServiceMetadata(Type tFrom,
            Type tTo,
            ServiceLifetime serviceLifetime,
            string? serviceName,
            object[] parameterValues)
        {
            TFrom = tFrom ?? throw new ArgumentNullException(nameof(tFrom));
            TTo = tTo ?? throw new ArgumentNullException(nameof(tTo));
            ServiceName = serviceName;
            ParameterValues = parameterValues ??
                throw new ArgumentNullException(nameof(parameterValues));
            ServiceLifetime = serviceLifetime;
        }
    }
}
