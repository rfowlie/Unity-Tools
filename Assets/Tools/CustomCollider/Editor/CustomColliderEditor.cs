using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomCollider))]
public class CustomColliderEditor : Editor
{
    CustomCollider cc;

    Vector3 directionH;
    Vector3 directionW;

    private void OnEnable()
    {
        cc = (CustomCollider)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        cc.handleOn = EditorGUILayout.Toggle("Handle On", cc.handleOn);
        cc.isTrigger = EditorGUILayout.Toggle("Is Trigger", cc.isTrigger);
        cc.currentCollider = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", cc.currentCollider);
        cc.center = EditorGUILayout.Vector3Field("Center", cc.center);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();

        if(cc.handleOn)
        {            
            if (cc.currentCollider == ColliderType.sphere)
            {
                SphereInspector();
            }
            else if (cc.currentCollider == ColliderType.box)
            {
                BoxInspector();
            }
            else if (cc.currentCollider == ColliderType.capsule)
            {
                CapsuleInspector();
            }
        }
    }

    private void SphereInspector()
    {
        EditorGUILayout.BeginVertical();        
        cc.radiusSphere = EditorGUILayout.FloatField("Radius Size", cc.radiusSphere);
        EditorGUILayout.EndVertical();
    }

    private void BoxInspector()
    {
        EditorGUILayout.BeginVertical();        
        cc.length = EditorGUILayout.FloatField("Length", cc.length);
        cc.width = EditorGUILayout.FloatField("Width", cc.width);
        cc.depth = EditorGUILayout.FloatField("Depth", cc.depth);
        EditorGUILayout.EndVertical();
    }

    private void CapsuleInspector()
    {
        EditorGUILayout.BeginVertical();        
        cc.radiusCapsule = EditorGUILayout.FloatField("Radius", cc.radiusCapsule);
        cc.height = EditorGUILayout.FloatField("Height", cc.height);
        cc.direction = (CapsuleDirection)EditorGUILayout.EnumPopup("Direction", cc.direction);
        EditorGUILayout.EndVertical();
    }


    private void OnSceneGUI()
    {
        if(cc.handleOn)
        {
            if (cc.currentCollider == ColliderType.sphere)
            {
                DrawSphere();
            }
            else if (cc.currentCollider == ColliderType.box)
            {
                DrawWireCube();
            }
            else if (cc.currentCollider == ColliderType.capsule)
            {
                DrawCapsule();
            }
        }        
    }

    private void DrawSphere()
    {
        cc.radiusSphere = Handles.RadiusHandle(cc.transform.rotation, cc.transform.position + cc.center, cc.radiusSphere);
    }

