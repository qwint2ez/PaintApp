using ConsolePaintApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaintApp;

namespace PaintApp.Tests
{
    [TestClass]
    public class CanvasTests
    {
        private Canvas canvas;

        [TestInitialize]
        public void Setup()
        {
            Canvas.IsTesting = true;
            canvas = new Canvas();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Canvas.IsTesting = false;
        }

        [TestMethod]
        public void AddShape_ValidCircle_AddsToCanvas()
        {
            Shape circle = new Circle(5, 5, 3);

            canvas.AddShape(circle);

            Assert.AreEqual(1, canvas.GetShapes().Count);
            Assert.AreEqual(circle, canvas.GetShapes()[0]);
        }

        [TestMethod]
        public void AddShape_OutOfBoundsCircle_DoesNotAdd()
        {
            Shape circle = new Circle(38, 18, 5);

            canvas.AddShape(circle);

            Assert.AreEqual(0, canvas.GetShapes().Count);
        }

        [TestMethod]
        public void EraseShape_ValidIndex_RemovesShape()
        {
            Shape circle = new Circle(5, 5, 3);
            canvas.AddShape(circle);

            canvas.EraseShape(0);

            Assert.AreEqual(0, canvas.GetShapes().Count);
        }

        [TestMethod]
        public void EraseShape_InvalidIndex_DoesNothing()
        {
            Shape circle = new Circle(5, 5, 3);
            canvas.AddShape(circle);

            canvas.EraseShape(1);

            Assert.AreEqual(1, canvas.GetShapes().Count);
        }

        [TestMethod]
        public void MoveShape_ValidMove_UpdatesPosition()
        {
            Shape square = new Square(10, 10, 4);
            canvas.AddShape(square);

            canvas.MoveShape(0, 15, 15);

            Assert.AreEqual(15, square.X);
            Assert.AreEqual(15, square.Y);
        }

        [TestMethod]
        public void MoveShape_OutOfBounds_DoesNotMove()
        {
            Shape square = new Square(10, 10, 4);
            canvas.AddShape(square);

            canvas.MoveShape(0, 50, 50);

            Assert.AreEqual(10, square.X);
            Assert.AreEqual(10, square.Y);
        }

        [TestMethod]
        public void Undo_AfterAddShape_RemovesShape()
        {
            Shape circle = new Circle(5, 5, 3);
            canvas.AddShape(circle);

            canvas.Undo();

            Assert.AreEqual(0, canvas.GetShapes().Count);
        }

        [TestMethod]
        public void Redo_AfterUndo_ReAddsShape()
        {
            Shape circle = new Circle(5, 5, 3);
            canvas.AddShape(circle);
            canvas.Undo();

            canvas.Redo();

            Assert.AreEqual(1, canvas.GetShapes().Count);
            Shape restoredCircle = canvas.GetShapes()[0];
            Assert.AreEqual(circle.X, restoredCircle.X); 
            Assert.AreEqual(circle.Y, restoredCircle.Y);
            Assert.AreEqual(circle.Size, restoredCircle.Size);
            Assert.AreEqual(circle.FillChar, restoredCircle.FillChar);
            Assert.AreEqual(circle.BackgroundChar, restoredCircle.BackgroundChar);
        }
    }
}