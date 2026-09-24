using UnityEngine;
using DG.Tweening;


public class GridCell : MonoBehaviour
{
    private Vector2 cellSize;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        transform.localScale = new Vector2(0.1f, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Circle")
        {
            
            spriteRenderer.enabled = true;
            Vector2 size = new Vector2(GridBuilder.Instance.CellSize, GridBuilder.Instance.CellSize);
            transform.DOScale(size, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Circle")
        {
            transform.DOScale(new Vector2(0.1f,0.1f), 0.5f).OnComplete(() => spriteRenderer.enabled = false);
        }
    }





}
