using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAnimator : StopMotionAnimator
{
    public GameObject[] cellPrefabs;
    GameObject[] _cells;
    GameObject[] cells
    {
        get 
        {
            if (_cells == null)
            {
                _cells = new GameObject[cellPrefabs.Length];
                for (int i = 0; i < cellPrefabs.Length; i++)
                {
                    _cells[i] = Instantiate(cellPrefabs[i]) as GameObject;
                    _cells[i].SetActive(false);
                    _cells[i].transform.SetParent(transform);
                    _cells[i].transform.localPosition = Vector3.zero;
                    _cells[i].transform.localRotation = Quaternion.identity;
                }
                animationLength = _cells.Length;
            }
            return _cells;
        }
    }

    void Update ()
    {
        Animate();
    }

    public override void RenderFrame (int _frame)
    {
        if (currentFrame >= 0)
        {
            cells[currentFrame].SetActive(false);
        }
        cells[_frame].SetActive(true);

        base.RenderFrame(_frame);
    }
}
