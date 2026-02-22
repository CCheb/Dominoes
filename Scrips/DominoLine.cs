using Godot;
using System;
using System.Collections.Generic;
using System.Numerics;
using Vector3 = Godot.Vector3;

public partial class DominoLine : Node3D
{
  [Export] public int maxDominoes = 20;
  [Export] public PackedScene dominoScene;
  [Export] public int dominoesRemaining;

  [Export] public Label countLabel;

  [Export] public AudioStreamPlayer applause;

  [Export] public float spacing = 1.5f;

  // number of dominoes before changing direction in the snake pattern
  [Export] public int curveNum = 5;

  // angle in radians to curve the line at each turn in the snake pattern
  [Export] public float curveAngle = 0.2f;

  public Vector3 startPosition;

  private bool isAboutToGetHit = false;

  // we want a list of dominoes to keep track of them
  public List<StaticBody3D> dominoes = new List<StaticBody3D>();

  // signal to emit if all dominoes are down
  [Signal]
  public delegate void DominoesDownEventHandler();


  public override void _Ready()
  {
    base._Ready();
    // set start position to current position of the line
    startPosition = GlobalPosition;
    // set dominoes remaining to max dominoes at the start
    dominoesRemaining = maxDominoes;
    UpdateCountLabel();
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
        domino.Call("set_front", true);
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

  }

  public async void OnDominoHit(int value)
  {
    if(!isAboutToGetHit)
    {
      // if the line is not supposed to get hit, ignore signal
      return;
    }
    
    // CHECK IF VALUE IS 0 TO AVOID UNNECESSARY CALCULATIONS
    if (value == 0)
    {
      return;
    }
    // CHECK IF VALUE IS GREATER THAN THE NUMBER OF DOMINOES REMAINING TO AVOID ERRORS
    if (value > dominoesRemaining)
    {
      value = dominoesRemaining;
    }

    applause.Play();

    // value recieved is the number of dominoes that will fall
    // so from the start of the line, call domino_die for value amount of dominoes
    // BEFORE THIS, we need to get positions of all dominoes so that the next dominoes in line can fill in
    // make an array of the positions and rotations of the dominoes that will fall
    Vector3[] positions = new Vector3[dominoesRemaining];
    Vector3[] rotations = new Vector3[dominoesRemaining];
    for (int i = 0; i < dominoesRemaining; i++)
    {
      positions[i] = dominoes[i].GlobalPosition;
      rotations[i] = dominoes[i].GlobalRotation;
    }

    // call kill_dominoes function
    KillDominoes(value);
    // wait a short time to allow dominoes to fall before filling in the line
    await ToSignal(GetTree().CreateTimer(3f), "timeout");
    // if all dominoes are down, emit signal to trigger win condition
    if (dominoesRemaining <= 0)
    {
      EmitSignal("DominoesDown");
      return;
    }
    // fill in the line with the next dominoes in line
    FillInLine(positions, rotations, value);
  }

  public async void KillDominoes(int value)
  {
    // first, trigger the dies with small delays between them
    for (int i = 0; i < value; i++)
    {
      dominoes[i].Call("domino_die");
      // small delay before triggering the next to fall
      if (i < value - 1)
      {
        await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
      }
    }

    // now wait longer to allow them to complete falling before deletion
    await ToSignal(GetTree().CreateTimer(1.5f), "timeout");
    // now delete them
    for (int i = 0; i < value; i++)
    {
      Node3D temp = dominoes[0];
      dominoes.RemoveAt(0);
      temp.QueueFree();
      dominoesRemaining--;
      UpdateCountLabel();
    }
  }

  public void FillInLine(Vector3[] positions, Vector3[] rotations, int value)
  {
    // starting from the beginning of the remaining line, move each domino to the position and rotation of the domino that just fell
    for (int i = 0; i < dominoesRemaining; i++)
    {
      dominoes[i].GlobalPosition = positions[i];
      dominoes[i].GlobalRotation = rotations[i];
    }
    dominoes[0].Call("set_front", true);
  }

  public void UpdateCountLabel()
  {
    countLabel.Text = "Dominoes Remaining: " + dominoesRemaining.ToString();
  }

  public void SetIsAboutToGetHit(bool value)
  {
    isAboutToGetHit = value;
  }
  
  public bool GetIsAboutToGetHit()
  {
    return isAboutToGetHit;
  }
}



