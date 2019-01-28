using Pixelplacement;
using UnityEngine;

public class MoveBlimpUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tween.LocalPosition(transform, transform.position + Vector3.up * 3, 1, 0);
    }
}
