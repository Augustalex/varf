using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Paper : MonoBehaviour
{
    public GameObject approvalDecalTemplate;
    private bool _approved;

    public void Approve(Vector3 stampPosition, Quaternion stampRotation)
    {
        var decal = Instantiate(approvalDecalTemplate, transform);
        decal.transform.position = new Vector3(
            stampPosition.x,
            transform.position.y + .1f,
            stampPosition.z
        );
        decal.transform.rotation = stampRotation;

        if (!_approved)
        {
            Enact();
        }

        _approved = true;
    }

    public abstract void Enact();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            var stamp = other.GetComponent<Stamp>();
            if (stamp)
            {
                stamp.StampSound();
                Approve(stamp.transform.position, stamp.transform.rotation);
            }
        }
    }
}