using UnityEngine;

public class PatrulhaDano : MonoBehaviour
{
    
    public float speed;
    public float dano;

    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        transform.position += -transform.right * speed * Time.deltaTime;
    }
}
