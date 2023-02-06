using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshPointFinderScript : MonoBehaviour
{
    public List<NavigationPointScript> NavigationPoints = new List<NavigationPointScript>();


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<NavigationPointScript>())
        {
            addPoint(collision.gameObject.GetComponent<NavigationPointScript>());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<NavigationPointScript>())
        {
            removePoint(collision.gameObject.GetComponent<NavigationPointScript>());
        }
    }

    private void addPoint(NavigationPointScript navPoint)
    {
        NavigationPoints.Add(navPoint);
    }

    private void removePoint(NavigationPointScript navPoint)
    {
        NavigationPoints.Remove(navPoint);
    }
}