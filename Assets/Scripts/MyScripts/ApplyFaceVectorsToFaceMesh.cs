using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


    public class ApplyFaceVectorsToFaceMesh : MonoBehaviour
    {
        // Start is called before the first frame update

        private ARFace detectedArFace;
        public ARFaceManager arFaceManager;
        public GameObject applyingFaceMesh;
        public Text text;
        private Vector2[] vectorToApplyUV;
        private Vector3[] vectorToApplyNormal;

        private void Start()
        {

        arFaceManager.facesChanged += ArFaceManager_facesChanged;

        vectorToApplyUV = new Vector2[applyingFaceMesh.GetComponent<MeshFilter>().mesh.uv.Length];
        vectorToApplyNormal = new Vector3[applyingFaceMesh.GetComponent<MeshFilter>().mesh.normals.Length];
        
        }

    private void ArFaceManager_facesChanged(ARFacesChangedEventArgs obj)
    {
             Debug.Log("face changed here");
        foreach(ARFace face in arFaceManager.trackables)
        {

            detectedArFace = arFaceManager.TryGetFace(face.trackableId);
        }
        detectedArFace.uvs.ToArray().CopyTo(vectorToApplyUV, 0);
        detectedArFace.normals.ToArray().CopyTo(vectorToApplyNormal, 0);
        applyingFaceMesh.GetComponent<MeshFilter>().mesh.uv = vectorToApplyUV;
        applyingFaceMesh.GetComponent<MeshFilter>().mesh.normals = vectorToApplyNormal;
        
            Debug.Log(detectedArFace.uvs.ToArray()[0] + "  Detected Face UVs");
            Debug.Log(detectedArFace.normals.ToArray()[0] + " Detected Face Normals");
            Debug.Log(applyingFaceMesh.GetComponent<MeshFilter>().mesh.uv[0] + " applyingFaceMesh Face UVs");
            Debug.Log(applyingFaceMesh.GetComponent<MeshFilter>().mesh.normals[0] + " applyingFaceMesh Face Normals");
            text.text = applyingFaceMesh.GetComponent<MeshFilter>().mesh.uv[0] + "\n" + detectedArFace.uvs.ToArray()[0];
            //applyingFaceMesh.GetComponent<MeshFilter>().mesh.uv = detectedArFace.uvs.ToArray();

        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }



}

