using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public GameObject player;
   public Vector3 offset;
   public float distance;

   public void Start()
   {
      
   }

   public void Update()
   {
       offset = new Vector3(0f, distance, player.transform.position.z - distance);
       transform.position = offset;
   }
}
