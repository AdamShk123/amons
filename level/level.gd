extends Node2D

var player_node: CharacterBody2D;
var camera_node: Camera2D;

func _ready():
	player_node = get_node("Player");
	camera_node = get_node("Camera");
	player_node.position_changed.connect(camera_node._on_player_position_changed);

func _process(delta):
	pass
