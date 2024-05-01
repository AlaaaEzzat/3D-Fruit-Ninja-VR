using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using Unity.XR.CoreUtils;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;
    public bool hasHit;
    public float thresholdVelocity;

    public Material crossSectionMaterial;
    public float cutForce = 2000;
    public float swingVelocity;

  
    void FixedUpdate()
    {

        if (Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer))
        {
            GameObject target = hit.transform.gameObject;
            swingVelocity = velocityEstimator.GetVelocityEstimate().magnitude;

            if (swingVelocity >= thresholdVelocity)
            {
                Slice(target);
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNomral = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNomral.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNomral);

        if (hull != null) 
        {

            if (target.gameObject.CompareTag("Bomb"))
            {
                target.transform.GetChild(0).gameObject.SetActive(true);
                target.transform.GetChild(0).gameObject.transform.parent = null;
                GameManager.instance.playerHealth -= 1;
                Destroy(target);
                AudioManager.instance.Play("Bomb");
            }
            else
            {
                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
                SetupSlicedComponent(upperHull);
                Destroy(upperHull, 2f);

                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
                SetupSlicedComponent(lowerHull);
                Destroy(lowerHull, 2f);
                Destroy(target);
                AudioManager.instance.Play("Slice");
                target.gameObject.GetComponent<IncreaseScore>().increaseScore();
            }

        }
    }

    public void SetupSlicedComponent(GameObject SlicedObject)
    {
        Rigidbody rb = SlicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = SlicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, SlicedObject.transform.position, 1);
    }

}
