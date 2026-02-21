using Godot;
using System;

public partial class DominoObject : StaticBody3D
{   
    [Export] public Area3D top_area;
    [Export] public Area3D bottom_area;

    public Node3D Domino_01;
    public Node3D Domino_02;
    public Node3D Domino_03;
    public Node3D Domino_04;
    public Node3D Domino_05;
    public Node3D Domino_06;
    public Node3D Domino_12;
    public Node3D Domino_13;
    public Node3D Domino_14;
    public Node3D Domino_15;
    public Node3D Domino_16;
    public Node3D Domino_23;
    public Node3D Domino_24;
    public Node3D Domino_25;
    public Node3D Domino_26;
    public Node3D Domino_34;
    public Node3D Domino_35;
    public Node3D Domino_36;
    public Node3D Domino_45;
    public Node3D Domino_46;
    public Node3D Domino_56;

    public int top_value = 0;
    public int bottom_value = 0;

    private float startRotationX = 0;
    private float targetRotationX = -80;

    int currentXDegrees = 0;

    public bool falling = false;
    public bool isPreRotated = false;

    // signal to emit the number value of the domino face that was hit
    [Signal]
    public delegate void DominoHitEventHandler(int value);

    public override void _Ready()
    {
        currentXDegrees = (int)Mathf.RadToDeg(Rotation.X);
        Domino_01 = GetNode<Node3D>("01");
        Domino_02 = GetNode<Node3D>("02");
        Domino_03 = GetNode<Node3D>("03");
        Domino_04 = GetNode<Node3D>("04");
        Domino_05 = GetNode<Node3D>("05");
        Domino_06 = GetNode<Node3D>("06");
        Domino_12 = GetNode<Node3D>("12");
        Domino_13 = GetNode<Node3D>("13");
        Domino_14 = GetNode<Node3D>("14");
        Domino_15 = GetNode<Node3D>("15");
        Domino_16 = GetNode<Node3D>("16");
        Domino_23 = GetNode<Node3D>("23");
        Domino_24 = GetNode<Node3D>("24");
        Domino_25 = GetNode<Node3D>("25");
        Domino_26 = GetNode<Node3D>("26");
        Domino_34 = GetNode<Node3D>("34");
        Domino_35 = GetNode<Node3D>("35");
        Domino_36 = GetNode<Node3D>("36");
        Domino_45 = GetNode<Node3D>("45");
        Domino_46 = GetNode<Node3D>("46");
        Domino_56 = GetNode<Node3D>("56");

        Domino_01.Visible = false;
        Domino_02.Visible = false;
        Domino_03.Visible = false;
        Domino_04.Visible = false;
        Domino_05.Visible = false;
        Domino_06.Visible = false;
        Domino_12.Visible = false;
        Domino_13.Visible = false;
        Domino_14.Visible = false;
        Domino_15.Visible = false;
        Domino_16.Visible = false;
        Domino_23.Visible = false;
        Domino_24.Visible = false;
        Domino_25.Visible = false;
        Domino_26.Visible = false;
        Domino_34.Visible = false;
        Domino_35.Visible = false;
        Domino_36.Visible = false;
        Domino_45.Visible = false;
        Domino_46.Visible = false;
        Domino_56.Visible = false;

        top_area.BodyEntered += OnTopCollisionEnter;
        bottom_area.BodyEntered += OnBottomCollisionEnter;
    }


