using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public void Hit(Vector3 hitPoint, Vector3 direction);
}
