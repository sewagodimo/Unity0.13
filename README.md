[TOC]

# Roll-A-Ball Tutorial #
This project is a tutorial based on Unity3D's [Roll-A-Ball](https://unity3d.com/learn/tutorials/projects/roll-ball-tutorial) tutorial.

# Roll-A-Ball Tutorial 2.0 #

## What is this tutorial for? ##
This tutorial acts as an introduction to the Unity work-flow. In contrast to the original Roll-A-Ball tutorial (which you should do), in this tutorial you will be fixing all the broken things! 

Hopefully by the end of this tutorial, you will understand how to use GameObjects, Prefabs, Components (e.g. MonoBehaviours) and Scenes a little better.

## Who do I talk to? ##
* If you have any questions, please contact the owner of this project (Ryan Mazzolini).

## How do I get set up? ##
* Clone the project from [https://bitbucket.org/creative630/rollaball](https://bitbucket.org/creative630/rollaball)
* Open the project in Unity, you can do this by browsing to the project through Unity, or by double clicking on a scene in the project's Assets folder.
* If not open already, open the "**RollABall**" scene in the Scenes folder under the project view.

### Play the game! ###
Press the play button. (The left most button in the image below.)

![Play button image here...](ReadMeImages/PlayPauseFrame.png)

You can move around in the game by using the ***WSAD***, or ***arrow keys***.
You might notice that you can't do very much in the game at the moment. You can roll around.... and that's it.

### Lets fix this! ###

First, create a new ***empty game object*** and make sure that all of its transform values are 0. This places the transform at the *worlds origin point*.

![Transform at the origin](ReadMeImages/TransformAtTheOrigin.png)

Next add a MonoBehaviour script called "**Collectable Spawner**" to the GameObject. This is already created for you, and is found in the "Scripts" folder. You can add the new component by clicking on the "Add Component" button below the transform. Your GameObject should now look like this:

![CollectableSpawner.png](ReadMeImages/CollectableSpawner.png)

If you run the game at this point you should receive an "**UnassignedReferenceException**" error message in the console. You can find the console under the Window menu, or by clicking on the red message at the bottom of the Unity Editor. To fix this, add the prefab called "Collectable" from the project menu to the empty component variable called "**Collectable Prefab**". It should now look like this:

![CollectableSpawner_2.png](ReadMeImages/CollectableSpawner_2.png)

Run the game! You should now be able to bash into collectables, which breaks apart and plays an audio clip.

Do you notice the yellow warning-text saying "**ScoreText is null**" when you bashed into a collectable?? Well that's no good...
I think one of the components is trying to tell you something...

``` csharp
if(ScoreText == null)
{
    Debug.LogWarning("ScoreText is null!");
}
```

### Lets add text UI! ###
Click on the ***GameObject***, then ***UI***, then ***Text*** menu items. This will create a Canvas with a text object called Text. It should look something like this in the project hierarchy.

![Text1.png](ReadMeImages/Text1.png)

Getting the hang of setting up game objects yet? Well select the Text object and make it look like this:

![Text2.png](ReadMeImages/Text2.png)

If you look at the game view you should see the text in the bottom left hand corner! If not double check all the component variables on the Text object.

The warning message is still there you say? Well lets get rid of it. Drag the Text object from the hierarchy onto the "Collectable Spawner" and Player's "Player controller" script.

The text should be working now! Yay! A complete game!

![Complete.png](ReadMeImages/Complete.png)

## Now the scripts... ##
Wondering how all the scripts work? Well lets go look at them! Maybe we can even find some things to add to the game ;)

### The player controller ###
The player controller controls a few things. This includes the player movement, particle effect creation and score manipulation. 

First we have variable declarations and component caching. The *Speed*, *CollectableParticlePrefab* and *ScoreText* objects are public, which allows them to be set in the Editor. (Watch out for overwriting the values between script and inspector!)

```csharp
public float Speed;
public GameObject CollectableParticlePrefab;
public Text ScoreText;
```

Next the ***RigidBody*** component is chached in the **Start** event. It would be very expensive to get the RigidBody in every update!

