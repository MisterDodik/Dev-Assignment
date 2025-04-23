using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float deltaX, deltaY;

    private float cellSize = 1.28f;
    private Vector2 worldGridOrigin = new Vector2(-3.84f, -5.12f);

    private Vector2 gridOrigin;
    private Vector4 selfBounds;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        var pivot = spriteRenderer.sprite.pivot;
        var size = spriteRenderer.sprite.bounds.size * 100;

        selfBounds = new Vector4( pivot.x,
                                  size.y - pivot.y,
                                  pivot.y,
                                  size.x - pivot.x) / (cellSize * 100f);


        gridOrigin = worldGridOrigin + new Vector2( Mathf.Floor(selfBounds.x) * cellSize,
                                                    Mathf.Floor(selfBounds.z) * cellSize );
    }


    public void OnBeginDrag(PointerEventData eventData)
	{
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(eventData.position);
        touchPos.z = 10;

        rb.bodyType = RigidbodyType2D.Dynamic;

        deltaX = touchPos.x - transform.position.x;
        deltaY = touchPos.y - transform.position.y;
    }

	public void OnDrag(PointerEventData eventData)
	{
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(eventData.position);
		touchPos.z = 10;
        rb.MovePosition(new Vector2(touchPos.x - deltaX,
                                    touchPos.y - deltaY));
    }

	public void OnEndDrag(PointerEventData eventData)
	{
        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.position = SnapToGrid(transform.position);
    }



    Vector2 SnapToGrid(Vector2 currentPosition)
    {
        Vector2 offset = (currentPosition - gridOrigin) / cellSize;
        Vector2 snappedOffset = new Vector2(Mathf.Round(offset.x), Mathf.Round(offset.y));
        return gridOrigin + snappedOffset * cellSize;
    }
}

