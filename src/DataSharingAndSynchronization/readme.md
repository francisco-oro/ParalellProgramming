# Critical Sections
- Uses the `lock` keyword
- Typically locks on an existing object
	- Best to make a new object to lock on
- A shorthand for Montor.Enter()/Exit()
- Blocks until a lock is available
	- Unless you use Monitor.TryEnter() with a timeout value

# Interlocked Operations
- Useful for atomically changing low-level primitives
- Interlocked.Increment()/Decrement()
- Interlocked.Add()
- Exchange/CompareExchange()

# Spin Locking and Lock Recursion
- A spin lock wastes CPU cycles without yielding
	- Useful for brief pauses to prevent rescheduling
- Enter() to take, Exit() to release (if taken successfully)
- Lock recursion = ability to enter a lock twice on the same thread
- SpinLock does not support lock recursion
- Owner tracking helps keep a record of thread that acquired the lock
	- Recursion w/tracking = exception, w/o =  deadlock

# Mutex
- A WaitHandle-derived synchronization primitive
- WaitOne() to acquire
	- Possibly with a timeout
- ReleaseMutex() to release
- Mutex.WaitAll() to acquire several
- Global/named mutexes are shared between processes
	- Mutex.OpenExisting() to acquire
	- mutex = new Mutex(false, <name>)

# Reader-Writer Locks
- A reader-writer lock can lock for reading or writing
	- (Enter/Exit)(Read/Write)Lock()
- Supports lock recursion in ctor parameter
	- Not recommended
- Supports upgreadeability
	- Enter/ExitUpgreadableReadLock() 