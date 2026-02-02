using UnityEngine;

public class Laser : Landmine
{
    public Transform leftCaster;
    public Transform rightCaster;
    public Transform beam;
    public BoxCollider beamTrigger;

    public float maxCheckDistance = 50f;

    void Start()
    {
        HideMine();
    }
}
