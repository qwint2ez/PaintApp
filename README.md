# PaintApp - Lab 1

## Description of the Project
This is a simple console-based Paint application implemented in C#. It allows users to draw shapes (circles and squares), erase them, move them, set backgrounds, save the canvas to a file, load from a file, and perform undo/redo actions. The application is designed to be user-friendly with interactive command input and robust error handling.

## Completed by Alexander Svidinsy, group 353504

## UML Diagram
![UML Diagram](https://github.com/qwint2ez/PaintApp/blob/main/uml_diagram.pdf)  

## Functional Requirements
1. **Draw a figure with configurable params:**  
   - Users can add a circle or square by specifying type, X, Y coordinates, and size. Coordinates start from the top-left corner, size defines diameter/side length. Errors are shown for invalid inputs (negative numbers or out-of-bounds).
   - Example: `add` → prompts for type, X, Y, size.

2. **Erase an object:**  
   - Removes a shape by its index from the list. Requires a valid non-negative index, otherwise shows an error.
   - Example: `erase` → prompts for index.

3. **Move object:**  
   - Moves a shape to new coordinates if they are within canvas bounds (40x20). Saves state for undo.
   - Example: `move` → prompts for index, new X, new Y.

4. **Add a background to a figure:**  
   - Sets a background character for the interior of a shape. Works for squares and circles with outlined borders.
   - Example: `background` → prompts for index and character.

5. **Save canvas as a file:**  
   - Saves current canvas state (dimensions and shape data) to a text file. Requires a filename.
   - Example: `save` → prompts for filename.

6. **Load from file:**  
   - Loads canvas state from a file if it exists and matches dimensions (40x20).
   - Example: `load` → prompts for filename.

7. **Undo/redo action:**  
   - Undoes or redoes the last action (add, erase, move, background). History is lost after a new action post-undo.
   - Example: `undo` or `redo`.

## Test Cases
See the table in the project documentation or code comments for a comprehensive set of test cases covering all functionalities.

## Additional Notes
- The application uses basic OOP principles: encapsulation (private fields with public methods), inheritance (Shape, Circle, Square), and polymorphism (Draw method).
- Canvas size is fixed at 40x20 for simplicity.
