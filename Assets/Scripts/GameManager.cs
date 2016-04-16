using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ShapeInfo[] Shapes;

    public Follow CameraFollow;

    private GameObject _currentShape;
    private int _currentShapeIndex = 0;

    public void Start()
    {
        foreach (ShapeInfo shape in Shapes)
        {
            if (shape.Shape == Shapes[0].Shape)
            {
                _currentShape = Instantiate(shape.Shape, shape.SpawnPosition.position, Quaternion.identity) as GameObject;
                _currentShapeIndex = 0;
            }
            else
            {
                Instantiate(shape.Shape, shape.SpawnPosition.position, Quaternion.identity);
            }
        }
    }

    public void Update()
    {
        CameraFollow.target = _currentShape.transform;
    }
}

[System.Serializable]
public struct ShapeInfo
{
    public GameObject Shape;
    public Transform SpawnPosition;
    public Image GuiImage;
}