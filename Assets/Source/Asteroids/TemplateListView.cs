using AlfredoMB.ServiceLocator;
using System.Collections.Generic;
using UnityEngine;

public class TemplateListView : MonoBehaviour
{
    public GameObject Template;
    private List<GameObject> _list = new List<GameObject>();

    public void UpdateValue(int value)
    {
        if (_list.Count == value)
        {
            return;
        }

        var spawner = ServiceLocator.Get<IGameObjectSpawner>();
        if (_list.Count < value)
        {
            for(int i=_list.Count; i<value; i++)
            {
                var spawned = spawner.Spawn(Template, Vector3.zero, Quaternion.identity, transform);
                _list.Add(spawned);
            }
        }
        else
        {
            for (int i = _list.Count - 1; value < _list.Count; i--)
            {
                var spawned = _list[i];
                _list.RemoveAt(i);
                spawner.Despawn(spawned);
            }
        }
        
    }
}