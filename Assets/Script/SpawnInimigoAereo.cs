using UnityEngine;

public class SpawnInimigoAereo : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawPrefab;
  
    public int spawnTime;
    private float time;
    
    void Start()
    {
        time = 0;
        Spawner();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnTime)
        {
            time = 0;
            Spawner();
          
        }
    }
    void Spawner()
    {
        Instantiate(prefab, spawPrefab.position, Quaternion.identity);
    }
}
