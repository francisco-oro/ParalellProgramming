# Concurrent Collections
- Concurrent collections use TryXxx() methods
	- Return a bool indicating success
- Optimized for multithreaded use
	- Some ops (e.g., Count) can block and make collection slow
- ConcurrentBag/Queue/Stack all implement IProducerConsumerCollection
- A BlockingCollection is a wrapper around one of the IProducerConsumerCollection classes
	- Provides blocking and bounding capabilities 