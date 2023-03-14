using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum HitTiming
{
    Critical,
    Hit,
    Miss
}

public class TimingController : MonoBehaviour
{
    [Header("Critical Zone")]
    public GameObject criticalZone;

    public float criticalZoneStart;
    public float criticalZoneEnd;

    [Header("Success Zone")]
    public GameObject successZone;
    public float successZoneStart;
    public float successZoneEnd;

    [Header("Cursor")]
    private GameObject cursor;
    public float currentPosition;
    private Vector3 cursorStartPos;
    private Vector3 cursorEndPos;
    public float cursorSpeed = 1;

    [Header("Parameters")]
    public bool isActivated = false;
    public bool oneShot = false;
    public bool reverse = false;

    [Header("Attack Data")]
    public statusEffects regularHitStatus;
    public int hitStatusStacks;
    public int damage;

    public statusEffects criticalHitStatus;
    public int criticalStatusStacks;
    public int CritDamage;
    

    private void Start()
    {
        //isActivated = false;
        cursorStartPos = new Vector3(5, -12, -11);
        cursorEndPos = new Vector3(495, -12, -11);

        cursor = transform.Find("Cursor").gameObject;

        DifficultyController dif = FindObjectOfType<DifficultyController>();
        if (dif.curMod != 0)
        {
            float successMid = successZoneEnd - successZoneStart;
            float criticalMid = criticalZoneEnd - criticalZoneStart;
            successMid = (successMid * dif.curMod) - (successZoneEnd - successZoneStart);
            criticalMid = (criticalMid * dif.curMod) - (criticalZoneEnd - criticalZoneStart);


            successZoneStart += successMid;
            successZoneEnd -= successMid;

            criticalZoneStart += criticalMid;
            criticalZoneEnd -= criticalMid;
        }

        successZone = transform.Find("SuccessZone").gameObject;
        criticalZone = transform.Find("CritZone").gameObject;

        CreateSquareMesh(successZoneStart * 500, successZoneEnd * 500 - successZoneStart * 500, successZoneEnd * 500, successZone, -5);
        CreateSquareMesh(criticalZoneStart * 500, criticalZoneEnd * 500 - criticalZoneStart * 500, criticalZoneEnd * 500, criticalZone, -10);
    }

    private void Update()
    {
        if (isActivated && !reverse)
        {
            currentPosition += Time.deltaTime * cursorSpeed;
        } else if (isActivated && reverse)
        {
            currentPosition -= Time.deltaTime * cursorSpeed;
        }
        if (currentPosition > 1 && !oneShot || currentPosition < 0 && !oneShot)
        {
            reverse = !reverse;
        }

        if (currentPosition > 1.1f)
        {
            isActivated = false;
        }

        cursor.transform.localPosition = Vector3.Lerp(cursorStartPos, cursorEndPos, currentPosition);
    }

    public void startTiming(bool oneChance)
    {
        oneShot = oneChance;
        currentPosition = 0;
        isActivated = true;
    }

    public HitTiming Clicked()
    {
        isActivated = false;
        if (currentPosition >= criticalZoneStart-0.01f && currentPosition <= criticalZoneEnd+0.01f)
        {
            return HitTiming.Critical;
        }
        if (currentPosition >= successZoneStart-0.01f && currentPosition <= successZoneEnd+0.01f)
        {
            return HitTiming.Hit;
        }
        return HitTiming.Miss;
    }


    public void CreateSquareMesh(float start, float mid, float end, GameObject zone, int z)
    {
        Debug.Log("Creating Mesh");

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(start, -50, z);
        vertices[1] = new Vector3(start, 50, z);
        vertices[2] = new Vector3(end, 50, z);
        vertices[3] = new Vector3(end, -50, z);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        zone.GetComponent<MeshFilter>().mesh = mesh;
    }
}
