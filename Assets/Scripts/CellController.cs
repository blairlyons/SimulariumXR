using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scale
{
    Cell,
    Molecular
}

public enum Organelle
{
    Mitochondria,
    Microtubules,
    Actin
}

public class CellController : MonoBehaviour
{
    public Organelle organelle;
    public Scale currentScale = Scale.Cell;
    public bool play;

    SimulationAnimator _simulation;
    SimulationAnimator simulation
    {
        get
        {
            if (_simulation == null)
            {
                _simulation = (Instantiate(Resources.Load("Simulations/" + organelle.ToString() + "Simulation")) as GameObject).GetComponent<SimulationAnimator>();
                _simulation.transform.SetParent(transform);
                _simulation.transform.localPosition = 0.5f * _simulation.boxSize * 1.5f * Vector3.up;
                _simulation.transform.localRotation = Quaternion.identity;
                _simulation.transform.localScale = 1.5f * Vector3.one;
                _simulation.RenderFrame(0);
            }
            return _simulation;
        }
    }

    CellAnimator _cell;
    CellAnimator cell
    {
        get
        {
            if (_cell == null)
            {
                _cell = (Instantiate(Resources.Load("Cells/" + organelle.ToString() + "Cell")) as GameObject).GetComponent<CellAnimator>();
                _cell.transform.SetParent(transform);
                _cell.transform.localPosition = Vector3.zero;
                _cell.transform.localRotation = Quaternion.identity;
                _cell.RenderFrame(0);
            }
            return _cell;
        }
    }

    void Start ()
    {
        if (currentScale == Scale.Cell)
        {
            cell.gameObject.SetActive(true);
        }
        else
        {
            simulation.gameObject.SetActive(true);
        }

        UpdatePlaying();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (play) { Pause(); }
            else { Play(); }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchScale(currentScale == Scale.Cell ? Scale.Molecular : Scale.Cell);
        }
    }

    public void Play ()
    {
        play = true;
        UpdatePlaying();
    }

    public void Pause ()
    {
        play = false;
        UpdatePlaying();
    }

    void UpdatePlaying ()
    {
        if (currentScale == Scale.Molecular)
        {
            if (play) { simulation.Play(); }
            else { simulation.Pause(); }
        }
        else
        {
            if (play) { cell.Play(); } 
            else { cell.Pause(); }
        }
    }

    public void SwitchScale (Scale _scale)
    {
        cell.gameObject.SetActive(_scale == Scale.Cell);
        simulation.gameObject.SetActive(_scale == Scale.Molecular);
        currentScale = _scale;
        UpdatePlaying();
    }
}
