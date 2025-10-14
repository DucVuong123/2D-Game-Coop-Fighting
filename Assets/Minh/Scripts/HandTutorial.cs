using UnityEngine;
using DG.Tweening;
public class HandTutorial : MonoBehaviour
{
  public Transform Possup, PossDown;
    Vector2 up, down;
    public float speed;
    private void Awake()
    {
        up = Possup.position;
        down = PossDown.position;
        MoveUp();
    }


    void MoveUp()
    {
        transform.DOMove(up, speed).OnComplete(MoveDown);
    }

    void MoveDown()
    {
        transform.DOMove(down, speed).OnComplete(MoveUp);
    }
}
