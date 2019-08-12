# JobOrder

Enter your input in the form of characters with a space inbetween them. If a character has a dependency on it then place its dependency character next to it without a space.
So for example if in a sequence of 3 jobs such as "a","b" and "c" where "c" is dependant on "b", then the output should return "c" before "b". The input should look something like this, "a bc c".
Another example is if the input is "a bc cf da eb f" then the job order should return "afcbde".
