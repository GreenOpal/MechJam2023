using UnityEngine;

public class GlobalBehaviour : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
