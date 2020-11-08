using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region Param
    [Header ("Field Of View")]
    [SerializeField]private float viewRadius;

    [Range (0,360)]
    [SerializeField] private float viewAngle;

    [Range(0, 1)]
    [SerializeField] private float meshResolution;

    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    [Space]
    [Header ("Time Between two Raycast")]
    [SerializeField] private float delay;
    [Min(2.5f)]
    [SerializeField] private float shiftDelay;



    [Space]
    [Header("Target Infos")]
    [SerializeField] private List<Transform> visibleTargets = new List<Transform>();
    [SerializeField] private List<GameObject> visibleGameObject = new List<GameObject>();

    [Space]
    [Header("Target")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask shootingObstacleMask;

    #endregion

    #region UPDATE | FIXEDUPDATE | LATEUPDATE
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";

        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay", delay);
        //StartCoroutine("DestroyPlayerWithDelay", delay + shiftDelay);
    }

    private void Update()
    {
       
    }

    

    private void LateUpdate()
    {
        DrawFieldOfView();
    }
    #endregion

    #region FindTarget
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        visibleGameObject.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            GameObject targetGO = targetsInViewRadius[i].gameObject;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask) || Physics.Raycast(transform.position, dirToTarget, dstToTarget, shootingObstacleMask))
                {
                    visibleTargets.Add(target);
                    visibleGameObject.Add(targetGO);
                }
            }
        }
    }

    void DestroyPlayer()
    {
        //Debug.Log("Destroy");
        if( visibleGameObject.Count > 0)
        {
            foreach (GameObject player in visibleGameObject)
            {
                Destroy(player);
            }
        }
        else
        {
            //Debug.Log("there is no player");
        }
    }
    #region Draw FOV
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    #endregion

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    #endregion

    #region ViewCastInfo
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    #endregion

    #region IENUMERATOR
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    IEnumerator DestroyPlayerWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            DestroyPlayer();
        }
    }
    #endregion

    #region GETTER && SETTER
    public float ViewAngle { get => viewAngle; set => viewAngle = value; }
    public LayerMask TargetMask { get => targetMask; set => targetMask = value; }
    public List<Transform> VisibleTargets { get => visibleTargets; set => visibleTargets = value; }
    public float ViewRadius { get => viewRadius; set => viewRadius = value; }
    public List<GameObject> VisibleGameobject { get => visibleGameObject; set => visibleGameObject = value; }
    #endregion


}
