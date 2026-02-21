using Godot;
using System;

public partial class Bleachers : StaticBody3D
{
  public override void _Ready()
  {
    base._Ready();
    // for all children of bleachers in group DOMINO
    foreach (Node domino in GetChildren())
    {
      if (domino.IsInGroup("DOMINO"))
      {
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
      }
    }
  }
}
