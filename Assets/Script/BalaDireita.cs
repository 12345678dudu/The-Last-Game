using UnityEngine;

public class BalaDireita : MonoBehaviour
{
  
    public float speed;
    public float dano;
    public AudioSource bala;
    void Start()
    {
        Destroy(gameObject, 5f);
        bala=GetComponent<AudioSource>();
    bala.Play();
    }
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }
}


