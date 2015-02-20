unity-scrolly
=============

A Touch/Drag scroll list implementation using Unity GUI (4.6+)

(Update 20-Feb-2015)
= Added simple cell pooling
- Updated project to work with latest Unity (4.6.3) 

Usage:

- Make a class for the list (ScrollListTest) and inherit from UIList<TCell>
- Make a class for the cell and feed it to UIList Generic (TCell --> ScrollCellTest) 
- Implement Abstract functions of UIList<T> in your list class and override as desired.
- Call Refresh() to make the list.