```csharp
private Rigidbody _rigidBody;

void Start ()
{
	_rigidBody = GetComponent<Rigidbody>();
}
```

The movement is handled in the ***FixedUpdate*** event. As we are doing physics calculations, we want to do this in-synch with the physics engine. If you would like to do this in the Update make sure to use deltaTime! 

The <code>Input.GetAxis("Horizontal")</code> and <code>Input.GetAxis ("Vertical")</code> are a short hand for getting the individual input keys, such as <code>Input.GetKey(KeyCode.UpArrow)</code>. 

A force is applied in the direction of movement across the X-Z plane. This is calculated in the ***movement*** variable. Notice how the Speed variable is used here? Well you can adjust the Speed in the inspector during play to find the best movement force.

```csharp
void FixedUpdate ()
{
	float moveHorizontal = Input.GetAxis ("Horizontal");
	float moveVertical = Input.GetAxis ("Vertical");

	Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

	_rigidBody.AddForce (movement * Speed);
}
```

The ***OnTriggerEnter*** event is called when a collider collides with a collider that acts as a trigger (the IsTrigger check box is ticked). A trigger collider does not react phyiscally, but will be called on collisions. In contrast the ***OnCollisionEnter*** event is called between to colliders that are not triggers, which also react physically with one another. 

**Try making the *Collectable* object's collider a non-trigger collider and replace the OnTriggerEnter function with the [OnCollisionEnter](http://docs.unity3d.com/ScriptReference/Collider.OnCollisionEnter.html).**

**Edit:** If you were having trouble with the trigger/collision. The **OnTriggerEnter** will only get called if the Collectable's *IsTrigger* check-box is checked (this is now ticked by default). For the OnCollisionEnter to be called the check-box must **not** be checked.

```csharp
void OnTriggerEnter(Collider other)
{
	if (other.gameObject.tag == "Collectable") 
	{
        Debug.Log("Bashed into " + other.gameObject.tag);

        Instantiate (CollectableParticlePrefab, other.transform.position, other.transform.rotation);
		Destroy (other.gameObject);
	    CollectableSpawner.NumCollectablesFound++;

		if(ScoreText == null)
		{
			Debug.LogWarning("ScoreText is null!");
		}

	    if (CollectableSpawner.NumCollectablesFound >= CollectableSpawner.NumCollectables)
        {
            if (ScoreText != null)
            {
                ScoreText.text = "You Win!";
            }
			//Hmmm... why is this commented out?
//                Invoke("ReloadScene", 5);
	    }
	    else
        {
            if (ScoreText != null)
            {
                ScoreText.text = "Collectables: " + CollectableSpawner.NumCollectablesFound + "/" +
                                 CollectableSpawner.NumCollectables;
            }
        }
    }
}
```

### The rotator script ###
The "***Rotator***"script is very simple at the moment. Attach it to your "***Collectable***" prefab to make your Collectable's rotate.
```csharp
public class Rotator : MonoBehaviour
{
	void Update()
	{
		//Note how deltatime is used here (Hint: not in FixedUpdate!)
		transform.Rotate(new Vector3(15f,30f,45f)*Time.deltaTime);
	}
}
```

**Try add a random spin direction and speed to the rotation.**

### The Collectable Spawner script ###
The "**Collectable Spawner**" script instantiates a number of collectables in a circle. This is oriented from the transforms position and rotation, so try modify the transform! See how it effects the spawn positions?

```csharp
void OnEnable ()
{
    for (int i = 0; i<NumberOfCollectables; i++)
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition += new Vector3 (Mathf.Sin((Mathf.PI*2 /NumberOfCollectables)*i ),0, Mathf.Cos((Mathf.PI*2 /NumberOfCollectables)*i)) * SpawnRadius;
        Instantiate(CollectablePrefab, spawnPosition, transform.rotation);
        NumCollectables++;

    }
}
```

**Try add spawn position randomization by using:**

```csharp
Random.Range()
```

Notice that the instantiation is is in an **OnEnable** function? With your new randomisation, try enable or disable the Collectable Spawner when the game is playing.