using System;
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
	void Start ()
    {
        ShowFigures();
    }

    // Update is called once per frame
    void Update()
    {
        if (dad.Action())
        {
            string from = GetSquare(dad.pickPosition);
            Debug.Log("from :" + from);
            string to = GetSquare(dad.dropPosition);
            Debug.Log("to :" + to);
            string figure = chess.GetFigureAt(GetSquareX(dad.pickPosition), GetSquareY(dad.pickPosition)).ToString();
            Debug.Log("figure :" + figure);
            string move = figure + from + to;
            Debug.Log("Move :" + move);
            chess = chess.Move(move);
            ShowFigures();
        }
    }

    string GetSquare(Vector2 position) //e2
    {
        int x = Convert.ToInt32(position.x / 2.0);
        int y = Convert.ToInt32(position.y / 2.0);
        return ((char)('a' + x)).ToString() + (y + 1).ToString(); 
    }

    int GetSquareX(Vector2 position) //e2
    {
        return Convert.ToInt32(position.x / 2.0);
    }

    int GetSquareY(Vector2 position) //e2
    {
        return Convert.ToInt32(position.y / 2.0);
    }

    private void ShowFigures()
    {
        int nr = 0;
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                string figure = chess.GetFigureAt(x, y).ToString();
                if (figure == ".") continue;
                PlaceFigure("Box" + nr, figure, x, y);
                nr++;
            }
        for (; nr < 32; nr++)
            {
                PlaceFigure("Box" + nr, "q", 9, 9);
            }      
    }

    private void PlaceFigure(string box, string figure, int x, int y)
    {
        // find box by name for place figure
        GameObject goBox = GameObject.Find(box);

        // find figure by name which we want place into box 
        GameObject goFigure = GameObject.Find(figure);

        GameObject goSquare = GameObject.Find("" + y + x);

        var spriteFigure = goFigure.GetComponent<SpriteRenderer>();

        var spriteBox = goBox.GetComponent<SpriteRenderer>();

        spriteBox.sprite = spriteFigure.sprite;

        goBox.transform.position = goSquare.transform.position;
    }
}

public class DragAndDrop
{
    enum State
    {
        none,
        drag
    }

    public Vector2 pickPosition { get; private set; }
    public Vector2 dropPosition { get; private set; }

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
        pickPosition = clickedItem.position;
        item = clickedItem.gameObject;
        state = State.drag;
        offset = pickPosition - clickPosition;
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
        dropPosition = item.transform.position;
        state = State.none;
        item = null;
    }
}
