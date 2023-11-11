using System.Numerics;
using Actors;
using Extensions;
using Log;
using Raylib_cs;

namespace Helpers.quadtree;

public class Quadtree : IDisposable
{
    private Rectangle _boundaries;
    public int Capacity { get; private set; }
    private bool _divided;
    private List<Actor> _totalActors = new();
    private readonly List<Actor> _actorsInQuadtree = new();
    private List<Actor> _actorsInViewPort = new();
    private Rectangle _currentViewPort;
    public event Action<List<Actor>> OnQuadTreeUpdate; 
    private Quadtree _northWest;
    private Quadtree _northEast;
    private Quadtree _southWest;
    private Quadtree _southEast;

    public Quadtree(Rectangle boundaries, int capacity)
    {
        _boundaries = boundaries;
        Capacity = capacity;
        _divided = false;
    }

    private void Clear()
    {
        _actorsInQuadtree.Clear();
        _northWest?.Clear();
        _northEast?.Clear();
        _southWest?.Clear();
        _southEast?.Clear();
        _divided = false;
    }

    public void AddActors(List<Actor> actors)
    {
        _totalActors.AddRange(actors);
    }

    public bool InsertInQuadtree(Actor actor)
    {
        if (!_boundaries.ContainsPoint(actor.Position))
            return false;

        if (!_divided)
        {
            if (_actorsInQuadtree.Count < Capacity)
            {
                _actorsInQuadtree.Add(actor);
                return true;
            }

            Subdivide();
        }

        return _northEast.InsertInQuadtree(actor) ||
               _northWest.InsertInQuadtree(actor) ||
               _southEast.InsertInQuadtree(actor) ||
               _southWest.InsertInQuadtree(actor);
    }

    public void Update(Rectangle boundaries, float dt)
    {
        Clear();
        _boundaries = boundaries;
        foreach (var actor in _totalActors)
        {
            InsertInQuadtree(actor);
        }

        _currentViewPort = boundaries;

        _actorsInViewPort = GetActorsWithinBoundaries(_currentViewPort); 
        //_actorsInViewPort.Sort((actor1, actor2) => actor1.Position.Y.CompareTo(actor2.Position.Y));
        OnQuadTreeUpdate?.Invoke(_actorsInViewPort);
    }

    public bool RemoveActor(Actor actor)
    {
        if (_actorsInQuadtree.Remove(actor))
        {
            if (_actorsInQuadtree.Count <= Capacity)
            {
                _divided = false;
            }
            return true;
        }
        
        if (_divided)
        {
            if (_northWest.RemoveActor(actor) || 
                _northEast.RemoveActor(actor) || 
                _southWest.RemoveActor(actor) || 
                _southEast.RemoveActor(actor))
            {
                return true;
            }
            
            if (!_northWest._divided &&
                !_northEast._divided &&
                !_southWest._divided &&
                !_southEast._divided &&
                _northWest._actorsInQuadtree.Count == 0 &&
                _northEast._actorsInQuadtree.Count == 0 &&
                _southWest._actorsInQuadtree.Count == 0 &&
                _southEast._actorsInQuadtree.Count == 0)
            {
               
                _divided = false;
            }
        }

        return false;
    }
    
    public void GetActorsWithinActorRange(Actor actor, List<Actor> result)
    {
        var position = actor.Position;
        if (_boundaries.ContainsPoint(position))
        {
            foreach (var actorInQuadtree in _actorsInQuadtree)
            {
                //if (actorsToExclude == null || !actorsToExclude.Contains(actorInQuadtree))
                    result.Add(actorInQuadtree);
            }

            if (_divided)
            {
                _northWest.GetActorsWithinActorRange(actor, result);
                _northEast.GetActorsWithinActorRange(actor, result);
                _southWest.GetActorsWithinActorRange(actor, result);
                _southEast.GetActorsWithinActorRange(actor, result);
            }
        }
    }
    
    private readonly List<Actor> _actorsWithinBoundaries = new();
    public List<Actor> GetActorsWithinBoundaries(Rectangle boundaries)
    {
        _actorsWithinBoundaries.Clear();
        if (!_boundaries.Intersects(boundaries))
        {
            return _actorsWithinBoundaries;
        }
        
        foreach (var actor in _actorsInQuadtree)
        {
            if (boundaries.ContainsPoint(actor.Position))
            {
                _actorsWithinBoundaries.Add(actor);
            }
        }

        if (_divided)
        {
            _actorsWithinBoundaries.AddRange(_northWest.GetActorsWithinBoundaries(boundaries));
            _actorsWithinBoundaries.AddRange(_northEast.GetActorsWithinBoundaries(boundaries));
            _actorsWithinBoundaries.AddRange(_southWest.GetActorsWithinBoundaries(boundaries));
            _actorsWithinBoundaries.AddRange(_southEast.GetActorsWithinBoundaries(boundaries));
        }

        return _actorsWithinBoundaries;
    }

