using UnityEngine;

public class PhotoButtonManager : MonoBehaviour
{
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}
