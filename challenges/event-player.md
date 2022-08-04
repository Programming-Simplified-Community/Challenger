# Events

As a project grows in size you might notice a lot of [coupling](https://blog.ndepend.com/programming-coupling/).

Having so many objects depend on each other can be problematic as it increases complexity and 
decreases maintainability.

For this challenge we'll be utilizing events! A method of combating coupling! 

If you consider video games, a player taking damage is an excellent example of an event.
When the player takes damage you have multiple systems communicating with each other

- Health bar needs to update
- Blood splatter on screen
- Perhaps some numbers appear over character to indicate damage per second

These different systems aren't directly tied to the player. They're just `listening`. Waiting 
for the player to go `HEY I uh.. got hurt`. Events can also pass data around. In this 
example it'd be the players new health value! 

Events allow us remove an entire system/component without another class completely breaking due to the tight coupling.

-----

# Requirements

Create a class called `EventHandler`

### subscribe

As a different system/class I should be able to say... hey sign me up for this
event name! Here's the function (also called the callback) to call whenever this
event is triggered.

#### parameters

1. name - event name
2. func - function/something callable

```python
def example_callback():
    pass

# simply showing the way to interact with method
subscribe("MY_AWESOME_EVENT_NAME", example_callback)
```

The parameter `func` **Must** be validated. If it's not a method/function it should be rejected!
Raise a `ValueError` with the message "func must be callable."

```python
subscribe("MY_AWESOME_EVENT_NAME", 123) # raises error
```

### notify

This method will execute all the functions associated with a given event name!

#### parameters

1. name - event name
2. data - data to send to these functions

```python
notify("MY_AWESOME_EVENT_NAME", 123)
```

-------

Now create a class called `Player` which inherits from `EventHandler`. 

Player should have only 1 parameter for `name`.

By default, a player starts with 100 `health`.

1. The `Player` class should have a `health` property which returns the current health
2. The `Player` class should have a `setter` for `health`. When the health changes it should notify subscribers that its health has changed.

`on_health_changed` is the event name associated to characters whose health has changed in value.

```python
def log(data):
    print(data)

player = Player("badger 2-3")
player.subscribe("on_health_changed", log)
player.health -= 2

# should print 98

player.health -= 10

# should print 88
```

The example shows 1 subscription but I should be able to have multiple subscriptions per event. Think of it as a
mailing list. Everyone subscribed gets notified.
