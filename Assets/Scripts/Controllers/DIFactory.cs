namespace Test
{
    using UnityDev.DI;
    using UnityEngine;

    /// <summary>
    /// Фабрика инжекции зависимостей
    /// </summary>
    public class DIFactory : IFactory
    {
        public T InstantiateObject<T>(T obj) where T : Object => DIContanier.Instance.InstantiateObject(obj);
    }
}
