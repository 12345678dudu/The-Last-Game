using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Corda : MonoBehaviour
{
    public KeyCode teclaSoltar = KeyCode.Space;
    public string ropeTag = "Rope";

    private HingeJoint2D hinge;
    private Rigidbody2D rb;

    public float cooldownGrudar = 0.3f; // tempo após soltar para poder grudar novamente
    private bool podeGrudar = true;
    private float timerCooldown = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Atualiza o cooldown
        if (!podeGrudar)
        {
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0)
                podeGrudar = true;
        }

        // Soltar corda
        if (Input.GetKeyDown(teclaSoltar))
        {
            Soltar();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Se não pode grudar ainda → ignora
        if (!podeGrudar)
            return;

        // Se colidiu com um segmento da corda
        if (col.collider.CompareTag(ropeTag))
        {
            Rigidbody2D segmentRB = col.collider.attachedRigidbody;
            if (segmentRB != null)
            {
                Grudar(segmentRB);
            }
        }
    }

    void Grudar(Rigidbody2D segment)
    {
        // Se já está grudado → ignora
        if (hinge != null)
            return;

        hinge = gameObject.AddComponent<HingeJoint2D>();
        hinge.connectedBody = segment;

        hinge.autoConfigureConnectedAnchor = false;

        // ÂNCORA DO JOGADOR
        hinge.anchor = new Vector2(0, 0.3f);

        // ÂNCORA DO SEGMENTO
        hinge.connectedAnchor = Vector2.zero;

        hinge.useLimits = false;
        hinge.enableCollision = false;

        hinge.enabled = true;
    }

    void Soltar()
    {
        if (hinge != null)
        {
            Destroy(hinge);
            hinge = null;
        }

        // Iniciar cooldown para impedir re-grudar imediato
        podeGrudar = false;
        timerCooldown = cooldownGrudar;
    }
}
