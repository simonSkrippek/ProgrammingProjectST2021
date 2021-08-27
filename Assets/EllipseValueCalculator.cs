using Shapes;
using TheStrive.Helper;
using UnityEngine;

public class EllipseValueCalculator : MonoBehaviour
{
    [System.Serializable]
    private struct Axis
    {
        public int pointIndex1;
        public Vector2 point1;
        public int pointIndex2;
        public Vector2 point2;
        public Vector2 vector;
        public float magnitude;
        public float angleToHorizontal;

        public Axis(int pointIndex1, Vector2 point1, int pointIndex2, Vector2 point2, Vector2 vector, float magnitude, float angleToHorizontal)
        {
            this.pointIndex1 = pointIndex1;
            this.point1 = point1;
            this.pointIndex2 = pointIndex2;
            this.point2 = point2;
            this.vector = vector;
            this.magnitude = magnitude;
            this.angleToHorizontal = angleToHorizontal;
        }
    }
    
    [SerializeField] private Transform[] ellipsePoints;
    [SerializeField] private Disc disc;
    
    [Header("EllipseInfo")]
    [ReadOnly] [SerializeField] private Vector2 ellipseCenter = new Vector2(1, 0);
    [ReadOnly] [SerializeField] private float xRadius;
    [ReadOnly] [SerializeField] private float yRadius;
    [ReadOnly] [SerializeField] private Axis horizontalAxisRay;
    [ReadOnly] [SerializeField] private Axis verticalAxisRay;
    
    private void OnValidate()
    {
        //make sure its a 4 point array
        if (ellipsePoints == null || ellipsePoints.Length != 4)
        {
            var new_ellipse_points = new Transform[4];
            ellipsePoints?.CopyTo(new_ellipse_points, 0);
            ellipsePoints = new_ellipse_points;
        }
    }

    private void OnDrawGizmos()
    {
        CalculateEllipseValues();
        DrawEllipseGUI();
    }
    private void CalculateEllipseValues()
    {
        //get the long axis: index point 1, index point 2, axis vector, axis magnitude, axis angle to horizontal
        Axis long_axis = default;
        for (var i = 0; i < ellipsePoints.Length; i++)
        {
            var point = ellipsePoints[i];
            for (var j = 0; j < ellipsePoints.Length; j++)
            {
                var other_point = ellipsePoints[j];

                var current_vector = (Vector2) other_point.position - (Vector2) point.position;
                var current_magnitude = current_vector.magnitude;
                
                if (!(long_axis.magnitude < current_magnitude)) continue;
                
                var angle = Vector2.Angle(Vector2.right, current_vector);
                if (angle > 90) angle = 180 - angle;
                long_axis = new Axis(i, ellipsePoints[i].position, j, ellipsePoints[j].position, current_vector, current_magnitude, angle);
            }
        }
        
        //get the short axis: index point 1, index point 2, axis vector, axis magnitude, axis angle to horizontal
        Axis short_axis = default;
        int first_point_index = -1;
        for (int i = 0; i < ellipsePoints.Length; i++)
        {
            //if first point has not been found
            if (first_point_index == -1)
            {
                if (i != long_axis.pointIndex1 && i != long_axis.pointIndex2) first_point_index = i;
            }
            else
            {
                if (i == long_axis.pointIndex1 || i == long_axis.pointIndex2 || i == first_point_index) continue;
                
                var vector = (Vector2) ellipsePoints[i].position - (Vector2) ellipsePoints[first_point_index].position;
                var angle = Vector2.Angle(Vector2.right, vector);
                if (angle > 90) angle = 180 - angle;
                short_axis = new Axis(first_point_index, ellipsePoints[first_point_index].position, i, ellipsePoints[i].position, vector, vector.magnitude, angle);
            }
        }
        
        // got both axes now

        ellipseCenter = long_axis.point1 + long_axis.vector * .5f;

        //which one is more horizontal?
        if (long_axis.angleToHorizontal > short_axis.angleToHorizontal)
        {
            horizontalAxisRay = short_axis;
            verticalAxisRay = long_axis;
        }
        else
        {
            horizontalAxisRay = long_axis;
            verticalAxisRay = short_axis;
        }

        xRadius = horizontalAxisRay.magnitude / 2f;
        yRadius = verticalAxisRay.magnitude / 2f;
    }
    private void DrawEllipseGUI()
    {
        disc.transform.position = ellipseCenter;
        disc.transform.localScale = new Vector3(xRadius, yRadius, 1f);
    }

    [ContextMenu("Print Values")]
    public void PrintValues()
    {
        CalculateEllipseValues();
        Debug.Log($"Ellipse Values | (xCoord, yCoord, xRadius, yRadius): {ellipseCenter.x}, {ellipseCenter.y}, {xRadius}, {yRadius}");
    }
}
