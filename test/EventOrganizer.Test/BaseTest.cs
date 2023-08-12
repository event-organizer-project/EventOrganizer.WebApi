using AutoFixture;
using EventOrganizer.Core.DTO;
using System.Reflection;

namespace EventOrganizer.Test
{
    public abstract class BaseTest<T> where T : class
    {
        protected T underTest;

        protected Fixture fixture = new CustomFixture();

        [Test, TestCaseSource(nameof(CreateConstructorsTestCases))]
        public void Constructor_With_Null_Parameter_Should_Throw_ArgumentNullException(ConstructorInfo ctor, object[] parameters, string nullParameterName)
        {
            var exception = Assert.Throws<TargetInvocationException>(() =>
                ctor.Invoke(parameters));

            Assert.IsAssignableFrom<ArgumentNullException>(exception.InnerException);
            Assert.That(GetExceptionMessage(nullParameterName), Is.EqualTo(exception.InnerException.Message));
        }

        [Test, TestCaseSource(nameof(CreateMethodsTestCases))]
        public void Method_With_Null_Parameter_Should_Throw_ArgumentNullException(MethodInfo method, object[] parameters, string nullParameterName, object instance)
        {
            var exception = Assert.Throws<TargetInvocationException>(() =>
                method.Invoke(instance, parameters));

            Assert.IsAssignableFrom<ArgumentNullException>(exception.InnerException);
            Assert.That(GetExceptionMessage(nullParameterName), Is.EqualTo(exception.InnerException.Message));
        }

        private static IEnumerable<TestCaseData> CreateConstructorsTestCases()
        {
            Type myType = typeof(T);

            var constructors = myType.GetConstructors().Where(ctor => ctor.IsPublic
                && ctor.GetParameters().Any(x => !x.ParameterType.IsValueType));

            foreach (var constructor in constructors)
            {
                var parameterInfos = constructor.GetParameters();

                var parameterValues = CreateParameterValues(parameterInfos);

                for (var i = 0; i < parameterValues.Count(); i++)
                {
                    yield return CreateTestCase(constructor, parameterValues, i, parameterInfos[i]);
                }
            }
        }

        private static IEnumerable<TestCaseData> CreateMethodsTestCases()
        {
            Type myType = typeof(T);

            var instance = InstanceFactory.Create(myType);

            var methods = myType.GetMethods().Where(method => method.IsPublic
                && !method.IsGenericMethod
                && !method.IsAbstract
                && method.DeclaringType == myType
                && method.GetParameters()
                    .Any(x => !x.ParameterType.IsValueType && x.ParameterType != typeof(VoidParameters)));

            foreach (var method in methods)
            {
                var parameterInfos = method.GetParameters();

                var parameterValues = CreateParameterValues(parameterInfos);

                for (var i = 0; i < parameterValues.Count(); i++)
                {
                    yield return CreateTestCase(method, parameterValues, i, parameterInfos[i], instance);
                }
            }
        }

        private static IEnumerable<object> CreateParameterValues(ParameterInfo[] parameters)
        {
            foreach (var parameter in parameters)
            {
                yield return InstanceFactory.Create(parameter.ParameterType);
            }
        }

        private static TestCaseData CreateTestCase(MethodBase methodBase, IEnumerable<object> parameters,
            int indexOfNullParameter, ParameterInfo nullParameterInfo, object instance = null)
        {
            var testCaseParameters = parameters.ToArray();
            testCaseParameters[indexOfNullParameter] = null;

            TestCaseData testCaseData;

            if (instance == null)
                testCaseData = new TestCaseData(methodBase, testCaseParameters, nullParameterInfo.Name);
            else
                testCaseData = new TestCaseData(methodBase, testCaseParameters, nullParameterInfo.Name, instance);

            if (methodBase.IsConstructor)
                testCaseData.SetName($"Constructor_With_Null_{nullParameterInfo.ParameterType.Name}_Parameter_Should_Throw_ArgumentNullException");
            else
                testCaseData.SetName($"{methodBase.Name}_With_Null_{nullParameterInfo.ParameterType.Name}_Parameter_Should_Throw_ArgumentNullException");
            return testCaseData;
        }

        private string GetExceptionMessage(string nullParameterName)
            => $"Value cannot be null. (Parameter '{nullParameterName}')";
    }
}