    private void DrawWireCube()
    {
        Handles.color = Color.white;
        Handles.DrawWireCube(cc.transform.position + cc.center, new Vector3(cc.width, cc.depth, cc.length));

        Vector3 forward = cc.transform.forward;
        Vector3 right = cc.transform.right;
        Vector3 up = cc.transform.up;

        float handleSize = (cc.transform.position - Camera.current.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f))).magnitude + 5f;
        cc.length = Handles.ScaleValueHandle(cc.length, cc.transform.position + forward * cc.length / 2, Quaternion.LookRotation(forward), handleSize, Handles.ArrowHandleCap, 1f);
        cc.width = Handles.ScaleValueHandle(cc.width, cc.transform.position + right * cc.width / 2, Quaternion.LookRotation(right), handleSize, Handles.ArrowHandleCap, 1f);
        cc.depth = Handles.ScaleValueHandle(cc.depth, cc.transform.position + up * cc.depth / 2, Quaternion.LookRotation(up), handleSize, Handles.ArrowHandleCap, 1f);
    }    

    private void DrawCapsule()
    {
        if(cc.direction == CapsuleDirection.Xaxis)
        {
            directionH = -cc.transform.right;
            directionW = -cc.transform.forward;
        }
        else if(cc.direction == CapsuleDirection.Yaxis)
        {
            directionH = cc.transform.up;
            directionW = -cc.transform.forward;
        }
        else if(cc.direction == CapsuleDirection.Zaxis)
        {
            directionH = -cc.transform.forward;
            directionW = cc.transform.up;
        }

        float handleSize = (cc.transform.position - Camera.current.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f))).magnitude;

        cc.height = Mathf.Max(1f, Handles.ScaleValueHandle(cc.height, cc.transform.position + cc.height/2 * directionH, Quaternion.LookRotation(directionH), handleSize, Handles.ArrowHandleCap, 0.1f));        
        cc.radiusCapsule = Mathf.Max(1f, Handles.ScaleValueHandle(cc.radiusCapsule, cc.transform.position + cc.radiusCapsule * directionW,  Quaternion.LookRotation(directionW), handleSize, Handles.ArrowHandleCap, 0.1f));
        

        Handles.RadiusHandle(cc.transform.rotation, cc.center + cc.transform.position + (directionH * (cc.height / 2 - cc.radiusCapsule)), cc.radiusCapsule);
        Handles.RadiusHandle(cc.transform.rotation, cc.center + cc.transform.position + (-directionH * (cc.height / 2 - cc.radiusCapsule)), cc.radiusCapsule);

        //Get remaining Height, if greater than 0 need more spheres
        float remainingHeight = cc.height - (cc.radiusCapsule * 2);

        if(remainingHeight > 0)
        {
            //get number of extra spheres
            int extraSpheres = (int)Mathf.Ceil((cc.height - cc.radiusCapsule * 2) / (cc.radiusCapsule * 2));

            //determine spacing
            float offset = remainingHeight / extraSpheres;

            //get starting point
            Vector3 startingPoint = cc. center + cc.transform.position - ((cc.height / 2) * (directionH)) + (cc.radiusCapsule * directionH);
                       
            //set spheres by spacing
            for (int i = 1; i <= extraSpheres; i++)
            {
                Handles.RadiusHandle(cc.transform.rotation, startingPoint + (i * offset * directionH) , cc.radiusCapsule);
            }
        }
    }

    //draws rectangle that can resize and rotate
    //private void DrawRectangle()
    //{
    //    //get forward and right vectors
    //    Vector3 forward = cc.transform.forward;
    //    Vector3 right = cc.transform.right;

    //    float handleSize = (cc.transform.position - Camera.current.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f))).magnitude;

    //    //make size handles for length and width
    //    cc.length = Handles.ScaleValueHandle(cc.length, cc.transform.position + forward * cc.length / 2,
    //                                         Quaternion.LookRotation(cc.transform.forward), handleSize,
    //                                         Handles.ArrowHandleCap, 1f);
    //    cc.length = Handles.ScaleValueHandle(cc.length, cc.transform.position + -forward * cc.length / 2,
    //                                         Quaternion.LookRotation(-cc.transform.forward), handleSize,
    //                                         Handles.ArrowHandleCap, 1f);

    //    cc.width = Handles.ScaleValueHandle(cc.width, cc.transform.position + right * cc.width / 2,
    //                                         Quaternion.LookRotation(cc.transform.right), handleSize,
    //                                         Handles.ArrowHandleCap, 1f);
    //    cc.width = Handles.ScaleValueHandle(cc.width, cc.transform.position + -right * cc.width / 2,
    //                                         Quaternion.LookRotation(-cc.transform.right), handleSize,
    //                                         Handles.ArrowHandleCap, 1f);

    //    //calculate vector points to make rectangle
    //    Vector3 topRight = cc.transform.position + (forward * cc.length / 2) + (right * cc.width / 2);
    //    Vector3 bottomRight = cc.transform.position - (forward * cc.length / 2) + (right * cc.width / 2);
    //    Vector3 bottomLeft = cc.transform.position - (forward * cc.length / 2) - (right * cc.width / 2);
    //    Vector3 topLeft = cc.transform.position + (forward * cc.length / 2) - (right * cc.width / 2);

    //    //put them into vector array
    //    Vector3[] verts = new Vector3[]
    //    {
    //        topRight, bottomRight, bottomLeft, topLeft
    //    };

    //    //draw rectangle
    //    Handles.DrawSolidRectangleWithOutline(verts, Color.clear, Color.red);
    //}
}
