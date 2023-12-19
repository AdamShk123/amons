extends CharacterBody2D

@export var max_y: float = 300.0;
@export var max_x: float = 200.0;
@export var a: float = 60.0;

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

signal position_changed(pos: Vector2);

func _ready():
	pass;

func _physics_process(delta):
	# Add the gravity.
	if(not is_on_floor()):
		velocity.y = move_toward(velocity.y, max_y, gravity * delta);
	else:
		velocity.y = 0;
	
	## Handle jump.
	if(Input.is_action_pressed("jump") and is_on_floor()):
		velocity.y = -max_y;

		
	var dir: float = Input.get_axis("left", "right");
	if(dir != 0.0):
		velocity.x = move_toward(velocity.x, max_x * dir, a);
	else:
		velocity.x = move_toward(velocity.x, 0, a);
	
	var old_pos: Vector2 = position;
	move_and_slide();
	if(old_pos != position):
		position_changed.emit(position);
		
		
