using UnityEngine;

public class Patrulha : MonoBehaviour
{
    
    public GameObject[] prefab;
    public Transform[] spawPrefab;
    public float spawnMax;
    public float spawnMin;
    private float spawnTime;
    private float time;
    private int indexPrefab;
    void Start()
    {
        time = 0;
        Spawner();
    }

    void Update()
    {
        time += Time.deltaTime;
         spawnTime = Random.Range(spawnMax, spawnMin);
        if (time >= spawnTime)
        {
            time = 0;
            Spawner();
          
        }
    }
    void Spawner()
    {
        if (HudInimigo.vidaPerdidas <= 100)
        {
            indexPrefab = Random.Range(0, spawPrefab.Length);
            Instantiate(prefab[indexPrefab], spawPrefab[indexPrefab].position, Quaternion.identity);
        }
    }
}
