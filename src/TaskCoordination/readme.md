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
	- `TaskCreationOptions.AttachedToParent`
	- Waiting on parent => waiting on child
- Child tasks can be continuations

# Synchronization Primitives
- All do the same thing
	- They have a counter
	- Let you execute N threads at a time
	- Other threads are unblocked until state changes
- `Barrier`: blocks all threads until N are waiting, then lets those N through via `SignalAndWait`
- `CountdownEvent`: Signaling and waiting separate; waitis until signal level reaches 0, then unblocks
- `ManualResetEventSlim` / `AutoResetEvent`: like `CountdownEvent` with a count of 1; `AutoResetEvent` resets after waiting
- `SemaphoreSlim`: counter `CurrentCount` decreased by `Wait()` and increased by `Release(N)`; can have a maximum.