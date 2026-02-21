using Godot;
using System;
using System.Collections.Generic;

public partial class DominoLine : Node3D
{
  [Export] public int maxDominoes = 20;
  [Export] public PackedScene dominoScene;
  [Export] public int dominoesRemaining;
  [Export] public float spacing = 1.0f;
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
      // add domino spacing in a sine wave pattern for visual interest
      float xOffset = i * spacing;
      float zOffset = Mathf.Sin(i * 0.5f) * 0.5f; // sine wave for z offset
      domino.GlobalPosition = startPosition + new Vector3(xOffset, 0, zOffset);
      dominoes.Add(domino);
      // generate two random numbers for the domino faces
      int face1 = (int)(GD.Randi() % 7); // 0-6
      int face2 = (int)(GD.Randi() % 7); // 0-6
      // adjust to fit domino type method
      int inputInt = face1 * 10 + face2; // combine faces into a single integer
      // call domino's method to set its faces
      domino.Call("domino_type", inputInt);
    }
  }
}
