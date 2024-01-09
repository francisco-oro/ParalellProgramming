# Parallel Loops
- Parallel.Xxx are blocking calls
	- Wait until all threads completed or an exception occured	
	
- Can check the state of the loop as it is executing in `ParallelLooopState`
- Can check the result of execution in `ParallelLoopResult`
- `ParallelLoopOptions` let us customize execution with
	- Max. degree of parallelism
	- Cancellation token

# Parallel.Invoke
- Runs several provided functions concurrently
- Is equivalent to
	- Creating a task for each lambda
	- Doing `Task.WaitAll()` on all the tasks

# Parallel.For/ForEach
- Parallel.For
	- Uses an index [start; finish)
	- Cannot provide a step
		- Create an `IEnumerable<int>` and use `Parallel.ForEach`
	- Partitions data into different tasks
	- Executes provided delegate with counter value argument
		- Might be inefficient
- Parallel.ForEach
	- Like `Parallel.For()` but
	- Takes an `IEnumerable<T>` instead

# Thread local storage
- Writing ot a shared variable from many tasks is inefficient
- Can store partially evaluated results for each task
- Can specify a function to integrate partial results into final results

# Partitioning
- Data is split into chunks by a partitioner
- Can create your own 
- Goal: Improve performance
	- E.g., void costly delegate creation calls