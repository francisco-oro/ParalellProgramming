# Overview
Typical .NET calls are blocking
```
int n = foo(); bar(n)
```
- Why should `foo()` block the current thread? (e.g., UI thread)
- Why not runn `foo()` in a separate thread and let `bar()` be a continuation?

Can spawn a thread to doo `foo()` in
- E.g., `ThreadPool.QueueUSerWorkItem` or `Task.Factory.StartNew`
- We can do `bar()` in a `ContinueWith()` call
- Too cumbersome, we waant to use 'ordinary' programming model
- Want to be able to call methods synchronously or asynchronously

.NET already has a 'resumable' paradigm
- `yield` allows temporary suspension of execution
- Repurposed for asynchronous use (PowerThreading `AsyncEnumerator`) 

.NET `async` and `await` keywords make it official

## How does async work?
Suppose you have a method
```c#
int Calculate();
```
To make it asynchronous, we redefine it as
```c#
TasK<int> CalculateAsync();
```
Internally, `Calculate` and `CalculateAsync` are the same
- They can both return 123
Now, the consuming method can be rewritten as
```c#
async void Foo()
{
	int n = await CalculateAsync();
}
```

## What does async do?
Not much, actually.
The `async` keyword does not change code genereated by the compiler
- Think of it as a compiler hint
- It enables the use of the `await` keyword
- Informs the compiler that, when it sees `await`, it's a special keyword
Why is this necessary? Because `await` wasn't a reserved word since the beginning of c#

## What does await do?
Quite a lot!
Does not perform a physical wait (blocking)
```c#
int foo = await CalculateAsync();
```

is not the same as
```c#
int foo = CalculateAsync().Result;
```
- This would block the caller - precisely what we want to avoid!

Notice there's no explicit unwrapping
- `await` coerces the return value from a `Task`
- And you complete in the same synchronization context, making the whole thing UI-safe!

## What await turns into
What you wrote: 
```c#
int n = await CalculateAsync();
tbFoo.Text = n.ToString();
```
What you end up with (approximate!);
```c#
CalculateAsync()
	.Start()
	.ContinueWith(t => tbFooo.Text = t.Result.ToString(), TaskScheduler.FromCurrentSynchronizationContext())` 
```

Registers continuation with the async operation
- In other words, "code that follows me is a continuation, like `ContinueWith`"
Gives up the current thread
- It's over. We no longer do anything

# ValueTask Usage Guidelines
Should every new async API return `ValueTask`? No. 
Default choice should still be `Task`/`Task<T>`
Easier to use correctly
Cached tasks (`Task` or `Task<bool>`) are faster to await than `ValueTasks`
`ValueTask`/`ValueTask<T>` are good choices when 
- You expect Api consumers to only await them directly; and
- You want to avoid allocation-related overhead; and 
- Either you expect sync completion to be a common case or you're able to effectively pool objects for the use of asynchronous completion

# Summary
Asyncrhronous programming is enabled with the `async` and `await` keywords
Methods that `await` must be decorated with `async`
When you `await`, you abandon current thread, fire up a new one and then do a context-preserving continuation of the code that's left
You can call `async` method synchronously
.NET 4.5 gives us awaitable `Task.Delay` and combinators