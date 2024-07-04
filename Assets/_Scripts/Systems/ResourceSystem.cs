using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public class ResourceSystem : StaticInstance<ResourceSystem> {
    public List<ScriptableUnit> ExampleHeroes { get; private set; }

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        ExampleHeroes = Resources.LoadAll<ScriptableUnit>("ExampleHeroes").ToList();
    }

    public ScriptableUnit GetExampleHero(int id) => ExampleHeroes[id];
    public ScriptableUnit GetRandomHero() => ExampleHeroes[Random.Range(0, ExampleHeroes.Count)];
}   