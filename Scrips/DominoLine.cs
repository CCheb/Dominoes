using Godot;
using System;
using System.Collections.Generic;

public partial class DominoLine : Node3D
{
  [Export] public int maxDominoes = 20;
  [Export] public PackedScene dominoScene;
  [Export] public int dominoesRemaining;
  [Export] public float spacing = 1.5f;
  // number of dominoes before changing direction in the snake pattern
  [Export] public int curveNum = 5;
  // angle in radians to curve the line at each turn in the snake pattern
  [Export] public float curveAngle = 0.2f;

  public Vector3 startPosition;

  // we want a list of dominoes to keep track of them
  public List<StaticBody3D> dominoes = new List<StaticBody3D>();

  public override void _Ready()
  {
    base._Ready();
    // set start position to current position of the line
    startPosition = GlobalPosition;
    // spawn dominoes
    for (int i = 0; i < maxDominoes; i++)
    {
      StaticBody3D domino = dominoScene.Instantiate<StaticBody3D>();
      AddChild(domino);

      if (i == 0)
      {
        // first domino unchanged
        domino.GlobalPosition = startPosition;
        domino.GlobalRotation = GlobalRotation;
      }
      else
      {
        // get previous
        StaticBody3D prev = dominoes[i - 1];
        // compute delta angle for snake pattern
        int segment = i / curveNum;
        float sign = (segment % 2 == 0) ? 1f : -1f;
        // get delta angle based on segment number and curve angle
        float delta = sign * curveAngle;
        // apply rotation relative to previous
        Vector3 new_rot = prev.GlobalRotation;
        new_rot.Y += delta;
        domino.GlobalRotation = new_rot;
        // position behind previous, along its -Z direction
        Vector3 dir = prev.GlobalTransform.Basis.Z.Normalized();
        domino.GlobalPosition = prev.GlobalPosition - dir * spacing;

      }

      dominoes.Add(domino);
      // generate two random numbers for the domino faces
      int face1;
      int face2;
      // make sure they are note equal to one another
      do
      {
        // faces are 0-6
        face1 = (int)(GD.Randi() % 7);
        face2 = (int)(GD.Randi() % 7);
      } while (face1 == face2);

      // adjust to fit domino type method
      // combine faces into a single integer
      int inputInt = face1 * 10 + face2;
      // call domino's method to set its faces
      domino.Call("domino_type", inputInt);

      // connnect signal from domino to this line to keep track of how many dominoes are left
      domino.Connect("DominoHit", new Callable(this, "OnDominoHit"));
    }


    dominoes[0].Call("domino_die");
  }
}
