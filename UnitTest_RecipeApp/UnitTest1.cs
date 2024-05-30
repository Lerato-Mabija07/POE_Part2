using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApp;

namespace RecipeApp.Tests
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void CalculateTotalCalories_ReturnsCorrectTotal()
        {
            // Arrange
            var ingredients = new Ingredient[]
            {
                new Ingredient("Sugar", 100, "g", 387, "Carbohydrates"),
                new Ingredient("Butter", 50, "g", 717, "Dairy"),
                new Ingredient("Flour", 200, "g", 364, "Grains")
            };
            var steps = new string[] { "Mix ingredients", "Bake at 180C for 20 minutes" };
            var recipe = new Recipe("Cake", ingredients, steps);

            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            double expectedCalories = 387 + 717 + 364;
            Assert.AreEqual(expectedCalories, totalCalories);
        }
    }
}
