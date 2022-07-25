using UnityEngine;


public enum ColliderType { sphere, box, capsule };
public enum CapsuleDirection { Xaxis, Yaxis, Zaxis }

public class CustomCollider : MonoBehaviour
{
    private Collider newCollider;

    [SerializeField] public bool handleOn = true;
    [SerializeField] public float handleSize = 1f;
    [SerializeField] public ColliderType currentCollider;
    [SerializeField] public Vector3 center;

    // sphere
    [SerializeField] public float radiusSphere = 1f;
    
    // box
    [SerializeField] public float length = 1f;
    [SerializeField] public float width = 1f;
    [SerializeField] public float depth = 1f;
   
    // capsule
    [SerializeField] public float radiusCapsule = 1f;
    [SerializeField] public float height = 1f;
    [SerializeField] public CapsuleDirection direction = CapsuleDirection.Xaxis;

    [SerializeField] public bool isTrigger = true;
 

    private void Start()
    {
        if (currentCollider == ColliderType.sphere)
        {
            newCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

            SphereCollider temp = newCollider.GetComponent<SphereCollider>();
            temp.center = center;
            temp.radius = radiusSphere;

            if (isTrigger)
            {
                temp.isTrigger = true;
            }
        }
        else if (currentCollider == ColliderType.box)
        {
            newCollider = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;

            BoxCollider temp = newCollider.GetComponent<BoxCollider>();
            temp.center = center;
            temp.size = new Vector3(width, depth, length);

            if (isTrigger)
            {
                temp.isTrigger = true;
            }
        }
        else if (currentCollider == ColliderType.capsule)
        {
            newCollider = gameObject.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;

            CapsuleCollider temp = GetComponent<CapsuleCollider>();
            temp.direction = (int)direction;
            temp.center = center;
            temp.radius = radiusCapsule;
            temp.height = height;

            if (isTrigger)
            { 
                temp.isTrigger = true;
            }
        }

        Destroy(this);
    }
}
