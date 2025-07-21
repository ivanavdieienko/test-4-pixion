using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = nameof(ScriptablesInstaller), menuName = "Installers/"+nameof(ScriptablesInstaller))]
public class ScriptablesInstaller : ScriptableObjectInstaller<ScriptablesInstaller>
{
    public override void InstallBindings()
    {
        Type[] bindableTypes =
        {
            typeof(Ability),
            typeof(Consequence),
            typeof(ParameterId),
            typeof(Stat),
            typeof(ITickable),
            typeof(IInitializable)
        };

        foreach (var scriptable in Resources.LoadAll<ScriptableObject>(""))
        {
            foreach (var type in bindableTypes)
            {
                if (type.IsAssignableFrom(scriptable.GetType()))
                {
                    Container.Bind(type).FromInstance(scriptable);
                }
            }
        }
    }
}