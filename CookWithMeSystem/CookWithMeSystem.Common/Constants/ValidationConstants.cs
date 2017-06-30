﻿namespace CookWithMeSystem.Common.Constants
{
    public class ValidationConstants
    {
        public const int MinPassword = 6;

        public const int MinRecipeTitle = 3;

        public const int MinRecipeDirections = 50;

        public const int MinRecipeServings = 1;

        public const int MinPreparationTime = 5;

        public const int MinIngredientName = 3;

        public const int MinStepAction = 5;

        public const int MinStepTime = 1;

        public const int MaxFirstName = 50;

        public const int MaxLastName = 50;

        public const int MaxRecipeTitle = 50;

        public const int MaxRecipeDirections = 1000;

        public const int MaxIngredientName = 50;

        public const int MaxCommentContent = 1000;

        public const int MaxCategoryName = 50;

        public const int MaxRecipeServings = 25;

        public const int MaxPreparationTime = 1000;

        public const int MaxStepAction = 250;

        public const int MaxStepTime = 1000;


        public const string RecipeErrorMessage = "{0} should be at least {2} characters long.";

        public const string RecipeNotFoundErrorMessage = "Recipe could not be found.";

    }
}
