# OOP Item Class


## Must have a constructor that accepts:

<ol>
	<li>name</li>
	<li>count - with a default value of 1. Represents the ""starting amount"" this stack will have</li>
	<li>
		max_stack_size - with a default value of 1
	</li>
</ol>

## Must have the following attributes

<ol>
	<li>'name' - which represents the item's name</li>
</ol>

## Must have the following properties

<ol>
	<li>'count' - which represents the number of items in this ""stack""</li>
	<li>'max_stack_size' - which represents the max stack size provided at creation.</li>
	<li>'is_stackable' - which returns true if 'max_stack_size' is greater than 1
</ol>

## Functionality

### Addition

I should be able to add a number to this item in the following manner:
```python
item += 2
```

If I try adding too much, aka - value exceeds the item's max stack size, I should receive a 
**ValueError** with the message ""Exceeds stack size"". The 'count' must be whatever it was
""before"" I tried adding too much.
<br><br>
For instance, if I have 32 arrows with a stack size of 64, I try adding 128. The end count should be 32.
<br><br>
Additionally, see what I did there...haha..., I should be able to add similar items together in the same way
```python
item += second_item
```
Same rules apply as above, but if successful, **second_item**'s count should be 0.
<br><br>
If I try adding two items that are different (don't have the same name) I should get a **ValueError**
with the message ""Cannot add differing types""

----

### Subtraction

I should be able to subtract a number from this item in the following manner:
```python
item -= 2
```

If I try removing too much, aka - value would put the item count to be less than 0, I should receive a
**ValueError** with the message ""Not Enough"". The 'count' must be whatever it was ""before""
I tried removing too much
<br><br>
For instance, if I have 32 arrows with a stack size of 64, I try removing 128. The end count should be 32

----

### Length

I should also be able to retrieve the item's current count in the following manner:
```python
len(item)
```

----

### Display

When the item gets printed, or converted into text it should display in the following manner:
```python
NAME, COUNT/MAX_STACK_SIZE
```