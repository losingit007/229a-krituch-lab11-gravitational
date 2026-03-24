using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> otherObjectsList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectsList == null)
        {
            otherObjectsList = new List<Gravity>();
        }

        otherObjectsList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach(Gravity obj in otherObjectsList) 
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRigidbody = other.rb;

        Vector3 direction = rb.position - otherRigidbody.position;

        float distance = direction.magnitude;

        if (distance == 0f) 
        {
            return;
        } 

        float forceMagnitude = G *(rb.mass * otherRigidbody.mass)/Mathf.Pow(distance, 2);

        Vector2 gravityForce = forceMagnitude * direction.normalized;

        otherRigidbody.AddForce(gravityForce);
    }
}
