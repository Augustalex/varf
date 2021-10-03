using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject approvalDecalTemplate;
    private bool _approved;

    public void Approve(Transform stampTransform)
    {
        var decal = Instantiate(approvalDecalTemplate, transform);
        decal.transform.position = new Vector3(
            stampTransform.position.x,
            transform.position.y + .1f,
            stampTransform.position.z
        );
        decal.transform.rotation = stampTransform.rotation;

        _approved = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            var stamp = other.GetComponent<Stamp>();
            if (stamp)
            {
                Approve(stamp.transform);
            }
        }
    }
}