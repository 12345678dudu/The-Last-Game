using UnityEngine;

public class InimigoAereo : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawPrefab;
    public float spawnTime;
    private float times;
    private int indexPrefab;
    public float velocidade;
    void Start()
    {
         times=0;
        transform.Rotate(0, 0, 90);
          Destroy(gameObject,5f);
    }

    void Update()
    {
        Movimentacao();
        times += Time.deltaTime;
        if (times >= spawnTime )
        {
            times = 0;
            Spawner();
        }
    }
    void Spawner()
    {
        Instantiate(prefab, spawPrefab.position, Quaternion.identity);
    }
    void Movimentacao()
    {
        transform.position += Vector3.left * velocidade * Time.deltaTime;
    }

}
