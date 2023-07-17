using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floating : MonoBehaviour
{
   public Rigidbody rb;
   public float depthBeforeSubmerged;
   public float displacementAmount;
   public int floaters;
   public float waterDrag;
   public float waterAngularDrag;
   public WaterSurface water;
   private WaterSearchParameters search;
   private WaterSearchResult searchResult;

   private void FixedUpdate()
   {
      rb.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

      search.startPosition = transform.position;

      water.FindWaterSurfaceHeight(search, out searchResult);

      if (transform.position.y < searchResult.height)
      {
         float displacementMultiplier = Mathf.Clamp01(searchResult.height -
                                                      transform.position.y / depthBeforeSubmerged) * displacementAmount;
         rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y)
                                               * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
         rb.AddForce(- rb.velocity * (displacementMultiplier * waterDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
         rb.AddTorque(- rb.angularVelocity * (displacementMultiplier * waterAngularDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
      }
   }
}
