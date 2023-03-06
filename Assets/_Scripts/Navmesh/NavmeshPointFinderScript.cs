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

    public void findPoint(string ID)
    {
        foreach(NavigationPointScript nav in NavigationPoints)
        {
            if (nav.navPointId == ID)
            {
                nav.MovePlayerHere();
                return;
            }
        }
        Debug.LogError("Could not find navPoint with ID: " + ID);
    }

    private void addPoint(NavigationPointScript navPoint)
    {
        NavigationPoints.Add(navPoint);
        Debug.Log("Added point with ID: " + navPoint.navPointId);
    }

    private void removePoint(NavigationPointScript navPoint)
    {
        NavigationPoints.Remove(navPoint);
        Debug.Log("Removed point with ID: " + navPoint.navPointId);
    }
}
