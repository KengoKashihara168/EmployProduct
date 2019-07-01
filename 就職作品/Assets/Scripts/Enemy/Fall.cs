using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] private Animator anime;
    private Rigidbody2D rigid;
    private AlphaController alpha;
    private readonly float DefaultGravity = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        alpha = GetComponent<AlphaController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Falling()
    {
        alpha.ChangeAlpha(0.0f);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        alpha.ChangeAlpha(1.0f);
        transform.GetChild(0).gameObject.SetActive(false);
        rigid.gravityScale = DefaultGravity;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.tag.Equals("Player"))
        {
            StartCoroutine(Falling());
        }
    }
}