    private void Subdivide()
    {
        var x = _boundaries.X;
        var y = _boundaries.Y;
        var w = _boundaries.Width / 2;
        var h = _boundaries.Height / 2;

        var nw = new Rectangle(x, y, w, h);
        _northWest = new Quadtree(nw, Capacity);

        var ne = new Rectangle(x + w, y, w, h);
        _northEast = new Quadtree(ne, Capacity);

        var sw = new Rectangle(x, y + h, w, h);
        _southWest = new Quadtree(sw, Capacity);

        var se = new Rectangle(x + w, y + h, w, h);
        _southEast = new Quadtree(se, Capacity);

        _divided = true;
    }
    
    public int GetTotalQuadtreeCount()
    {
        int count = 1; // Count the current quadtree node

        if (_divided)
        {
            count += _northWest.GetTotalQuadtreeCount();
            count += _northEast.GetTotalQuadtreeCount();
            count += _southWest.GetTotalQuadtreeCount();
            count += _southEast.GetTotalQuadtreeCount();
        }

        return count;
    }

    public int GetTotalActorCount()
    {
        var count = _actorsInQuadtree.Count;

        if (_divided)
        {
            count += _northWest.GetTotalActorCount();
            count += _northEast.GetTotalActorCount();
            count += _southWest.GetTotalActorCount();
            count += _southEast.GetTotalActorCount();
        }

        return count;
    }

    public override string ToString()
    {
        int totalQuadtreeCount = GetTotalQuadtreeCount();
        int totalActorCount = GetTotalActorCount();

        return $"Total Quadtree Nodes: {totalQuadtreeCount}, Total Actors: {totalActorCount}";
    }

    public void Draw()
    {
        for (var i = 0; i < _actorsInViewPort.Count; i++)
        {
            var actor = _actorsInViewPort[i];
            actor.Draw();
        }
    }
    
    public void DrawDebug()
    {
        Raylib.DrawLine((int)_boundaries.X, (int)_boundaries.Y, (int)(_boundaries.X + _boundaries.Width), (int)_boundaries.Y, Color.RED);
        Raylib.DrawLine((int)_boundaries.X, (int)_boundaries.Y, (int)_boundaries.X, (int)(_boundaries.Y + _boundaries.Height), Color.RED);
        Raylib.DrawLine((int)_boundaries.X, (int)(_boundaries.Y + _boundaries.Height), (int)(_boundaries.X + _boundaries.Width), (int)(_boundaries.Y + _boundaries.Height), Color.RED);
        Raylib.DrawLine((int)(_boundaries.X + _boundaries.Width), (int)_boundaries.Y, (int)(_boundaries.X + _boundaries.Width), (int)(_boundaries.Y + _boundaries.Height), Color.RED);

        var thickness = 5;
        Raylib.DrawLineEx(new Vector2((int)_currentViewPort.X, (int)_currentViewPort.Y), new Vector2((int)(_currentViewPort.X + _currentViewPort.Width), (int)_currentViewPort.Y), thickness, Color.GREEN);
        Raylib.DrawLineEx(new Vector2((int)_currentViewPort.X, (int)_currentViewPort.Y), new Vector2((int)_currentViewPort.X, (int)(_currentViewPort.Y + _currentViewPort.Height)), thickness, Color.GREEN);
        Raylib.DrawLineEx(new Vector2((int)_currentViewPort.X, (int)(_currentViewPort.Y + _currentViewPort.Height)), new Vector2((int)(_currentViewPort.X + _currentViewPort.Width), (int)(_currentViewPort.Y + _currentViewPort.Height)), thickness, Color.GREEN);
        Raylib.DrawLineEx(new Vector2((int)(_currentViewPort.X + _currentViewPort.Width), (int)_currentViewPort.Y), new Vector2((int)(_currentViewPort.X + _currentViewPort.Width), (int)(_currentViewPort.Y + _currentViewPort.Height)), thickness, Color.GREEN);
        
        if (_divided)
        {
            _northWest.DrawDebug();
            _northEast.DrawDebug();
            _southWest.DrawDebug();
            _southEast.DrawDebug();
        }
    }
    
    public void Dispose()
    {
        _actorsInQuadtree.Clear();
        _actorsInViewPort.Clear();
        _totalActors.Clear();
        _northWest?.Dispose();
        _northEast?.Dispose();
        _southWest?.Dispose();
        _southEast?.Dispose();
    }
}