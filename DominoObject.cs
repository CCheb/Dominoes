using Godot;
using System;

public partial class DominoObject : StaticBody3D
{
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


    public CollisionShape3D top;
    public CollisionShape3D bottom;
    public int top_value = 0;
    public int bottom_value = 0;

    public override void _Ready()
    {
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


        //get collision shaped
        top = GetNode<CollisionShape3D>("Collision_Top");
        bottom = GetNode<CollisionShape3D>("Collision_Bottom");


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
    }


    public void domino_type(int number)
    {
        if (number == 12)
        {
            top_value = 1;
            bottom_value = 2;
            Domino_12.Visible = true;
        }
        if (number == 13)
        {
            top_value = 1;
            bottom_value = 3;
            Domino_13.Visible = true;
        }
        if (number == 14)
        {
            top_value = 1;
            bottom_value = 4;
            Domino_14.Visible = true;
        }
        if (number == 15)
        {
            top_value = 1;
            bottom_value = 5;
            Domino_15.Visible = true;
        }
        if (number == 16)
        {
            top_value = 1;
            bottom_value = 6;
            Domino_16.Visible = true;
        }
        if (number == 23)
        {
            top_value = 2;
            bottom_value = 3;
            Domino_23.Visible = true;
        }
        if (number == 24)
        {
            top_value = 2;
            bottom_value = 4;
            Domino_24.Visible = true;
        }
        if (number == 25)
        {
            top_value = 2;
            bottom_value = 5;
            Domino_25.Visible = true;
        }
        if (number == 26)
        {
            top_value = 2;
            bottom_value = 6;
            Domino_26.Visible = true;
        }
        if (number == 34)
        {
            top_value = 3;
            bottom_value = 4;
            Domino_34.Visible = true;
        }
        if (number == 35)
        {
            top_value = 3;
            bottom_value = 5;
            Domino_35.Visible = true;
        }
        if (number == 36)
        {
            top_value = 3;
            bottom_value = 6;
            Domino_36.Visible = true;
        }
        if (number == 45)
        {
            top_value = 4;
            bottom_value = 5;
            Domino_45.Visible = true;
        }
        if (number == 46)
        {
            top_value = 4;
            bottom_value = 6;
            Domino_46.Visible = true;
        }
        if (number == 56)
        {
            top_value = 5;
            bottom_value = 6;
            Domino_56.Visible = true;
        }
    }

    //colision function 
}
