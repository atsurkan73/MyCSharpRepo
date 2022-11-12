/* 
Create generic Stack<T> class with next methods: 
Push(obj) - adds obj at the top of stack
Pop() -returns top element of stack & removes it
Clear() -clear stack
Count - property return number of elements
Peek() -returns top element but doesn’t remove it
CopyTo(arr) -copies stack to array
*/

using StackLesson;


StackTest<Object> stackTest1 = new StackTest<Object>();
StackTest<Object> stackTest2 = new StackTest<Object>();
OperationInt operationInt = new OperationInt();
OperationString operationString = new OperationString();


// Calling generic methods of class StackTest<T> that deal with any type of data.


operationInt.ChoooseOperation(stackTest1);   //Int value is used for demonstration how stack works.  
operationString.ChoooseOperation(stackTest2); // String value is used for demonstration how stack works.  

