using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDoNotDeadAreaRadiusChangeItem : MonoBehaviour
{
    [SerializeField] float chengeSize;  //�傫���̕ω���

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�X���C����������
        if (collision.gameObject.tag == "Slime")
        {
            collision.gameObject.GetComponent<SlimeController>()._slimeBuf._doNotDeadAreaRadius += chengeSize;
            Destroy(this.gameObject);
        }
    }
}