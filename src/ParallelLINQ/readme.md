# Summary
- Turn a LINQ query parallel by
	- Calling `AsParallel` on an `IEnumerable`
	- Use a `ParallelEnumerable`
- Use `WithCancellation()` to provide a cancellation token
- Catch 
	- `AggregateException`
	- `OperationCancelledException` to provide a cancellation token

- `WithMergeOptions(ParallelMergeOptions.Xxx)` determine how soon produced results can be consumed
- Parallel version of `Aggregate()` provides a syntax for custom per-task aggregation options