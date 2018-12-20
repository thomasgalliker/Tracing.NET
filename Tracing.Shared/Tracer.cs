using System;
using System.Threading;

namespace Tracing
{
    /// <summary>
    ///     Provides tracing functionality and encapsulates the concrete tracing implementation.
    /// </summary>
    public static class Tracer
    {
        private static ITracerFactory defaultTracerFactory;
        private static ITracerFactory tracerFactory;

        static Tracer()
        {
            Initialize();
        }

        private static ITracerFactory CreateDefaultFactory()
        {
            return new EmptyTracerFactory();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        internal static void Initialize()
        {
            SetDefaultFactory(CreateDefaultFactory());

            var defaultTracerFactoryConfiguration = DefaultTracerFactoryConfiguration.GetDefaultTracerFactory();
            SetFactory(defaultTracerFactoryConfiguration);
        }

        /// <summary>
        ///     Gets the configured <see cref="ITracerFactory" />.
        /// </summary>
        public static ITracerFactory Factory
        {
            get
            {
                // In case the tracerFactory becomes null,
                // we want to return the default tracer factory.
                return tracerFactory ?? defaultTracerFactory;
            }
        }

        /// <summary>
        ///     Sets the default tracer factory <see cref="ITracerFactory" /> to use within the <see cref="Tracer" />.
        /// </summary>
        /// <param name="factory">The <see cref="ITracerFactory" /> to use within the <see cref="Tracer" />.</param>
        internal static void SetDefaultFactory(ITracerFactory factory)
        {
            Guard.ArgumentNotNull(nameof(factory), factory);

            Interlocked.Exchange(ref defaultTracerFactory, factory);
        }

        /// <summary>
        ///     Sets the concrete <see cref="ITracerFactory" /> to use within the <see cref="Tracer" />.
        /// </summary>
        /// <param name="factory">The <see cref="ITracerFactory" /> to use within the <see cref="Tracer" />.</param>
        public static void SetFactory(ITracerFactory factory)
        {
            // TODO: Why not protecting SetFactory method?
            //Guard.ArgumentNotNull(nameof(factory), factory);

            Interlocked.Exchange(ref tracerFactory, factory);
        }

        /// <summary>
        ///     Creates a <see cref="ITracer" /> whose name is set to the specified <paramref name="tracerName" />.
        /// </summary>
        /// <param name="tracerName">The name of the tracer.</param>
        /// <returns>A new <see cref="ITracer" /> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="tracerName" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="tracerName" /> parameter is an empty string.</exception>
        public static ITracer Create(string tracerName)
        {
            Guard.ArgumentNotNullOrEmpty(nameof(tracerName), tracerName);

            return Factory.Create(tracerName);
        }

        /// <summary>
        ///     Creates a <see cref="ITracer" /> whose name is set to the <see cref="Type.FullName" /> of the specified
        ///     <paramref name="tracerType" />.
        /// </summary>
        /// <param name="tracerType">The type whose fully qualified type name is used as the name of the tracer.</param>
        /// <returns>A new <see cref="ITracer" /> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="tracerType" /> parameter is <c>null</c>.</exception>
        public static ITracer Create(Type tracerType)
        {
            Guard.ArgumentNotNull(nameof(tracerType), tracerType);

            return Factory.Create(tracerType);
        }

        /// <summary>
        ///     Creates a <see cref="ITracer" /> whose name is set to the <see cref="Type.FullName" /> of the specified
        ///     <typeparamref name="T" />.
        /// </summary>
        /// <example>
        ///     Call
        ///     <code>ITracer tracer = Tracer.Create(this); in order to create a new ITracer instance named by the type of 'this'.</code>
        /// </example>
        /// <param name="tracerTarget">This parameter can be ignored. We're only interested in the type of the object.</param>
        /// <typeparam name="T">The type whose fully qualified type name is used as the name of the tracer.</typeparam>
        /// <returns>A new <see cref="ITracer" /> instance.</returns>
        public static ITracer Create<T>([ValidatedNotNull] T tracerTarget = default(T))
        {
            return Factory.Create<T>();
        }
    }
}