using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; 

    private void Start()
    {
        SwitchCamera(0); 
    }

    public void SwitchCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
