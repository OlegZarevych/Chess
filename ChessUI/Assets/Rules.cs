using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Chess;

public class Rules : MonoBehaviour {

    DragAndDrop dad;
    Chess.Chess chess;

    public Rules()
    {
        dad = new DragAndDrop();
        chess = new Chess.Chess();
    }
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        dad.Action();
    }

    void ShowFigures()
    {
        int nr = 0;
        for(int y = 0; y < 8; y++)
            for(int x = 0; x < 8; x++)
            {
                string figure = chess.GetFigureAt(x, y).ToString();
                if (figure == ".") continue;
                PlaceFigure("box" + nr, figure, x, y);
                nr++;
            }
        for(; nr < 32; nr++)
        {
            PlaceFigure("box" + nr, "q",9 ,9);
        }
    }

    void PlaceFigure(string box, string figure, int x, int y)
    {

    }
}

public class DragAndDrop
{
    enum State
    {
        none,
        drag
    }

    State state;
    GameObject item;
    Vector2 offset;

    public DragAndDrop()
    {
        state = State.none;
        item = null;
    }

    public bool Action()
    {
        switch (state)
        {
            case State.none:
                if (IsMouseButtonPressed())
                    PickUp();
                break;
            case State.drag:
                if (IsMouseButtonPressed())
                    Drag();
                else
                {
                    Drop();
                    return true;
                }
                break;

        }
        return false;
    }

    bool IsMouseButtonPressed()
    {
        return Input.GetMouseButton(0);
    }

    void PickUp()
    {
        Vector2 clickPosition = GetClickPosition();
        Transform clickedItem = GetItemAt(clickPosition);

        if (clickedItem == null) return;

        item = clickedItem.gameObject;
        state = State.drag;
        offset = (Vector2)clickedItem.position - clickPosition;
    }

    Vector2 GetClickPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Transform GetItemAt(Vector2 position)
    {
        RaycastHit2D[] figures = Physics2D.RaycastAll(position, position, 0.5f);
        if (figures.Length == 0)
            return null;
        return figures[0].transform;
    }

    void Drag()
    {
        item.transform.position = GetClickPosition() + offset;
    }

    void Drop()
    {
        state = State.none;
        item = null;
    }
}
