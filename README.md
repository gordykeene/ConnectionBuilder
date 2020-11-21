# ConnectionBuilder

Demonstrates a method of building sets of connections using a simple set of rules.

The Rules:
1) Every node must have at least connection.
2) All connections must not cross another connection.

Examples:


Index | Left | Right
---|---|---
1  | x | x

Should yield (1 set)
	set:
		connection (1, 1)

---

Index | Left | Right
---|---|---
1  | x | x
2  | x | 

Should yield (1 set)
	set
		connection (1, 1)
		connection (2, 1)

---

Index | Left | Right
---|---|---
1  | x | x
2  | x | x

Should yield (2 sets), 
	set
		connection (1, 1)
		connection (2, 1)
	set
		connection (1, 2)
		connection (2, 2)

---

Index | Left | Right
---|---|---
1  | x | x
2  | x | 
3  | x | x

Should yield 2 result sets, 
	set
		connection (1, 1)
		connection (2, 1)
		connection (3, 3)
	set
		connection (1, 1)
		connection (2, 3)
		connection (3, 3)

---

Index | Left | Right
---|---|---
1  | x | x
2  | x | x
3  | x | x

Should yield 2 result sets, 
	set
		connection (1, 1)
		connection (2, 2)
		connection (3, 3)
	set
		connection (1, 1)
		connection (1, 2)
		connection (2, 2)
		connection (3, 3)
	set
		connection (1, 1)
		connection (1, 2)
		connection (2, 2)
		connection (2, 3)
		connection (3, 3)
	set
		connection (1, 1)
		connection (1, 2)
		connection (2, 2)
		connection (2, 3)
		connection (3, 3)

