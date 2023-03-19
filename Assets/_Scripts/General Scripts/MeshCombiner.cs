using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private List<MeshFilter> sourceMeshFilters;
    [SerializeField] private List<GameObject> parentMeshFilters;
    [SerializeField] private MeshFilter targetMeshFilter;

    [ContextMenu("Combine Meshes")]
    private void CombineMeshes()
    {
        sourceMeshFilters = new List<MeshFilter>();
        for (var i = 0; i < parentMeshFilters.Count; i++)
        {
            foreach(MeshFilter mf in parentMeshFilters[i].GetComponentsInChildren<MeshFilter>())
            {
                sourceMeshFilters.Add(mf);
            }
        }

        var combine = new CombineInstance[sourceMeshFilters.Count];

        for (var i = 0; i < sourceMeshFilters.Count; i++)
        {
            combine[i].mesh = sourceMeshFilters[i].sharedMesh;
            combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
        }

        

#if UNITY_EDITOR
        Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Models/WoodenFloor.fbx");
        mesh.UploadMeshData(true);
        mesh.CombineMeshes(combine);
        mesh.RecalculateBounds();
        EditorUtility.SetDirty(mesh);
        AssetDatabase.SaveAssets();

#endif
    }
}
