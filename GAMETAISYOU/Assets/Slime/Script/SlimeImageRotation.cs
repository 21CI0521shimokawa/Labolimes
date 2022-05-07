using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeImageRotation : MonoBehaviour
{
    [SerializeField] SlimeController slimeController;

    float angleZ;

    // Start is called before the first frame update
    void Start()
    {
        angleZ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(slimeController._rayHitFoot && slimeController.s_state != State.AIR)
        {
            Debug.Log(Quaternion.FromToRotation(new Vector3(0, 1, 0), slimeController._rayHitFoot.normal).eulerAngles);

            angleZ = Quaternion.FromToRotation(new Vector3(0, 1, 0), slimeController._rayHitFoot.normal).eulerAngles.z;
            angleZ *= slimeController._direction == SlimeController._Direction.Right ? 1 : -1;  //スライムの向きに合わせて回転

            transform.Rotate(new Vector3(0, 0, angleZ - transform.localEulerAngles.z));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -transform.localEulerAngles.z));
        }
    }
}
