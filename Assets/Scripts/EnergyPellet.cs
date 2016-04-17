using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class EnergyPellet : MonoBehaviour
{
    public float Value = 3f;

    private AudioSource _player;

    public void Awake()
    {
        _player = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        ShapeController collController = collision.gameObject.GetComponent<ShapeController>();

        if (collController)
        {
            _player.Play();
            collController.AddEnergy(Value);

            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, _player.clip.length);
        }
    }
}
