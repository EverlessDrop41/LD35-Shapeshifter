using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public bool Lerp;
    public float LerpTime = 0.1f;
    public bool X, Y, Z;

    public float XOffset, YOffset, ZOffset;

    void FixedUpdate()
    {
        if (Target)
        {
            if (Lerp)
            {
                Vector3 targetpos = Vector3.Lerp(transform.position, Target.position, LerpTime);
                transform.position = new Vector3(
                    (X ? targetpos.x + XOffset : transform.position.x),
                    (Y ? targetpos.y + YOffset : transform.position.y),
                    (Z ? targetpos.z + ZOffset : transform.position.z));
            }
            else
            {
                transform.position = new Vector3(
                    (X ? Target.position.x + XOffset : transform.position.x),
                    (Y ? Target.position.y + YOffset : transform.position.y),
                    (Z ? Target.position.z + ZOffset : transform.position.z));
            }
        }
    }
}