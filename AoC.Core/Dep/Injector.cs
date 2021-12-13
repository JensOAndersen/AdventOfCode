using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Core.Dep
{
    internal class Injector
    {
        private static Dictionary<Type, Type> _abstractTypes = new Dictionary<Type, Type>();
        private static HashSet<Type> _concreteTypes = new HashSet<Type>();
        public static void RegisterType<TAbstract, TConcrete>() where TConcrete : TAbstract
        {
            var type = typeof(TAbstract);

            if (_abstractTypes.ContainsKey(type))
                throw new TypeAlreadyRegisteredException();

            _abstractTypes.Add(type, typeof(TConcrete));
        }

        public static void RegisterType(Type type)
        {
            if (_concreteTypes.Contains(type))
                throw new TypeAlreadyRegisteredException();

            _concreteTypes.Add(type);
        }

        public static void RegisterType<TConcrete>()
        {
            var type = typeof(TConcrete);

            RegisterType(type);
        }

        public static async Task<TInstance> CreateInstanceOfAsync<TInstance>() => (TInstance)await CreateInstanceOfAsync(typeof(TInstance));

        private static async Task<object> CreateInstanceOfAsync(Type desiredInstance)
        {
            var constructors = Resolve(desiredInstance).GetConstructors();

            if (constructors.Length == 0) //user could have registered an interface
                throw new NoConstructorsFoundException($"{desiredInstance.Name} have no available constructors");

            var constructor = constructors[0]; //always use first constructor TODO: maybe use more than one constructor?, though what use would it be

            //create tasks for parameters
            var parameterTasks = constructor.GetParameters().Select(x => CreateInstanceOfAsync(x.ParameterType)).ToArray();
            var parameters = await Task.WhenAll(parameterTasks);

            var instance = constructor.Invoke(parameters);

            var interfaces = desiredInstance.GetInterfaces();

            //check if it should initialize
            if (interfaces.Contains(typeof(IShouldInit)))
                await ((IShouldInit)instance).Init();

            return instance;
        }

        private static Type Resolve(Type type)
        {
            if (_concreteTypes.Contains(type))
                return type;

            if (_abstractTypes.TryGetValue(type, out var foundType))
                return foundType;

            else throw new TypeNotRegisteredException($"{type.Name} is not registered in the injector");
        }

        /// <summary>
        /// Warning - this one is expensive
        /// </summary>
        /// <typeparam name="TypeObject"></typeparam>
        /// <param name="typeObject"></param>
        /// <returns></returns>
        public static TypeObject GetObject<TypeObject>(Type typeObject) where TypeObject : class
        {
            return (TypeObject)GetObject(typeObject);
        }

        public static TypeObject GetObject<TypeObject>() where TypeObject : class
        {
            return (TypeObject)GetObject(typeof(TypeObject));
        }


        private static object GetObject(Type typeObject)
        {
            var constructor = typeObject.GetConstructors()[0];
            var parameters = constructor.GetParameters().Select(x => GetObject(x.ParameterType));
            return constructor.Invoke(parameters.ToArray());
        }
    }
}
