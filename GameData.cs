using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public List<Object> objects;

    [System.Serializable]
    public class Object
    {
        public Vector2 objectPosition;
        public GameObject objectPrefab;
    }
}
