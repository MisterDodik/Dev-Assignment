using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private Transform objectParent;


    private void Start()
    {
        for(int i = 0; i<data.objects.Count; i++)
        {
            GameData.Object currentData = data.objects[i];
            GameObject spawned = Instantiate(currentData.objectPrefab, objectParent);

            spawned.transform.localPosition = currentData.objectPosition;

        }
    }
}
