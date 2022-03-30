using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : StageGimmick
{
    enum STATE { NORMAL, PRESSED };
    [Header("PressurePlate Script")]
    [SerializeField]STATE m_state;
    [Tooltip("???"), SerializeField] Vector2 sinkTo = new Vector2(0f, -0.1f);
    [Tooltip("??????????????"), SerializeField] bool needAllSameButtonCheck = true;
    [Tooltip("????"), SerializeField] LayerMask searchLayer = ~0;

    List<GameObject> m_matchItem; //????????????
    List<GameObject> m_matchButton; //????????
    Vector2 m_normalPos; //????
    bool ispressed; //?????????

    void Start()
    {
        GameManager.Instance.RegisterButton(this.gameObject);
        m_matchItem = new List<GameObject>();
        m_matchButton = new List<GameObject>();
        m_normalPos = transform.position;
    }

    void Update()
    {
        SwitchState();
    }

    void SwitchState()
    {
        switch (m_state)
        {
            case STATE.NORMAL:
                transform.position = Vector3.MoveTowards(transform.position, m_normalPos, Time.deltaTime);

                if (CheckAboveItem())
                {
                    _IsOpen = true;
                    GetComponent<SpriteRenderer>().color = Color.red;
                    m_state = STATE.PRESSED;

                    //??????????????????????????
                    if (CheckSameNumberButton())
                    {
                        OpenItem();
                    }
                }
                break;
            case STATE.PRESSED:
                transform.position = Vector3.MoveTowards(transform.position, m_normalPos + sinkTo, Time.deltaTime);
                //OpenItem();

                if (!CheckAboveItem())
                {
                    _IsOpen = false;
                    ispressed = false;
                    GetComponent<SpriteRenderer>().color = Color.cyan;
                    m_state = STATE.NORMAL;
                    CloseItem();
                }
                break;
        }
    }

    /// <summary>
    /// ??????????????
    /// </summary>
    /// <returns></returns>
    bool CheckAboveItem()
    {
        if (!ispressed)
        {
            return false;
        }

        Vector2 pos = transform.position;

        var colliders = Physics2D.OverlapBoxAll(pos + new Vector2(0f, transform.localScale.y / 2.0f + 0.2f), new Vector2(transform.localScale.x - 0.2f, 0.2f), 0f, searchLayer);
        // foreach(var item in colliders)
        // {
        //     if (item.CompareTag("Slime") || item.CompareTag("Player"))
        //         return true;
        // }
        if (colliders.Length > 0)
        {
            Slime_Outage(colliders);
            return true;
        }
        return false;
    }

    /// <summary>
    /// ?????????????????????
    /// </summary>
    /// <returns></returns>
    bool CheckSameNumberButton()
    {
        //????????true???
        if(!needAllSameButtonCheck)
        {
            return true;
        }

        if(GameManager.Instance.GetSameNumberButton(_Number, out m_matchButton))
        {
            foreach(var button in m_matchButton)
            {
                if(!button.GetComponent<StageGimmick>()._IsOpen)
                {
                    return false;
                }
            }
        }
        return true;
    }  

    void OpenItem()
    {
        if(GameManager.Instance.SearchItem(_Number, out m_matchItem))
        {
            foreach(var item in m_matchItem)
            {
                item.GetComponent<StageGimmick>().Open();  
            }
        }
    }

    void CloseItem()
    {
        if(GameManager.Instance.SearchItem(_Number, out m_matchItem))
        {
            foreach(var item in m_matchItem)
            {
                item.GetComponent<StageGimmick>().Close();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject)
        {
            ispressed = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, transform.localScale.y / 2.0f + 0.2f, 0), new Vector2(transform.localScale.x - 0.2f, 0.2f));
    }


    //�X���C���̑����~ ������܂ł̎��ԉ���
    private void Slime_Outage(Collider2D[] _colliders)
    {
        foreach (var item in _colliders)
        {
            if (item.gameObject.tag == "Slime")
            {
                SlimeController slimeController = item.gameObject.GetComponent<SlimeController>();
                if (!slimeController.core && slimeController._ifOperation)
                {
                    slimeController._ifOperation = false;
                    slimeController.deadTime += 10.0f;
                }
            }
        }
    }
}