# ParalellProgramming
Information, exercises, and examples of the Core Multithreading and Parallelization concepts supported by the .NET Framework

- Use `Task<T>` is the fundamental building block
- Locks, mutexes and other structures control concurrent data access; or
- Use concurrent collections
	- `BlockingCollection` for the producer-consumer pattern

- Plenty of data structures for task coordination
- Parallelize a loop with `Parallel.For`/`ForEach()`
- Parallelize a LINQ query with `AsParallel()`