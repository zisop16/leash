using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private Rope _rope;
    void OnCollisionEnter(Collision col) {
        _rope.Connected = true;
        Debug.Log("hi");
    }
}
