using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    private void Start()
    {
        if (_isOpen)
        {
            OpenDoor();
        }
    }
    [ContextMenu("ToggleDoor")]
    public void ToggleDoor()
    {
        _isOpen = !_isOpen;


        if (_isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        transform.Rotate(transform.up * 90);

        
    }

    private void CloseDoor()
    {
        transform.Rotate(-transform.up*90);
    }


}
