using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour {
  
    public void MoveForward()
    {
        var transform1 = transform;
        transform1.position += transform1.forward * (20 * Time.deltaTime);
    }
}
