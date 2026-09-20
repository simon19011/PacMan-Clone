using UnityEngine;

public class PacCatMovement : MonoBehaviour
{
    [Header("References")]
    public Grid grid;
    public Tweener tweener;
    private Animator animator;
    [Header("Movement")]
    public float movementSpeed = 2f;
    [Header("Path")]
    public Vector3Int[] path;
    private int currentTargetIndex = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = grid.GetCellCenterWorld(path[0]);

        MoveToNextPosition();
    }

    private void MoveToNextPosition()
    {
        int nextIndex = (currentTargetIndex + 1) % path.Length;
        Vector3 startPosition = grid.GetCellCenterWorld(path[currentTargetIndex]);
        Vector3 endPosition = grid.GetCellCenterWorld(path[nextIndex]);
        Vector3 direction = endPosition - startPosition;

        SetAnimationDirection(direction);

        float distance = Vector3.Distance(startPosition, endPosition);
        float duration = distance / movementSpeed;

        tweener.AddTween(
            transform,
            startPosition,
            endPosition,
            duration,
            () =>
            {
                currentTargetIndex = nextIndex;
                MoveToNextPosition();
            }
        );
    }

    private void SetAnimationDirection(Vector3 direction)
    {
        if (direction.y > 0)
        {
            animator.SetInteger("Direction", 0);
        }

        else if (direction.y < 0)
        {
            animator.SetInteger("Direction", 1);
        }

        else if (direction.x < 0)
        {
            animator.SetInteger("Direction", 2);
        }

        else if (direction.x > 0)
        {
            animator.SetInteger("Direction", 3);
        }
    }
}
