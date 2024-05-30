using System;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Recipe[] recipes = new Recipe[10]; // Initial capacity, can be adjusted as needed
            int numRecipes = 0;

            Console.WriteLine("Welcome to the Recipe Application!");

            while (true)
            {
                Console.WriteLine("\n1. Enter Recipe Details");
                Console.WriteLine("2. Display Recipe List");
                Console.WriteLine("3. Display Recipe");
                Console.WriteLine("4. Scale Recipe");
                Console.WriteLine("5. Reset Quantities");
                Console.WriteLine("6. Clear All Data");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");

                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option. Please enter a number.");
                    continue;
                }

                try
                {
                    switch (option)
                    {
                        case 1:
                            numRecipes = EnterRecipe(recipes, numRecipes);
                            break;
                        case 2:
                            DisplayRecipeList(recipes, numRecipes);
                            break;
                        case 3:
                            DisplayRecipe(recipes, numRecipes);
                            break;
                        case 4:
                            ScaleRecipe(recipes, numRecipes);
                            break;
                        case 5:
                            ResetQuantities(recipes, numRecipes);
                            break;
                        case 6:
                            numRecipes = ClearData(recipes, numRecipes);
                            break;
                        case 7:
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please enter a number between 1 and 7.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        static int EnterRecipe(Recipe[] recipes, int numRecipes)
        {
            if (numRecipes >= recipes.Length)
            {
                Console.WriteLine("Recipe storage is full. Cannot add more recipes.");
                return numRecipes;
            }

            Console.Write("\nEnter the name for the recipe: ");
            string name = Console.ReadLine();

            Console.Write("\nEnter the number of ingredients: ");
            int numIngredients;
            if (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0)
            {
                Console.WriteLine("Invalid number of ingredients. Please enter a positive number.");
                return numRecipes;
            }

            Ingredient[] ingredients = new Ingredient[numIngredients];

            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"Enter ingredient {i + 1} name: ");
                string ingredientName = Console.ReadLine();

                Console.Write($"Enter quantity for {ingredientName}: ");
                double quantity;
                if (!double.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a positive number.");
                    return numRecipes;
                }

                Console.Write($"Enter unit of measurement for {ingredientName}: ");
                string unit = Console.ReadLine();

                Console.Write($"Enter number of calories for {ingredientName}: ");
                double calories;
                if (!double.TryParse(Console.ReadLine(), out calories) || calories < 0)
                {
                    Console.WriteLine("Invalid number of calories. Please enter a non-negative number.");
                    return numRecipes;
                }

                Console.Write($"Enter food group for {ingredientName}: ");
                string foodGroup = Console.ReadLine();

                ingredients[i] = new Ingredient(ingredientName, quantity, unit, calories, foodGroup);
            }

            Console.Write("\nEnter the number of steps: ");
            int numSteps;
            if (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0)
            {
                Console.WriteLine("Invalid number of steps. Please enter a positive number.");
                return numRecipes;
            }

            string[] steps = new string[numSteps];

            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Enter step {i + 1} description: ");
                steps[i] = Console.ReadLine();
            }

            recipes[numRecipes] = new Recipe(name, ingredients, steps);
            numRecipes++;

            Console.WriteLine("\nRecipe details entered successfully!");
            return numRecipes;
        }

        static void DisplayRecipeList(Recipe[] recipes, int numRecipes)
        {
            if (numRecipes == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.WriteLine("\nRecipe List:");
            Array.Sort(recipes, 0, numRecipes, Comparer<Recipe>.Create((x, y) => x.Name.CompareTo(y.Name)));
            for (int i = 0; i < numRecipes; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }
        }

        static void DisplayRecipe(Recipe[] recipes, int numRecipes)
        {
            if (numRecipes == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.Write("\nEnter the number of the recipe to display: ");
            int recipeNumber;
            if (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > numRecipes)
            {
                Console.WriteLine("Invalid recipe number. Please enter a number between 1 and " + numRecipes);
                return;
            }

            Recipe recipe = recipes[recipeNumber - 1];
            recipe.Display();
        }

        static void ScaleRecipe(Recipe[] recipes, int numRecipes)
        {
            if (numRecipes == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.Write("\nEnter the number of the recipe to scale: ");
            int recipeNumber;
            if (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > numRecipes)
            {
                Console.WriteLine("Invalid recipe number. Please enter a number between 1 and " + numRecipes);
                return;
            }

            Recipe recipe = recipes[recipeNumber - 1];
            recipe.Scale();
        }

        static void ResetQuantities(Recipe[] recipes, int numRecipes)
        {
            if (numRecipes == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            Console.Write("\nEnter the number of the recipe to reset quantities: ");
            int recipeNumber;
            if (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > numRecipes)
            {
                Console.WriteLine("Invalid recipe number. Please enter a number between 1 and " + numRecipes);
                return;
            }

            Recipe recipe = recipes[recipeNumber - 1];
            recipe.ResetQuantities();
        }

        static int ClearData(Recipe[] recipes, int numRecipes)
        {
            for (int i = 0; i < numRecipes; i++)
            {
                recipes[i] = null;
            }

            Console.WriteLine("All data cleared successfully!");
            return 0;
        }
    }

    class Recipe
    {
        public string Name { get; }
        public Ingredient[] Ingredients { get; }
        public string[] Steps { get; }
        public int NumIngredients { get { return Ingredients.Length; } }
        public int NumSteps { get { return Steps.Length; } }

        public Recipe(string name, Ingredient[] ingredients, string[] steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
        }

        public void Display()
        {
            Console.WriteLine($"\nRecipe: {Name}");

            Console.WriteLine("\nIngredients:");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.CurrentQuantity} {ingredient.Unit} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }

            double totalCalories = 0;
            foreach (var ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            Console.WriteLine($"\nTotal Calories: {totalCalories}");
            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: Total calories exceed 300!");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }
        }

        public void Scale()
        {
            Console.Write("\nEnter scale factor (0.5 for half, 2 for double, 3 for triple): ");
            double factor;
            if (!double.TryParse(Console.ReadLine(), out factor) || factor <= 0)
            {
                Console.WriteLine("Invalid scale factor. Please enter a positive number.");
                return;
            }

            foreach (var ingredient in Ingredients)
            {
                ingredient.CurrentQuantity *= factor;
            }

            Console.WriteLine("Recipe scaled successfully!");
        }

        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.ResetQuantity();
            }

            Console.WriteLine("Quantities reset successfully!");
        }
    }

    class Ingredient
    {
        public string Name { get; }
        public double OriginalQuantity { get; }
        public double CurrentQuantity { get; set; }
        public string Unit { get; }
        public double Calories { get; }
        public string FoodGroup { get; }

        public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Name = name;
            OriginalQuantity = quantity;
            CurrentQuantity = quantity; // Set current quantity to original quantity initially
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        public void ResetQuantity()
        {
            CurrentQuantity = OriginalQuantity; // Reset current quantity to original quantity
        }
    }
}
