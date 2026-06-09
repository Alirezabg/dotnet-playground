# Topic 4 — Generics & Constraints

**Interview definition (fill this in yourself before asking Copilot):**

> _Your answer here... (Why generics over `object`? What do type constraints buy you?)_

## Your Task
Design a generic `IRepository<T>` abstraction for the Product Catalogue.
- Define `Add`, `GetById`, and `GetAll` over a type parameter `T`
- Add a constraint so `T` must be an entity with an `Id` (e.g. `where T : IEntity`)
- Provide a simple in-memory implementation backed by a `Dictionary<Guid, T>`

Write your code below in a new `.cs` file in this folder.

## Questions Your Instructor Will Ask
- What problem do generics solve that `object` + casting does not?
- What does the `where T : ...` constraint give you inside the method body?
- What is the difference between covariance and contravariance, and where have you seen `out`/`in` on an interface?

## Interview Tip
> Be ready to explain *type safety* and *no boxing* as the two headline benefits of generics over `object`.


Generics let you write reusable code without losing type safety. Instead of storing everything as object and casting it back later, generics let the compiler know the real type at compile time.


In C#, boxing means taking a value type and wrapping it inside an object so it can be treated like a reference type.

Common value types:

int
double
decimal
bool
char
DateTime
Guid
struct
enum

Common reference types:

object
string
class
interface
array
delegate

Boxing example
int number = 5;

object boxedNumber = number;

Unboxing example
object boxedNumber = 5;

int number = (int)boxedNumber;

Why does boxing matter?

Because boxing creates an object on the heap.

Example:

int number = 5;
object boxed = number;

The int value must be copied into a new object.

That means:

1. Extra memory allocation
2. Extra work for the runtime
3. Later garbage collection

For one value, it does not matter much.

For thousands or millions of values, it can hurt performance.


Boxing is when a value type, like int, is converted to object. The runtime wraps the value in an object on the heap. Unboxing is converting that object back to the original value type. Generics avoid this because List<int> stores integers directly, while old non-generic collections like ArrayList store values as object, causing boxing and unboxing.