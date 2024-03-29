# Project 66.

Написать игру "Танчи по сети". За раз в одной игровой комнате может участвовать 2-4 танка. За победу начисляются монеты, которые можно тратить на прокачку оружия и брони. На сервере может быть не ограниченное количество игровых сессий (делать в последнюю очередь).  На сервере дать возможность просмотра списка игров и их стату, а так же - количество сессий. Для входа в игру необходимо пройти регистрацию (логин + пароль или просто логин). В дальнейшем вход в игру через эти данные. На стартовой форме клиента он может видеть магазин и свою статистику (количество игр / % побед и тд).
Саму игру реализовывать через WinForms + GDI, разумеется с MVC. Сервер - WinForms / WPF. Сам процесс боя легче всего реализовать через пересылки JSON информации о координатах игроков и тд.


![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_66_Client/Resources/Main.png)
___
# Project 60.

Implement an interactive aquarium game with functionality:

- Adding fish
- Feeding the fish

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_60/Resources/Form_Main.png)
___
# Project 59.

When the time limit for using the PC expires, desktop shortcuts should start moving randomly.
When you click on the shortcut - it should be removed from the "desktop". 
When there are no shortcuts left, the computer should turn off.
___
# Project 58.

Write a program to control applications.

Adding the application name to the lists:

- List of programs that will close when opened
- List of programs that will open when closed
___
# Project 56.

## Task 1:
Set up a windowed application that generates a set
prime numbers in a user-specified range.
If no lower bound is specified, the stream starts at 2.
If no upper bound is specified, generation occurs before the application terminates. Use the threading. 
## Task 2
Add to the first job a thread that generates
set of Fibonacci numbers. The numbers must
in the window interface.
## Task 3
Add buttons to the second task for complete
stop each of the threads. One button for one
flow. If the user clicked on the stop button,
the thread completely loses its work.
## Task 4
The purpose of the button for pausing and resuming each of the threads. For example,
the user can pause the generation of Fibonacci numbers by clicking on the button. Continuation of generation
possible by pressing another button.
## Task 5
Required for the fourth task
restarting streams.
___
# Project 48.

Implement 2 forms
- Authorizations
- Registrations

Data model :
- email
- password (5-15 characters)

Through dapper and MVC pattern to implement the work of the model
On the server side, there must be procedures !
The password must be transmitted unencrypted !
Add validation for mail (presence of '@' and '.' in the address)

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_48/Resources/Form_Login.png)
___
# Project 47.

Write your clone of WordPad

Menu File:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_47/Resources/Menu_File.png)

Menu Save as:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_47/Resources/Menu_SaveAs.png)


Menu Print:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_47/Resources/Menu_Print.png)


Menu Main:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_47/Resources/Menu_Main.png)


Menu View:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_47/Resources/Menu_View.png)
___
# Project 46.

Write your clone of NotePad++

Menu:

![Image text](https://raw.githubusercontent.com/VLola/c-sharp-winforms/master/Project_46/Resources/Menu_Notepad.png)
___
# Project 45.

Write your provider-contacts. The application should display a list of contacts. Should be functional:
+ Adding a contact
+ Deleting a contact
+ Search by name/surname/patronymic/operator/date added
+ Sort by first name/last name/middle name/operator/date added
+ When you click on the card - a window with more detailed information should open


The card consists of:
+ Name *
+ Surname
+ Middle name
+ Date of Birth
+ Phone number (1*-10) with operator auto-detection
+ Address (country / city / street / house / apartment)
+ Machine (make / model / reg number)
+ Company (name / position)

\* required fields

___
# Project 44.

Convert phonebook using disconnected mode of operation
___
# Project 43.

Create an application for convenient reading of books.

On the start window in the form of a shelf of books, display everything.
It should be possible to add a new one (with a random cover, from a txt file) or remove it.
When you click on a specific one - open it in a separate window

In the reading window - display the opportunity
+ page flipping forward/backward
+ closing the book (the next time you open it, it should return to the same page)
+ Voice over content

___
# Project 42.

## Task 1:
Display your (short!) summary using the MessageBox sequence (at least three in number).
Moreover, the title of the latter should display
average number of characters per page (total number of characters in summary / number of MessageBoxes).

## Task 2:
Write a function that "guesses" the number conceived by the user from 1 to 2000.
To query the user, use MessageBox.
After the number is guessed, it is necessary to display the number of requests,
required for this, and provide the user with the opportunity to play again,
without leaving the program (MessageBoxes are decorated with buttons and icons according to the situation).

## Task 3:
Imagine you have a rectangle on your form,
whose borders are 10 pixels apart from the borders of the form's workspace.
You need to create the following handlers:
+ Handler for pressing the left mouse button,
which outputs a message about where the current point is:
inside the rectangle, outside, on the border of the rectangle.
If the Control (Ctrl) button was pressed when the left mouse button was pressed,
then the application should close; 
+ Right click handler
which displays in the window title information about the size of the client (working) area of  the window in the form:
Width = x, Height y - the corresponding parameters of your window;
+ Handler for moving the mouse pointer within the workspace,
which should display the current x and y coordinates of the mouse in the window title.

## Task 4:
Develop an application based on a form.
  + The user "clicks" the left mouse button on the form and,
without releasing the button, moves the mouse over it,
and at the moment of releasing the button, according to the obtained coordinates of the rectangle
(you know, of course, that two points on the plane are enough to create a rectangle)
you need to create a "static", which contains its serial number
(meaning the order of appearance on the form)
  + The minimum size of a "static" is 10x10,
when trying to create an element of smaller sizes, the user should see a corresponding warning.
  + When right-clicking over a static surface
information about its area and coordinates (relative to the shape) should appear in the window title.
If there are several "statics" at the click point,
then preference is given to "static" with a serial number.
___