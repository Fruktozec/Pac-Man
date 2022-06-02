using UnityEngine;

public class PowerPellet : Pellet
{
    [SerializeField] private float duration = 8f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
