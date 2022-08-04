# OOP Inventory
Building off previous OOP challenges we'll be creating a simple Inventory-like system!

For this challenge you must create a class called `Inventory`. 

## Must have a constructor that accepts:

1. size - with a default value of `10`. This indicates the size of our inventory!

## Must have the following properties

1. size - indicating the size of our inventory
2. open_slots - returns the number of empty slots
3. is_full - returns True if no slots are available

## Functionality

**Items inside our inventory are represented by either strings, or `None` if empty**

Like in a video game, I should be able to place an item into a specified slot.
From a user perspective, slots are represented via 1 through size of inventory

So, 1 through 10 if we have an inventory with 10 slots. 

### Placing Item

I should be able to place an item into a specified slot in the following manner:

```python
inv = Inventory(5)

inv[5] = "Health Potion"
inv[1] = "Shield"

# under the hood, it should look like the following
# [ "Shield", None, None, None, "Health Potion" ]
```

If I attempt to add something to an inventory slot that's **not** valid
I should get a `ValueError` with the message "Invalid inventory slot".

### Removing items

If we pretend the items from above are in there,

I should be able to remove an item from a specified slot in the following manner:

```python
del inv[1]

# under the hood, it should look like the following
# [ None, None, None, None, "Health Potion" ]
```

If I attempt to remove something from an inventory slot that's **not** valid
I should get a `ValueError` with the message "Invalid inventory slot".

## Length

I should be able to retrieve the inventory size via:

```python
inv = Inventory(20)
len(inv)

# 20
```

## Getting

I should be able to retrieve an item in a specified slot in the following manner:
```python
inv = Inventory(2)
inv[1] = "Shield"
inv[2] = "Arrows"

print(inv[1]) # Shield
```

If the inventory slot is empty, I should get `None` back.

## Looping

I should be able to loop through the inventory in the following manner:

```python
inv = Inventory(5)
inv[5] = "Health Potion"
inv[1] = "Shield"

for slot, item in inv:
    print(f"{slot}: {item}")
```

```
1: Health Potion
2: Empty
3: Empty
4: Empty
5: Shield
```

If the inventory slot is empty, instead of returning `None` I should get "Empty" back instead

## Showcase Full / Open

```python
inv = Inventory(2)
inv[1] = "meow"
inv[2] = "mix"

print(inv.open_slots) # 0
print(inv.is_full) # True
```