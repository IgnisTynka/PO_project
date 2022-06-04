using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Projekt;

class Board : Drawable {

    public Board(Player player1, Player player2, Vector2f position) {
        _position = position;
        _size = new Vector2i(11, 11);
        _runes = new Rune[_size.X, _size.Y];
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                _runes[i, j] = Rune.Random();
                _runes[i, j].Position = new Vector2f(i * _runeSize.X, j * _runeSize.Y);
            }
        }
        while(RemoveGroups());
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                _runes[i, j].Scale = 1f;
            }
        }
        Player1 = player1;
        Player2 = player2;
        _firstPlayerTurn = true;

        _move = new Vector2i(-1, -1);
        _state = 0;
        _animationTime = 0f;
    }

    private void SwapRunes(Vector2i pos1, Vector2i pos2) {
        _runes[pos1.X, pos1.Y].Position = new Vector2f(pos2.X * _runeSize.X, pos2.Y * _runeSize.Y);
        _runes[pos2.X, pos2.Y].Position = new Vector2f(pos1.X * _runeSize.X, pos1.Y * _runeSize.Y);
        Rune tmp = _runes[pos1.X, pos1.Y];
        _runes[pos1.X, pos1.Y] = _runes[pos2.X, pos2.Y];
        _runes[pos2.X, pos2.Y] = tmp;
    }

    private void ChangeState(int state) {
        _state = state;
        _animationTime = 0f;
        if (state == 0) {
            _move = new Vector2i(-1, -1);
            _selected = new Vector2i(-1, -1);
            _firstPlayerTurn = !_firstPlayerTurn; 
        }
    }

    private void ScaleNewRunes(float scale) {
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                if (_runes[i, j].Scale < 1f) {
                    _runes[i, j].Scale = scale;
                }
            }
        }
    }

    public void Update(float deltaTime) {
        if (_move.X != -1) {
            _animationTime += deltaTime;
            if (_state == 0) {
                ChangeState(1);
            }
            else if (_state == 1) {
                if (_animationTime < .4f) {
                    Vector2f vector = (Vector2f)(_move - _selected);
                    vector.X *= _runeSize.X;
                    vector.Y *= _runeSize.Y;
                    _runes[_selected.X, _selected.Y].Position += vector * deltaTime / .4f;
                    _runes[_move.X, _move.Y].Position -= vector * deltaTime / .4f;
                    return;
                }
                else {
                    SwapRunes(_selected, _move);
                    ScaleNewRunes(1f);
                    if (RemoveGroups()) {
                        ChangeState(3);
                    }
                    else {
                        ChangeState(2);
                    }
                }
            }
            else if (_state == 2) {
                if (_animationTime < .4f) {
                    Vector2f vector = (Vector2f)(_move - _selected);
                    vector.X *= _runeSize.X;
                    vector.Y *= _runeSize.Y;
                    _runes[_selected.X, _selected.Y].Position += vector * deltaTime / .4f;
                    _runes[_move.X, _move.Y].Position -= vector * deltaTime / .4f;
                    return;
                }
                else {
                    SwapRunes(_selected, _move);
                    ChangeState(0);
                }
            }
            else if (_state == 3) {
                if (_animationTime < .4f) {
                    ScaleNewRunes(_animationTime / .4f);
                }
                else {
                    ScaleNewRunes(1f);
                    if (RemoveGroups()) {
                        ChangeState(3);
                    }
                    else {
                        ChangeState(0);
                    }
                }
            }
        }
    }

    public void Draw(RenderTarget target, RenderStates states) {
        states.Transform.Translate(_position);
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                target.Draw(_runes[i, j], new RenderStates(states));
            }
        }
    }

    public bool OnClick(Object? sender, MouseButtonEventArgs args) {
        if (args.Button != Mouse.Button.Left) {
            return false;
        }
        if (_move.X != -1) {
            return false;
        }
        Vector2f pos = ((RenderWindow)sender!).MapPixelToCoords(new Vector2i(args.X, args.Y)) + _runeSize / 2f;
        Vector2i clicked = new Vector2i(
            (int)((pos.X - _position.X) > 0 ? (pos.X - _position.X) / _runeSize.X : -1),
            (int)((pos.Y - _position.Y) > 0 ? (pos.Y - _position.Y) / _runeSize.Y : -1)
        );
        if (clicked.X >= 0 && clicked.X < _size.X && clicked.Y >= 0 && clicked.Y < _size.Y) {
            if (_selected.X == -1) {
                _selected = clicked;
                _runes[_selected.X, _selected.Y].Selected = true;
            }
            else if (_selected == clicked) {
                _runes[_selected.X, _selected.Y].Selected = false;
                _selected = new Vector2i(-1, -1);
            }
            else if (_selected.X == clicked.X && _selected.Y == clicked.Y + 1) {
                _runes[_selected.X, _selected.Y].Selected = false;
                _move = clicked;
            }
            else if (_selected.X == clicked.X && _selected.Y == clicked.Y - 1) {
                _runes[_selected.X, _selected.Y].Selected = false;
                _move = clicked;
            }
            else if (_selected.X == clicked.X + 1 && _selected.Y == clicked.Y) {
                _runes[_selected.X, _selected.Y].Selected = false;
                _move = clicked;
            }
            else if (_selected.X == clicked.X - 1 && _selected.Y == clicked.Y) {
                _runes[_selected.X, _selected.Y].Selected = false;
                _move = clicked;
            }
            else {
                _runes[_selected.X, _selected.Y].Selected = false;
                _selected = clicked;
                _runes[_selected.X, _selected.Y].Selected = true;
            }
            return true;
        }
        return false;
    }

    public bool RemoveGroups() {
        bool[,] toRemove = new bool[_size.X, _size.Y];
        bool found = false;

        Player? active = _firstPlayerTurn ? Player1 : Player2;
        Player? opponent = _firstPlayerTurn ? Player2 : Player1;

        // collumns
        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j + 2 < _size.Y; j++) {
                if (_runes[i, j].Type == _runes[i, j + 1].Type && _runes[i, j].Type == _runes[i, j + 2].Type) {
                    found = true;
                    toRemove[i, j] = true;
                    toRemove[i, j + 1] = true;
                    toRemove[i, j + 2] = true;
                    if (opponent != null && _runes[i, j].Type == RuneType.Dark) {
                        opponent.Damage(3);
                    }
                    else if (active != null) {
                        active.Collect(_runes[i, j].Type, 3);
                    }
                }
            }
        }

        // rows
        for (int i = 0; i + 2 < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                if (_runes[i, j].Type == _runes[i + 1, j].Type && _runes[i, j].Type == _runes[i + 2, j].Type) {
                    found = true;
                    toRemove[i, j] = true;
                    toRemove[i + 1, j] = true;
                    toRemove[i + 2, j] = true;
                    if (opponent != null && _runes[i, j].Type == RuneType.Dark) {
                        opponent.Damage(3);
                    }
                    else if (active != null) {
                        active.Collect(_runes[i, j].Type, 3);
                    }
                }
            }
        }

        for (int i = 0; i < _size.X; i++) {
            for (int j = 0; j < _size.Y; j++) {
                if (toRemove[i, j]) {
                    _runes[i, j] = Rune.Random();
                    _runes[i, j].Position = new Vector2f(i * _runeSize.X, j * _runeSize.Y);
                    _runes[i, j].Scale = 0f;
                }
            }
        }

        return found;
    }

    public Player? Player1;
    public Player? Player2;

    private Vector2i _size;
    private Vector2f _position;
    private Vector2f _runeSize = new Vector2f(48f, 48f);
    private Rune[,] _runes;
    private bool _firstPlayerTurn;

    private Vector2i _selected;
    private Vector2i _move;
    private int _state;
    private float _animationTime;
}