    public void domino_type(int number)
    {
        // switch case to determine which domino to show based on the top and bottom values
        switch (number)
        {
            case 1:
                top_value = 0;
                bottom_value = 1;
                Domino_01.Visible = true;
                break;
            case 2:
                top_value = 0;
                bottom_value = 2;
                Domino_02.Visible = true;
                break;
            case 3:
                top_value = 0;
                bottom_value = 3;
                Domino_03.Visible = true;
                break;
            case 4:
                top_value = 0;
                bottom_value = 4;
                Domino_04.Visible = true;
                break;
            case 5:
                top_value = 0;
                bottom_value = 5;
                Domino_05.Visible = true;
                break;
            case 6:
                top_value = 0;
                bottom_value = 6;
                Domino_06.Visible = true;
                break;
            case 12:
                top_value = 1;
                bottom_value = 2;
                Domino_12.Visible = true;
                break;
            case 13:
                top_value = 1;
                bottom_value = 3;
                Domino_13.Visible = true;
                break;
            case 14:
                top_value = 1;
                bottom_value = 4;
                Domino_14.Visible = true;
                break;
            case 15:
                top_value = 1;
                bottom_value = 5;
                Domino_15.Visible = true;
                break;
            case 16:
                top_value = 1;
                bottom_value = 6;
                Domino_16.Visible = true;
                break;
            case 23:
                top_value = 2;
                bottom_value = 3;
                Domino_23.Visible = true;
                break;
            case 24:
                top_value = 2;
                bottom_value = 4;
                Domino_24.Visible = true;
                break;
            case 25:
                top_value = 2;
                bottom_value = 5;
                Domino_25.Visible = true;
                break;
            case 26:
                top_value = 2;
                bottom_value = 6;
                Domino_26.Visible = true;
                break;
            case 34:
                top_value = 3;
                bottom_value = 4;
                Domino_34.Visible = true;
                break;
            case 35:
                top_value = 3;
                bottom_value = 5;
                Domino_35.Visible = true;
                break;
            case 36:
                top_value = 3;
                bottom_value = 6;
                Domino_36.Visible = true;
                break;
            case 45:
                top_value = 4;
                bottom_value = 5;
                Domino_45.Visible = true;
                break;
            case 46:
                top_value = 4;
                bottom_value = 6;
                Domino_46.Visible = true;
                break;
            case 56:
                top_value = 5;
                bottom_value = 6;
                Domino_56.Visible = true;
                break;
            // CASES BELOW THIS ARE FOR SWITCHED DOMINOES!!!!!!!!!
            // Therefore you need to rotate the domino 180 degrees
            // and switch the top and bottom values
            case 10:
                top_value = 1;
                bottom_value = 0;
                Domino_01.Visible = true;
                RotateAroundZ(Domino_01);
                break;
            case 20:
                top_value = 2;
                bottom_value = 0;
                Domino_02.Visible = true;
                RotateAroundZ(Domino_02);
                break;
            case 30:
                top_value = 3;
                bottom_value = 0;
                Domino_03.Visible = true;
                RotateAroundZ(Domino_03);
                break;
            case 40:
                top_value = 4;
                bottom_value = 0;
                Domino_04.Visible = true;
                RotateAroundZ(Domino_04);
                break;
            case 50:
                top_value = 5;
                bottom_value = 0;
                Domino_05.Visible = true;
                RotateAroundZ(Domino_05);
                break;
            case 60:
                top_value = 6;
                bottom_value = 0;
                Domino_06.Visible = true;
                RotateAroundZ(Domino_06);
                break;
            case 21:
                top_value = 2;
                bottom_value = 1;
                Domino_12.Visible = true;
                RotateAroundZ(Domino_12);
                break;
            case 31:
                top_value = 3;
                bottom_value = 1;
                Domino_13.Visible = true;
                RotateAroundZ(Domino_13);
                break;
            case 41:
                top_value = 4;
                bottom_value = 1;
                Domino_14.Visible = true;
                RotateAroundZ(Domino_14);
                break;
            case 51:
                top_value = 5;
                bottom_value = 1;
                Domino_15.Visible = true;
                RotateAroundZ(Domino_15);
                break;
            case 61:
                top_value = 6;
                bottom_value = 1;
                Domino_16.Visible = true;
                RotateAroundZ(Domino_16);
                break;
            case 32:
                top_value = 3;
                bottom_value = 2;
                Domino_23.Visible = true;
                RotateAroundZ(Domino_23);
                break;
            case 42:
                top_value = 4;
                bottom_value = 2;
                Domino_24.Visible = true;
                RotateAroundZ(Domino_24);
                break;
            case 52:
                top_value = 5;
                bottom_value = 2;
                Domino_25.Visible = true;
                RotateAroundZ(Domino_25);
                break;
            case 62:
                top_value = 6;
                bottom_value = 2;
                Domino_26.Visible = true;
                RotateAroundZ(Domino_26);
                break;
            case 43:
                top_value = 4;
                bottom_value = 3;
                Domino_34.Visible = true;
                RotateAroundZ(Domino_34);
                break;
            case 53:
                top_value = 5;
                bottom_value = 3;
                Domino_35.Visible = true;
                RotateAroundZ(Domino_35);
                break;
            case 63:
                top_value = 6;
                bottom_value = 3;
                Domino_36.Visible = true;
                RotateAroundZ(Domino_36);
                break;
            case 54:
                top_value = 5;
                bottom_value = 4;
                Domino_45.Visible = true;
                RotateAroundZ(Domino_45);
                break;
            case 64:
                top_value = 6;
                bottom_value = 4;
                Domino_46.Visible = true;
                RotateAroundZ(Domino_46);
                break;
            case 65:
                top_value = 6;
                 bottom_value = 5;
                Domino_56.Visible = true;
                RotateAroundZ(Domino_56);
                break;
            default:
                GD.Print(number.ToString() + " is an invalid domino type");
                break;
        }
    }

    private void RotateAroundZ(Node3D domino01)
    {
        // rotate the domino 180 degrees around the Z axis about the point
        // at the domino's center offset by +1 in Y (local pivot = origin + (0,1,0)).
        Transform3D t = domino01.Transform;
        Vector3 pivot = t.Origin + Vector3.Up;
        t = t.Translated(-pivot);
        t = t.Rotated(new Vector3(0, 0, 1), Mathf.Pi);
        t = t.Translated(pivot);
        domino01.Transform = t;

        // set isPreRotated to true so that the domino's collisions aren't mixed up
        isPreRotated = true;
    }

    // collision functions 
    public void OnTopCollisionEnter(Node3D body)
    {
        // check if its in group BULLET
        if (body.IsInGroup("BULLET"))
        {
            if(isPreRotated)
            {
                // if the domino is pre-rotated then the top and bottom values are switched
                EmitSignal("DominoHit", bottom_value);
                return;
            }
            // emit signal with the value of the top face of the domino
            EmitSignal("DominoHit", top_value);
        }
    }

    public void OnBottomCollisionEnter(Node3D body)
    {
        // check if its in group BULLET
        if (body.IsInGroup("BULLET"))
        {
            if(isPreRotated)
            {
                // if the domino is pre-rotated then the top and bottom values are switched
                EmitSignal("DominoHit", top_value);
                return;
            }
            // emit signal with the value of the bottom face of the domino
            EmitSignal("DominoHit", bottom_value);
        }
    }


    //domino die
    //when called this will make the domino fall
    //need to lerp this
    public void domino_die()
    {
        falling = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        
        if(falling)
        {
            if(currentXDegrees > -90)
            {
                int rotation = currentXDegrees -1;
                RotateX(Mathf.DegToRad(-2f));
            }
        }
        currentXDegrees = (int)Mathf.RadToDeg(Rotation.X);
    }


}
