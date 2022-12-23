namespace Test
{
    using UnityEngine;

    /// <summary>
    /// Интерфейс фабрики
    /// </summary>
    public interface IFactory
    {
        T InstantiateObject<T>(T obj) where T : Object;
    }
}
