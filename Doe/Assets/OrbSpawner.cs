using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject Orb;
    public void GeraOrb(Vector3 position)
    {
        Instantiate(Orb, position, Quaternion.identity);
    }
}
