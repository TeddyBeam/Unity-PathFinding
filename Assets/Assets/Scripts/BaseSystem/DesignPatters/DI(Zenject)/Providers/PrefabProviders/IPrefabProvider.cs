#if !NOT_UNITY3D

namespace BaseSystems.DesignPatterns.DependencyInjection
{
    public interface IPrefabProvider
    {
        UnityEngine.Object GetPrefab();
    }
}

#endif

