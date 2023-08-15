using Moq;
using System.Reflection;

namespace EventOrganizer.Test
{
    public class InstanceFactory
    {
        public static object Create(Type type)
        {
            if (type == null) 
                throw new ArgumentNullException(nameof(type));

            if (type.IsValueType)
                return Activator.CreateInstance(type);

            if (type == typeof(string))
                return Guid.NewGuid().ToString();

            if (type.IsArray)
                return Array.CreateInstance(type.GetElementType(), 0);

            if (type.IsInterface)
            {
                var mock = Activator.CreateInstance(typeof(Mock<>).MakeGenericType(type));
                var mockValue = mock.GetType()
                    .GetProperties()
                    .First(x => x.Name == "Object")
                    .GetValue(mock);

                return mockValue;
            }

            if (HasDefaultConstructor(type))
                return Activator.CreateInstance(type);

            return CreateComplicatedInstance(type);
        }

        private static bool HasDefaultConstructor(Type type)
        {
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            
            if (constructors.Length == 0)
                return true;

            return constructors.Any(ctor => ctor.GetParameters().Length == 0);
        }

        private static object CreateComplicatedInstance(Type type)
        {
            var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).First();

            var parameters = constructor.GetParameters().Select(x => Create(x.ParameterType)).ToArray();

            return constructor.Invoke(parameters);
        }
    }
}
