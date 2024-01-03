# Task Coordination

- Call `ContinueWith()` on a task to have another task follow
	- State, cancellation options
- Continuations can be conditional
	- E.g., TaskContinuationOptions.NotOnFaulted 
- One-to-many continuations
	- `Task.Factory.ContinueWhenAll()`
- One-to-any continuations
	- `Task.Factory.ContinueWhenAny()`	
- Beware of waiting on continuations that might not occur

# Child Tasks
- Detached
	- Just a task created with the task
	- Same rules as a task created anywhere
- Attached  