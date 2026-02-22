using Godot;
using System;

public partial class Main : Node3D
{
    [Export] public Node3D P1;
    [Export] public Node3D P2;
    [Export] public Camera3D TransCamera1;
    [Export] public Camera3D TransCamera2;
    [Export] public Node3D DominoLine1;
    [Export] public Node3D DominoLine2;
    [Export] public Control WinScreen;
    [Export] public Label WinLabel;

    public bool isP1Turn = true;
    
    // This game is a turn-based game between two players
    // Each player has a camera and a line of dominoes (camera is a child of player)
    // There are also two transition cameras showing the dominoes when hit

    public override void _Ready()
    {
        WinScreen.Visible = false;
        SetP1Turn();

        // connect signals from domino lines to trigger camera transitions
        P2.GetNode<ShootingState>("PlayerStateMachine/ShootingState").Connect("DominoWasHit", new Callable(this, "TriggerTransitionCamera2"));
        P1.GetNode<ShootingState>("PlayerStateMachine/ShootingState").Connect("DominoWasHit", new Callable(this, "TriggerTransitionCamera1"));
        // connect signals for when all dominoes are down to trigger win screens
        DominoLine1.Connect("DominoesDown", new Callable(this, "Domino1LineDown"));
        DominoLine2.Connect("DominoesDown", new Callable(this, "Domino2LineDown"));
    }



    public void SetP1Turn()
    {  
        isP1Turn = true;
        P1.GetNode<Control>("Control").Visible = true;
        P1.Visible = true;

        var p2State = P2.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        if (p2State != null) p2State.SetAcceptingInput(false);

        P2.GetNode<Control>("Control").Visible = false;
        P2.Visible = false;
        // Set current camera to player 1's camera (path CameraController - Camera3D)
        P1.GetNode<Camera3D>("CameraController/Camera3D").Current = true;
        // disable shot reception for player 2's line true
        DominoLine2.Call("SetIsAboutToGetHit", true);
        DominoLine1.Call("SetIsAboutToGetHit", false);
    }
    public void SetP2Turn()
    {
        isP1Turn = false;
        P1.GetNode<Control>("Control").Visible = false;
        P1.Visible = false;

        var p1State = P1.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        if (p1State != null) p1State.SetAcceptingInput(false);
        
        P2.GetNode<Control>("Control").Visible = true;
        P2.Visible = true;
        // Set current camera to player 2's camera (path CameraController - Camera3D)
        P2.GetNode<Camera3D>("CameraController/Camera3D").Current = true;
        // disable shot reception for player 1's line true
        DominoLine1.Call("SetIsAboutToGetHit", true);
        DominoLine2.Call("SetIsAboutToGetHit", false);
    }
    public async void TriggerTransitionCamera1()
    {
        if(!isP1Turn)
            return;
        // stop clicks from triggering shooting state while in transition
        var p1State = P1.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        var p2State = P2.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        if (p1State != null) p1State.SetAcceptingInput(false);
        if (p2State != null) p2State.SetAcceptingInput(false);

        // for all children in group "BLEACHERS" call the method "DominoesHop" to make them hop
        foreach (Node bleacher in GetTree().GetNodesInGroup("BLEACHERS"))
        {
            bleacher.Call("DominoesHop");
        }

        TransCamera1.Current = true;
        await ToSignal(GetTree().CreateTimer(5.0f), "timeout");
        // re-enable input and switch to P2
        if (p1State != null) p1State.SetAcceptingInput(true);
        if (p2State != null) p2State.SetAcceptingInput(true);
        SetP2Turn();
    }
    public async void TriggerTransitionCamera2()
    {
        if(isP1Turn)
            return;
        var p1State = P1.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        var p2State = P2.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        if (p1State != null) p1State.SetAcceptingInput(false);
        if (p2State != null) p2State.SetAcceptingInput(false);

        // for all children in group "BLEACHERS" call the method "DominoesHop" to make them hop
        foreach (Node bleacher in GetTree().GetNodesInGroup("BLEACHERS"))
        {
            bleacher.Call("DominoesHop");
        }

        TransCamera2.Current = true;
        await ToSignal(GetTree().CreateTimer(5.0f), "timeout");
        // re-enable input and switch to P1
        if (p1State != null) p1State.SetAcceptingInput(true);
        if (p2State != null) p2State.SetAcceptingInput(true);
        SetP1Turn();
    }
    public async void Domino1LineDown()
    {
        await ToSignal(GetTree().CreateTimer(2.0f), "timeout");

        var p1State = P1.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        var p2State = P2.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        if (p1State != null) p1State.SetAcceptingInput(false);
        if (p2State != null) p2State.SetAcceptingInput(false);

        WinScreen.Visible = true;
        WinLabel.Text = "Player 2 Wins!";
        var playAgainButton = WinScreen.GetNode<Button>("ColorRect/MarginContainer/VBoxContainer/PlayAgainButton");
        playAgainButton.GrabFocus();
    }
    public async void Domino2LineDown()
    {
        await ToSignal(GetTree().CreateTimer(2.0f), "timeout");

        var p1State = P1.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        var p2State = P2.GetNodeOrNull<PlayerStateMachine>("PlayerStateMachine");
        if (p1State != null) p1State.SetAcceptingInput(false);
        if (p2State != null) p2State.SetAcceptingInput(false);

        WinScreen.Visible = true;
        WinLabel.Text = "Player 1 Wins!";
        var playAgainButton = WinScreen.GetNode<Button>("ColorRect/MarginContainer/VBoxContainer/PlayAgainButton");
        playAgainButton.GrabFocus();
    }

    public void OnPlayAgainButtonPressed()
    {
        GetTree().ReloadCurrentScene();
    }

    public void OnQuitButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/start_screen.tscn");
    }
}
