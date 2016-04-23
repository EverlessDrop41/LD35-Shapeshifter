using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image ActiveCharacterImage;

    public List<ShapeInfo> Shapes;
    private List<GameObject> _insatiatedShapes = new List<GameObject>();

    public Follow CameraFollow;

    private GameObject _currentShape;
    private int _currentShapeIndex = 0;

    public void Start()
    {
        int index = 0;
        foreach (ShapeInfo shape in Shapes)
        {
            _insatiatedShapes.Add(Instantiate(shape.Shape, shape.SpawnPosition.position, Quaternion.identity) as GameObject);

            if (index == 0)
            {
                _currentShapeIndex = 0;
                _currentShape = _insatiatedShapes[index];
                _currentShape.GetComponent<ShapeController>().beingControlled = true;
                UpdateActiveCharacterImage(Shapes[index]);
            }

            index++;
        }

    }

    public void Update()
    {
        CameraFollow.Target = _currentShape.transform;

        if (Input.GetButtonDown("Next Character"))
        {
            NextCharacter();
        }
        else if (Input.GetButtonDown("Previous Character"))
        {
            PreviousCharacter();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void NextCharacter()
    {
        _currentShape.GetComponent<ShapeController>().beingControlled = false;
        _currentShapeIndex++;
        if (_currentShapeIndex > _insatiatedShapes.Count - 1)
        {
            _currentShapeIndex = 0;
            _currentShape = _insatiatedShapes[0];
        }
        else
        {
            _currentShape = _insatiatedShapes[_currentShapeIndex];
        }
        UpdateActiveCharacterImage(Shapes[_currentShapeIndex]);
        _currentShape.GetComponent<ShapeController>().beingControlled = true;
    }

    void PreviousCharacter()
    {
        _currentShape.GetComponent<ShapeController>().beingControlled = false;
        _currentShapeIndex--;
        if (_currentShapeIndex < 0)
        {
            int lastIndex = _insatiatedShapes.Count - 1;
            _currentShapeIndex = lastIndex;
            _currentShape = _insatiatedShapes[lastIndex];
        }
        else
        {
            _currentShape = _insatiatedShapes[_currentShapeIndex];
        }
        UpdateActiveCharacterImage(Shapes[_currentShapeIndex]);
        _currentShape.GetComponent<ShapeController>().beingControlled = true;
    }

    void UpdateActiveCharacterImage(ShapeInfo shapeInfo)
    {
        ActiveCharacterImage.sprite = shapeInfo.GuiImage;
        ActiveCharacterImage.color = shapeInfo.Color;
    }
}

[System.Serializable]
public struct ShapeInfo
{
    public GameObject Shape;
    public Transform SpawnPosition;
    public Sprite GuiImage;
    public Color Color;
}